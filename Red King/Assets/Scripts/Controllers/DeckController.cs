using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.XR;
using Random = System.Random;

namespace Cards
{
    public class DeckController : MonoBehaviour
    {
        public static DeckController Instance { get; private set; }

        [Header("Deck")]
        [SerializeField] private List<CardData> deck = new();
        [SerializeField] private int seed;
        [SerializeField] private bool useRandomSeed = true;
        [SerializeField] private bool isShuffled;
        public bool canDraw = true;
        public CardData drawnCard = null;
        public event Action DeckClicked;


        [Header("Hands")]
        [SerializeField] private GameObject cardPrefab;
        [SerializeField] private GameObject playerHand;
        [SerializeField] private GameObject botHand;
        public Sprite cardBack;
        [SerializeField] private int handSize = 4;
        public float cardSpacing = 1.2f;

        private DisplayCardController dcController;

        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
                return;
            }
            Instance = this;
        }

        private void OnDestroy()
        {
          if (Instance == this) Instance = null;  
        }

        private void Start()
        {
            dcController = FindAnyObjectByType<DisplayCardController>();
        }

        public CardData DrawTop()
        {
            if (deck.Count == 0) { Debug.LogWarning("Deck is empty"); return null; }
            var card = deck[^1];
            deck.RemoveAt(deck.Count - 1);
            return card;
        }

        public void ShuffleDeck()
        {
            if (useRandomSeed) seed = Environment.TickCount;

            var seededRandom = new Random(seed);

            for (int i = deck.Count - 1; i > 0; i--)
            {
                var j = seededRandom.Next(i + 1);
                (deck[i], deck[j]) = (deck[j], deck[i]);
            }

            Debug.Log("Deck has been shuffled and populated.");
        }

        private List<CardData> DealHand(int count)
        {
            var hand = new List<CardData>();

            if (count > deck.Count)
            {
                Debug.LogWarning($"Asked for {count} cards but only {deck.Count} left");
                count = deck.Count;
            }

            for (var i = 0; i < count; i++)
            {
                var top = deck.Count - 1;
                hand.Add(deck[top]);
                deck.RemoveAt(top);
            }

            return hand;
        }

        public void DealToHand(Transform handParent, int count)
        {
            var cards = DealHand(count);
            Debug.Log(cards.Count);

            for (var i = 0; i < cards.Count; i++)
            {
                var go = Instantiate(cardPrefab, handParent);
                go.name = cards[i].name;

                var x = (i - (cards.Count - 1) / 2f) * cardSpacing;
                go.transform.localPosition = new Vector3(x, 0f, 0f);

                var controller = go.GetComponent<CardController>();
                if (!controller)
                {
                    Debug.LogError("Card prefab has no CardController on it.", cardPrefab);
                    continue;
                }

                controller.cardData = cards[i];

                go.GetComponent<CardView>().Setup(cards[i], cardBack);
            }
        }

        private void OnMouseDown() => DeckClicked?.Invoke();
    }
}
