using UnityEngine;
using UnityEngine.EventSystems;

public class HoverEffect : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Vector3 hoverScale = new Vector3(1.1f, 1.1f, 1.1f); // Scale when hovered
    private Vector3 originalScale; // Original scale to return to

    void Start()
    {
        originalScale = transform.localScale; // Save the original scale
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        transform.localScale = hoverScale; // Enlarge the object
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        transform.localScale = originalScale; // Return to original size
    }
}