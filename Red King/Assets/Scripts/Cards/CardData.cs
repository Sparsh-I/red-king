using UnityEngine;

namespace Cards {
    [CreateAssetMenu(fileName = "NewCard", menuName = "Card")]
    public class CardData : ScriptableObject
    {
        [Header("Card Value")]
        public int rank;
        public CardSuit suit;
        public Sprite cardSprite;
    }
}
