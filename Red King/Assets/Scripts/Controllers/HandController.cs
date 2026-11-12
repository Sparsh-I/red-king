using Cards;
using UnityEngine;
using UnityEngine.XR;

public class HandController : MonoBehaviour
{
    [Header("Controllers")]
    [SerializeField] private DeckController deckController;
    private DisplayCardController dcController;

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
        dcController = FindAnyObjectByType<DisplayCardController>();
        deckController = FindAnyObjectByType<DeckController>();
        var hand = GetComponent<Transform>();
        deckController.DealToHand(hand, maxHandCapacity);
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
        dcController.DisplayCard(card);
    }

    public void HighlightHand(bool on)
    {
        foreach (var outline in GetComponentsInChildren<CardOutline>())
            outline.SetOutline(on);
    }
}
