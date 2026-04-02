using UnityEngine;
using UnityEngine.UI;

public class TileControl : MonoBehaviour
{
    public Sprite greySprite;
    public Sprite colorSprite;

    private Image myImage;

    void Awake()
    {
        myImage = GetComponent<Image>();
    }

    public void UpdateVisual(bool isRevealed)
    {
        if (isRevealed)
        {
            myImage.sprite = colorSprite;
        }
        else
        {
            myImage.sprite = greySprite;
        }

        
        myImage.SetNativeSize();
    }
}