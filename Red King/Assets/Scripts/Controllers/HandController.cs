using Cards;
using NUnit.Framework.Constraints;
using UnityEngine;
using UnityEngine.XR;

[RequireComponent(typeof(Transform))]
public class HandController : MonoBehaviour
{
    [Header("Controllers")]
    [SerializeField] private DeckController deckController;
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


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        var hand = GetComponent<Transform>();
        
        dcController = FindAnyObjectByType<DisplayCardController>();
        
        deckController = FindAnyObjectByType<DeckController>();
        deckController.DealToHand(hand, maxHandCapacity);

        pcSpawner = FindAnyObjectByType<PlayedCardSpawner>();
    }

    // Update is called once per frame
    void Update()
    {
        if (currentlyPlaying)
        {
            
        }
    }

    public void PlayCard(CardData card)
    {
        Debug.Log("Played the " + card.rank + " of " + card.suit);
        pcSpawner.SpawnCard(card);
        // dcController.DisplayCard(card);

        foreach (Transform child in transform)
        {
            var controller = child.GetComponent<CardController>();
            if (controller != null && controller.cardData == card) {
                Destroy(child.gameObject);
                break;
            }
        }
    }

    public void HighlightHand(bool on)
    {
        foreach (var outline in GetComponentsInChildren<CardOutline>())
            outline.SetOutline(on);
    }
}
