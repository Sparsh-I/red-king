using UnityEngine;

namespace Cards
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class CardView : MonoBehaviour
    {
        public CardData Data { get; private set; }

        public void Setup(CardData data, Sprite cardBack)
        {
            Data = data;
            GetComponent<SpriteRenderer>().sprite = cardBack;
        }
    }
}