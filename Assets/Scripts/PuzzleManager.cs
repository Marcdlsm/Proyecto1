using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using TMPro;
using UnityEngine.SceneManagement;
using System.Collections;

public class PuzzleManager : MonoBehaviour
{
    public GameObject tilePrefab;
    public Sprite cuadroGris;

    private TileControl[,] tablero = new TileControl[4, 4];
    private bool[,] estados = new bool[4, 4]; //false = gris, true = color

    [Header("Configuración del Contador")]
    public int movimientosRestantes = 4;
    public TextMeshProUGUI textoContador;

    [Header("Configuración de la Intro")]
    public CanvasGroup panelIntro;         
    public TextMeshProUGUI textoDialogo;   
    public string mensajeEnemigo = "¡No podrás limpiar este bosque tan fácilmente!";
    public float velocidadTexto = 0.05f;   // Segundos por letra
    public float tiempoEsperaAntesDeEmpezar = 2f; // Tiempo que el panel se queda al terminar

    private bool inicializando = true;
    private bool introActiva = true;
    private bool juegoTerminado = false;

    void Start()
    {
        introActiva = true;
        inicializando = true;

        GenerarTablero();

        DesordenarTableroInicial();

        textoContador.gameObject.SetActive(false);
        StartCoroutine(SecuenciaIntro());
    }

    void DesordenarTableroInicial()
    {
        //Desordenar
        PulsarColumna(1);
        PulsarFila(2);
    }

    IEnumerator SecuenciaIntro()
    {
        // 1. Mostrar el panel de intro
        panelIntro.alpha = 1f;
        panelIntro.blocksRaycasts = true; // Impedir pulsar botones

        // 2. Efecto de texto gradual
        textoDialogo.text = ""; // Empezar sin na
        foreach (char letra in mensajeEnemigo.ToCharArray())
        {
            textoDialogo.text += letra; // Añadimos una letra
            yield return new WaitForSeconds(velocidadTexto); // Esperar
        }

        // 3. Pausa al terminar
        yield return new WaitForSeconds(tiempoEsperaAntesDeEmpezar);

        // 4. Ocultar el panel de intro
        panelIntro.alpha = 0f;
        panelIntro.blocksRaycasts = false; // Ya se puede pulsar

        introActiva = false;
        inicializando = false;

        textoContador.gameObject.SetActive(true);
        ActualizarInterfaz();
    }

    void RegistrarMovimiento()
    {
        // Si el juego terminó o si el script está preparando el tablero, NO RESTAMOS
        if (juegoTerminado || inicializando) return;

        movimientosRestantes--;
        ActualizarInterfaz();

        if (movimientosRestantes <= 0)
        {
            if (!ComprobarVictoria()) PerderJuego();
        }
    }
    void ActualizarInterfaz()
    {
        textoContador.text = "Movimientos: " + movimientosRestantes.ToString();
    }

    void PerderJuego()
    {
        juegoTerminado = true;
        Debug.Log("¡TE HAS QUEDADO SIN TIEMPO!");
        // Escena derrota
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public Sprite[] colorSprites; //Las 16 a color
    public Sprite[] grisesSprites;   //Las 16 grises

    void GenerarTablero()
    {
        int index = 0;
        for (int fila = 0; fila < 4; fila++)
        {
            for (int col = 0; col < 4; col++)
            {
                GameObject nuevaPieza = Instantiate(tilePrefab, this.transform);
                TileControl script = nuevaPieza.GetComponent<TileControl>();

                script.greySprite = grisesSprites[index];
                script.colorSprite = colorSprites[index];

                tablero[fila, col] = script;
                estados[fila, col] = false;
                script.UpdateVisual(false);

                index++;
            }
        }
    }

    //Botones
    //Columnas
    public void PulsarColumna(int c)
    {
        if (juegoTerminado) return;
        if (introActiva && !inicializando) return;

        for (int f = 0; f < 4; f++)
        {
            estados[f, c] = !estados[f, c];
            tablero[f, c].UpdateVisual(estados[f, c]);
        }

        RegistrarMovimiento();
        ComprobarVictoria();
    }
    //Filas
    public void PulsarFila(int f)
    {
        if (juegoTerminado) return;
        if (introActiva && !inicializando) return;

        for (int c = 0; c < 4; c++)
        {
            estados[f, c] = !estados[f, c];
            tablero[f, c].UpdateVisual(estados[f, c]);
        }

        RegistrarMovimiento();
        ComprobarVictoria();
    }

    bool ComprobarVictoria()
    {
        foreach (bool esColor in estados)
        {
            if (!esColor) return false;
        }

        juegoTerminado = true;
        Debug.Log("¡HAS GANADO!");
        return true;
    }
}