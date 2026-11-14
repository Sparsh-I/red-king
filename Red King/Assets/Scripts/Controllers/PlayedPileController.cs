using System;
using Cards;
using NUnit.Framework.Constraints;
using UnityEngine;

public class PlayedPileController : MonoBehaviour
{
    [SerializeField] private HandController playerHand;
    [SerializeField] private SpriteRenderer displayCard;

    void OnMouseDown()
    {
        if (!DeckController.Instance.drawnCard || !playerHand.hasDrawnCard) return;
        playerHand.PlayCard(DeckController.Instance.drawnCard);
        displayCard.sprite = null;
    }
}
