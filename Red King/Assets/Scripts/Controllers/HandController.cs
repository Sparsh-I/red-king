using System;
using System.Collections;
using Cards;
using NUnit.Framework;
using NUnit.Framework.Constraints;
using UnityEngine;
using UnityEngine.XR;

[RequireComponent(typeof(Transform))]
public class HandController : MonoBehaviour
{
    private DisplayCardController dcController;
    private PlayedCardSpawner pcSpawner;

    [Header("Player State")]
    public bool currentlyPlaying = false;
    public bool hasDrawnCard = false;
    public bool isBot = false;
    public int maxHandCapacity = 4;

    [Header("Player Actions")]
    [SerializeField] private bool canView = false;
    [SerializeField] private bool canViewOpp = false;
    [SerializeField] private bool canSwap = false;

    public CardData drawnCard { get; private set; }
    public event Action<HandController> TurnFinished;

    private void Awake()
    {
        dcController = FindAnyObjectByType<DisplayCardController>();
        pcSpawner = FindAnyObjectByType<PlayedCardSpawner>();
    }

    public void DealStartingHand() => DeckController.Instance.DealToHand(transform, maxHandCapacity);

    public void BeginTurn()
    {
        currentlyPlaying = true;
        hasDrawnCard = false;
        drawnCard = null;
        HighlightHand(true);

        if (isBot) StartCoroutine(BotTurn());
    }

    public void EndTurn()
    {
        currentlyPlaying = false;
        HighlightHand(false);
        dcController.HideCard();
    }

    public void DrawCard()
    {
        if (!currentlyPlaying || hasDrawnCard) return;

        drawnCard = DeckController.Instance.DrawTop();
        if (drawnCard == null) return;

        hasDrawnCard = true;
        if (!isBot) dcController.DisplayCard(drawnCard);
    }

    public void SwapDrawnWith(CardData cardInHand)
    {
        if (!currentlyPlaying || !hasDrawnCard) return;

        pcSpawner.SpawnCard(cardInHand);
        ReplaceCard(cardInHand, drawnCard);
        FinishTurn();
    }

    public void DiscardDrawn()
    {
        if (!currentlyPlaying || !hasDrawnCard) return;

        pcSpawner.SpawnCard(drawnCard);
        FinishTurn();
    }

    private void FinishTurn()
    {
        hasDrawnCard = false;
        drawnCard = null;
        TurnFinished?.Invoke(this);
    }

    private IEnumerator BotTurn()
    {
        yield return new WaitForSeconds(1f);
        DrawCard();
        if (!hasDrawnCard) { FinishTurn(); yield break; }

        yield return new WaitForSeconds(1f);
        var cards = GetComponentsInChildren<CardController>();
        if (cards.Length > 0 && UnityEngine.Random.value > 0.5f)
            SwapDrawnWith(cards[UnityEngine.Random.Range(0, cards.Length)].cardData);
        else
            DiscardDrawn();
    }

    public void ReplaceCard(CardData cardInHand, CardData newCard)
    {
        foreach (Transform child in transform)
        {
            var controller = child.GetComponent<CardController>();
            if (controller == null || controller.cardData != cardInHand) continue;

            controller.cardData = newCard;
            child.name = newCard.name;
            child.GetComponent<CardView>().Setup(newCard, DeckController.Instance.cardBack);
            return;
        }
    }

    public void HighlightHand(bool on)
    {
        foreach (var outline in GetComponentsInChildren<CardOutline>())
            outline.SetOutline(on);
    }
}
