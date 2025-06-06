using UnityEngine;

namespace Cards {
    [CreateAssetMenu(fileName = "NewCard", menuName = "Card")]
    public class CardData : ScriptableObject
    {
        [Header("Card Value")]
        [SerializeField] public int rank;
        [SerializeField] public CardSuit suit;
        [SerializeField] public Sprite cardSprite;
    }
}
