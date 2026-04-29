using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class Tile : MonoBehaviour
{
    // --- 1. DECLARACIÓN DE VARIABLES (Esto es lo que te falta) ---
    public Color colorGrisInicial = Color.gray;
    public Color colorFaseColorear = new Color(0.7f, 0.7f, 0.7f);
    public Color colorActivado = Color.cyan;
    public Color colorLineas = Color.black;

    private SpriteRenderer spriteRenderer;
    private LineRenderer lineRenderer;
    private bool estaPisada = false;
    private bool puedoColorear = false;

    // --- 2. CONFIGURACIÓN INICIAL ---
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        lineRenderer = GetComponent<LineRenderer>();

        // Color inicial: Muy transparente (casi invisible)
        spriteRenderer.color = new Color(0, 0, 0, 0.1f);

        ConfigurarLineas();
    }

    public void PrepararParaColorear()
    {
        puedoColorear = true;
        // Cambiamos a un gris suave semitransparente para indicar que se puede pisar
        if (!estaPisada)
        {
            spriteRenderer.color = new Color(1, 1, 1, 0.2f);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && puedoColorear && !estaPisada)
        {
            estaPisada = true;
            // Color de victoria: Por ejemplo, un azul cian semitransparente
            spriteRenderer.color = new Color(0, 1, 1, 0.4f);
        }
    }

    void ConfigurarLineas()
    {
        // Aquí va el código que hicimos antes para dibujar el cuadrado negro
        lineRenderer.startColor = colorLineas;
        lineRenderer.endColor = colorLineas;
        lineRenderer.useWorldSpace = false;
        lineRenderer.positionCount = 4;
        float offset = 0.5f;
        lineRenderer.SetPosition(0, new Vector3(-offset, -offset, -0.1f));
        lineRenderer.SetPosition(1, new Vector3(offset, -offset, -0.1f));
        lineRenderer.SetPosition(2, new Vector3(offset, offset, -0.1f));
        lineRenderer.SetPosition(3, new Vector3(-offset, offset, -0.1f));
        lineRenderer.loop = true;
    }
}