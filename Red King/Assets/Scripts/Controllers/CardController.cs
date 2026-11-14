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
            if (!handController.isBot)
            {
                if (handController.hasDrawnCard) 
                {
                    Debug.Log("has drawn card branch");
                    handController.ReplaceCard(cardData, DeckController.Instance.drawnCard);
                } 
                else handController.PlayCard(cardData);
            
            }
        }
    }
}
