using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using Random = System.Random;

namespace Cards
{
    public class DeckController : MonoBehaviour
    {
        [SerializeField] private List<CardData> deck = new();
        [SerializeField] private int seed;
        [SerializeField] private bool useRandomSeed = true;
        [SerializeField] private bool isShuffled;

        [Header("Hands")]
        [SerializeField] private GameObject cardPrefab;
        [SerializeField] private Transform playerHand;
        [SerializeField] private Transform botHand;
        [SerializeField] private Sprite cardBack;
        [SerializeField] private int handSize = 4;
        [SerializeField] private float cardSpacing = 1.2f;

        private void Start()
        {
            if (isShuffled) ShuffleDeck();

            DealToHand(playerHand, handSize);
            DealToHand(botHand, handSize);
        }

        private void Update()
        {
            
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

        public List<CardData> DealHand(int count)
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

        private void DealToHand(Transform handParent, int count)
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
    }
}
