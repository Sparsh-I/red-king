using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cards
{
    public class CardController : MonoBehaviour
    {
        public CardData cardData;
        private HandController handController;
        
        // Start is called before the first frame update
        void Start()
        {
            handController = GetComponentInParent<HandController>();
        }

        void OnMouseDown()
        {
            if (!handController.isBot) handController.PlayCard(cardData);
        }
    }
}
