using Cards;
using UnityEngine;

public class DisplayCardController : MonoBehaviour
{
    private SpriteRenderer sr;
    [SerializeField] private int maxScale;
    [SerializeField] private float growthRate = 0.1f;
    public bool isDisplaying = false;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        isDisplaying = false;
    }

    // Update is called once per frame
    void Update()
    {   
        GetComponent<Renderer>().enabled = isDisplaying;

        if (isDisplaying)
        {
            var target = Vector3.one * maxScale;
            transform.localScale = Vector3.MoveTowards(
                transform.localScale, target, growthRate * Time.deltaTime);
        }
        else transform.localScale = Vector3.one;

        // if (isDisplaying) {
        //     if (transform.localScale.x <= maxScale)
        //         transform.localScale += new Vector3(transform.localScale.x + growthRate, 
        //                                             transform.localScale.y + growthRate, 
        //                                             transform.localScale.z + growthRate);
        // }
        // else transform.localScale = Vector3.one;
    }

    public void DisplayCard(CardData card)
    {
        sr.sprite = card.cardSprite;
        isDisplaying = true;
    }

    public void HideCard()
    {
        isDisplaying = false;
    }
}
