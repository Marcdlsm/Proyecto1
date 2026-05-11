using UnityEngine;

public class GridManager : MonoBehaviour
{
    public GameObject tilePrefab;
    public GameObject trashPrefab;
    public SpriteRenderer fondoImagen;

    [Range(0f, 1f)] public float probabilidadBasura = 0.2f;

    [HideInInspector] public int width;
    [HideInInspector] public int height;
    [HideInInspector] public int totalCasillas;

    void Start()
    {
        AdaptarGridAlFondo();
        GenerarGrid();
        AjustarCamara();
        GenerarLimites();
    }

    void AdaptarGridAlFondo()
    {
        if (fondoImagen == null) return;

        float fondoWidth = fondoImagen.bounds.size.x;
        float fondoHeight = fondoImagen.bounds.size.y;

        width = Mathf.RoundToInt(fondoWidth);
        height = Mathf.RoundToInt(fondoHeight);
        totalCasillas = width * height;

        Vector3 esquinaInferiorIzquierda = new Vector3(
            fondoImagen.bounds.min.x + 0.5f,
            fondoImagen.bounds.min.y + 0.5f,
            0
        );

        transform.position = esquinaInferiorIzquierda;
    }

    void GenerarGrid()
    {
        if (GameManager.instance != null)
        {
            GameManager.instance.basuraTotal = 0;
            GameManager.instance.basuraRecogida = 0;
        }

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                Vector3 pos = transform.position + new Vector3(x, y, 0);
                GameObject nuevaCasilla = Instantiate(tilePrefab, pos, Quaternion.identity, transform);

                if (trashPrefab != null && Random.value < probabilidadBasura)
                {
                    Vector3 posBasura = new Vector3(pos.x, pos.y, -0.5f);
                    Instantiate(trashPrefab, posBasura, Quaternion.identity, nuevaCasilla.transform);

                    if (GameManager.instance != null) GameManager.instance.RegistrarBasura();
                }
            }
        }
    }

    void GenerarLimites()
    {
        if (fondoImagen == null) return;

        float ancho = fondoImagen.bounds.size.x;
        float alto = fondoImagen.bounds.size.y;
        Vector3 centro = fondoImagen.bounds.center;

        GameObject contenedorMuros = new GameObject("LimitesMapa");

        void CrearMuro(string nombre, Vector3 posicion, Vector2 tamaño)
        {
            GameObject muro = new GameObject(nombre);
            muro.transform.parent = contenedorMuros.transform;
            muro.transform.position = posicion;
            BoxCollider2D collider = muro.AddComponent<BoxCollider2D>();
            collider.size = tamaño;
        }

        float grosor = 1f;
        CrearMuro("Muro_Arriba", centro + new Vector3(0, alto / 2 + grosor / 2, 0), new Vector2(ancho + (grosor * 2), grosor));
        CrearMuro("Muro_Abajo", centro - new Vector3(0, alto / 2 + grosor / 2, 0), new Vector2(ancho + (grosor * 2), grosor));
        CrearMuro("Muro_Izquierda", centro - new Vector3(ancho / 2 + grosor / 2, 0, 0), new Vector2(grosor, alto));
        CrearMuro("Muro_Derecha", centro + new Vector3(ancho / 2 + grosor / 2, 0, 0), new Vector2(grosor, alto));
    }

    void AjustarCamara()
    {
        Camera cam = Camera.main;
        if (cam == null || fondoImagen == null) return;

        cam.transform.position = new Vector3(
            fondoImagen.transform.position.x,
            fondoImagen.transform.position.y,
            cam.transform.position.z
        );

        float screenRatio = (float)Screen.width / (float)Screen.height;
        float targetHeight = height / 2f;
        float targetWidth = width / screenRatio / 2f;

        cam.orthographicSize = Mathf.Max(targetHeight, targetWidth) + 1f;
    }
}