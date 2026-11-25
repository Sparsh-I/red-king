using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cards
{
    public class CardController : MonoBehaviour
    {
        public CardData cardData;
        private HandController handController;
        
        private void Awake()
        {
            handController = GetComponentInParent<HandController>();
        }

        private void OnMouseDown()
        {
            if (handController == null || handController.isBot) return;
            if (!handController.currentlyPlaying) return;

            if (handController.hasDrawnCard)
                handController.SwapDrawnWith(cardData);
            else
                Debug.Log("Draw a card first");
        }
    }
}
