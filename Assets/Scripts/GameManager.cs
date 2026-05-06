using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [Header("Ajustes de Juego")]
    public int basuraTotal = 0;
    public int basuraRecogida = 0;
    public bool faseLimpiezaCompletada = false;

    [Header("Paneles UI")]
    public GameObject pantallaGanar;
    public GameObject pantallaPerder;

    void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);

        Time.timeScale = 1f;
    }

    public void RegistrarBasura()
    {
        basuraTotal++;
    }

    public void RecogerBasura()
    {
        basuraRecogida++;

        if (basuraRecogida >= basuraTotal)
        {
            CompletarFaseLimpieza();
        }
    }

    void CompletarFaseLimpieza()
    {
        faseLimpiezaCompletada = true;
        Debug.Log("¡Fase de limpieza completada! Ahora a colorear.");

        Tile[] todasLasCasillas = Object.FindObjectsByType<Tile>(FindObjectsSortMode.None);
        foreach (Tile tile in todasLasCasillas)
        {
            tile.PrepararParaColorear();
        }
    }

    public void GanarJuego()
    {
        pantallaGanar.SetActive(true);
        Time.timeScale = 0f;
    }

    public void PerderJuego()
    {
        pantallaPerder.SetActive(true);
        Time.timeScale = 0f;
    }

    public void ReiniciarJuego()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}