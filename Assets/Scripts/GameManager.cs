using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [Header("Ajustes de Juego")]
    public int basuraTotal = 0;
    public int basuraRecogida = 0;
    public TextMeshProUGUI textoBasura;
    public bool faseLimpiezaCompletada = false;

    [Header("Paneles UI")]
    public GameObject pantallaGanar;
    public GameObject pantallaPerder;

    void Awake()
    {
        if (instance == null)
        {
            instance = this; 
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        Time.timeScale = 1f;
    }

    void Start()
    {
        ActualizarInterfaz();
    }

    public void RegistrarBasura()
    {
        basuraTotal++;
        ActualizarInterfaz();
    }

    public void RecogerBasura()
    {
        basuraRecogida++;
        ActualizarInterfaz();

        if (basuraRecogida >= basuraTotal)
        {
            CompletarFaseLimpieza();
        }
    }

    public void ActualizarInterfaz()
    {
        if (textoBasura != null)
        {
            textoBasura.text = "Basura: " + basuraRecogida + " / " + basuraTotal;
            textoBasura.color = Color.yellow;
        }
        else
        {
            // Ver si el hueco está vacío
            Debug.LogWarning("Textobasura vacío");
        }
    }

    void CompletarFaseLimpieza()
    {
        faseLimpiezaCompletada = true;
        Debug.Log("¡Fase de limpieza completada! Ahora a colorear.");

        Tile[] todasLasCasillas = Object.FindObjectsByType<Tile>(FindObjectsSortMode.None);
        foreach (Tile tile in todasLasCasillas)
        {
            if (tile != null) tile.PrepararParaColorear();
        }
    }

    public void GanarJuego()
    {
        if (pantallaGanar != null) pantallaGanar.SetActive(true);
        Time.timeScale = 0f;
    }

    public void PerderJuego()
    {
        if (pantallaPerder != null) pantallaPerder.SetActive(true);
        Time.timeScale = 0f;
    }

    public void ReiniciarJuego()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}