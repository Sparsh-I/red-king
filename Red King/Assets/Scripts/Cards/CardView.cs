using UnityEngine;

namespace Cards
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class CardView : MonoBehaviour
    {
        public CardData Data { get; private set; }
        [SerializeField] private Transform card;
        [SerializeField] private Vector3 hoverScale = new(0.15f, 0.15f, 0.15f);

        public void Setup(CardData data, Sprite cardBack)
        {
            Data = data;
            GetComponent<SpriteRenderer>().sprite = cardBack;
        }

        public void OnMouseEnter()
        {
            card.transform.localScale += hoverScale;
        }

        public void OnMouseExit()
        {
            card.transform.localScale = new Vector3(1, 1, 1);
        }
    }
}