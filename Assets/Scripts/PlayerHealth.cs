using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    public int vidaMax = 3;
    private int vidaActual;
    public Slider barraVida;

    void Start()
    {
        vidaActual = vidaMax;

        if (barraVida != null)
        {
            barraVida.maxValue = vidaMax;
            barraVida.value = vidaMax;
        }
    }

    public void RecibirDaño(int daño)
    {
        vidaActual -= daño;
        if (barraVida != null) barraVida.value = vidaActual;

        if (vidaActual <= 0)
        {
            Morir();
        }
    }

    void Morir()
    {
        FindObjectOfType<GameManager>().PerderJuego();
    }
}