using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class PreserveSpriteSize : MonoBehaviour
{
    void Update()
    {
        Image image = GetComponent<Image>();
        if (image && image.sprite)
        {
            image.SetNativeSize();
        }
    }
}
