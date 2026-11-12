using Cards;
using UnityEngine;

public class HandController : MonoBehaviour
{
    [SerializeField] private bool canView = false;
    [SerializeField] private bool canViewOpp = false;
    [SerializeField] private bool canSwap = false;
    public bool currentlyPlaying = false;
    public bool hasDrawnCard = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (currentlyPlaying)
        {
            
        }
    }

    public void PlayCard(CardData card)
    {
        Debug.Log("Played the " + card.rank + " of " + card.suit);
    }
}
