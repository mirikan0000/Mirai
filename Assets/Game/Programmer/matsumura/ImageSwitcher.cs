using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ImageSwitcher : MonoBehaviour
{
    public Sprite[] images;
    private int currentImageIndex = 0;
    private Image imageComponent;

    private void Start()
    {
        imageComponent = GetComponent<Image>();
        if (imageComponent == null)
        {
            Debug.LogError("Image component is not attached to the object.");
            return;
        }

        if (images.Length > 0)
        {
            imageComponent.sprite = images[currentImageIndex];
        }
    }

    private void Update()
    {
        if (imageComponent == null)
        {
            return;
        }

        if (Input.GetKeyDown(KeyCode.I))
        {
            currentImageIndex = (currentImageIndex + 1) % images.Length;
            imageComponent.sprite = images[currentImageIndex];
        }
    }
}
