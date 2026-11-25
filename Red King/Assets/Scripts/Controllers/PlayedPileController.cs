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
        if (playerHand == null || !playerHand.currentlyPlaying) return;
        if (!playerHand.hasDrawnCard) return;

        playerHand.DiscardDrawn();
    }
}
