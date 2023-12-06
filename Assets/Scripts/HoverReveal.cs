using UnityEngine;
using UnityEngine.EventSystems; // Required for Event Trigger

public class HoverReveal : MonoBehaviour
{
    public Vector3 enlargedScale = new Vector3(1.1f, 1.1f, 1.1f);
    public Sprite normalSprite;
    public Sprite hoverSprite;

    private Vector3 originalScale;
    private SpriteRenderer spriteRenderer;

    void Start()
    {
        originalScale = transform.localScale;
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void OnMouseEnter()
    {
        transform.localScale = enlargedScale;
        if (hoverSprite != null)
            spriteRenderer.sprite = hoverSprite;
    }

    void OnMouseExit()
    {
        transform.localScale = originalScale;
        spriteRenderer.sprite = normalSprite;
    }
}
