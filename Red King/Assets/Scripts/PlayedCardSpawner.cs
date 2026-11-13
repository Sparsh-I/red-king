using Cards;
using UnityEngine;

public class PlayedCardSpawner : MonoBehaviour
{
    [SerializeField] private int maxTilt;
    private int playedCardCount;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playedCardCount = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SpawnCard(CardData card)
    {   
        playedCardCount++;

        GameObject playedCard = new("Played Card");

        playedCard.transform.position = new Vector3(0,0,0);
        playedCard.transform.SetParent(transform);

        playedCard.AddComponent<SpriteRenderer>();
        var sr = playedCard.GetComponent<SpriteRenderer>();
        sr.sprite = card.cardSprite;
        sr.sortingOrder = playedCardCount;

        int randomNum = Random.Range(maxTilt * -1, maxTilt);
        playedCard.transform.rotation = Quaternion.Euler(0f, 0f, randomNum);
    }
}
