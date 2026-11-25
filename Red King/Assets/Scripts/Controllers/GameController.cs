using Cards;
using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour
{
    [SerializeField] private DeckController deck;
    [SerializeField] private HandController playerHand, botHand;
    [SerializeField] private float turnDelay = 1f;

    private HandController currentHand;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private IEnumerator Start()
    {
        playerHand.isBot = false;
        botHand.isBot = true;

        playerHand.TurnFinished += OnTurnFinished;
        botHand.TurnFinished += OnTurnFinished;
        deck.DeckClicked += OnDeckClicked;

        deck.ShuffleDeck();
        playerHand.DealStartingHand();
        botHand.DealStartingHand();

        yield return null;

        StartTurn(playerHand);
    }

    private void OnDestroy()
    {
        playerHand.TurnFinished -= OnTurnFinished;
        botHand.TurnFinished -= OnTurnFinished;
        deck.DeckClicked -= OnDeckClicked;
    }

    private void StartTurn(HandController hand)
    {
        currentHand = hand;
        hand.BeginTurn();
        Debug.Log(hand.isBot ? "Bot's turn" : "Player's turn");
    }

    private void OnTurnFinished(HandController finished)
    {
        if (finished != currentHand) return;
        StartCoroutine(SwitchTurn());
    }

    private void OnDeckClicked()
    {
        if (currentHand != null && !currentHand.isBot) currentHand.DrawCard();
    }

    private IEnumerator SwitchTurn()
    {
        currentHand.EndTurn();
        yield return new WaitForSeconds(turnDelay);

        StartTurn(currentHand == playerHand ? botHand : playerHand);
    }
}
