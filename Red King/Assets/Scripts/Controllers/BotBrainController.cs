using System.Collections.Generic;
using System.Linq;
using Cards;
using UnityEngine;

[System.Serializable]
public struct MemoryEntry
{
    public CardData card;
    public int position;
    public int turnsSinceSeen;

    public MemoryEntry(CardData card, int position, int turnsSinceSeen = 0)
    {
        this.card = card;
        this.position = position;
        this.turnsSinceSeen = turnsSinceSeen;
    }
}

public class BotBrainController : MonoBehaviour
{
    [SerializeField] private float memoryStrength;
    [SerializeField] private List<MemoryEntry> memoryList; 

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Awake()
    {
        memoryList = new List<MemoryEntry>();;    
    }

    // public void AddCardToMemory(CardData card)
    // {
    //     memoryList.Append
    // }
}
