using UnityEngine;
using UnityEngine.UI; // Importante para usar Image

public class TileControl : MonoBehaviour
{
    public Sprite greySprite;
    public Sprite colorSprite;

    private Image myImage; // Cambiado de SpriteRenderer a Image

    void Awake()
    {
        myImage = GetComponent<Image>(); // Cambiado de SpriteRenderer a Image
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

        // ESTA LÍNEA OBLIGA A LA UI A AJUSTARSE AL RECORTE DEL SPRITE
        myImage.SetNativeSize();
    }
}