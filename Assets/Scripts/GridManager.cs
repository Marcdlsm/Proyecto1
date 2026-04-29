using UnityEngine;

public class GridManager : MonoBehaviour
{
    public GameObject tilePrefab;
    public GameObject trashPrefab;
    public SpriteRenderer fondoImagen;

    [Range(0f, 1f)] public float probabilidadBasura = 0.2f;

    [HideInInspector] public int width;
    [HideInInspector] public int height;

    void Start()
    {
        AdaptarGridAlFondo();
        GenerarGrid();
        AjustarCamara();
    }

    void AdaptarGridAlFondo()
    {
        if (fondoImagen == null) return;

        float fondoWidth = fondoImagen.bounds.size.x;
        float fondoHeight = fondoImagen.bounds.size.y;

        width = Mathf.RoundToInt(fondoWidth);
        height = Mathf.RoundToInt(fondoHeight);

        Vector3 esquinaInferiorIzquierda = new Vector3(
            fondoImagen.bounds.min.x + 0.5f,
            fondoImagen.bounds.min.y + 0.5f,
            0
        );

        transform.position = esquinaInferiorIzquierda;
    }

    void GenerarGrid()
    {
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
                }
            }
        }
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