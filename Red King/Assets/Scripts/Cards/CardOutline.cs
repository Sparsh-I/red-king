using UnityEngine;

public class CardOutline : MonoBehaviour
{
    [SerializeField] private Color outlineColour = Color.yellow;
    [SerializeField] private float outlineScale = 1f;

    private SpriteRenderer main;
    private SpriteRenderer outline;

    void Awake()
    {
        main = GetComponent<SpriteRenderer>();

        var go = new GameObject("Outline");
        go.transform.SetParent(transform, false);
        go.transform.localScale = Vector3.one * outlineScale;

        outline = go.AddComponent<SpriteRenderer>();
        outline.color = outlineColour;
        outline.enabled = false;
    }

    void LateUpdate()
    {
        outline.sprite = main.sprite;
        outline.sortingLayerID = main.sortingLayerID;
        outline.sortingOrder = main.sortingOrder - 1;
    }

    public void SetOutline(bool on) => outline.enabled = on;
}
