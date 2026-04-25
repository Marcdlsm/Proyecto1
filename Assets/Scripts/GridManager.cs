using UnityEngine;

public class GridManager : MonoBehaviour
{
    public GameObject tilePrefab;
    public int width = 10;
    public int height = 10;

    void Start()
    {
        GenerarGrid();
        AjustarCamara(); // <-- Añade esta línea
    }

    void GenerarGrid()
    {
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                Instantiate(tilePrefab, new Vector2(x, y), Quaternion.identity, transform);
            }
        }
    }

    // --- NUEVO MÉTODO ---
    void AjustarCamara()
    {
        Camera cam = Camera.main; // Busca la Main Camera

        if (cam == null) return;

        // 1. Centrar la cámara
        // Calculamos el centro exacto de la cuadrícula
        float centerX = (width / 2f) - 0.5f;
        float centerY = (height / 2f) - 0.5f;
        // Movemos la cámara allí, manteniendo su Z (normalmente -10)
        cam.transform.position = new Vector3(centerX, centerY, cam.transform.position.z);

        // 2. Ajustar el Zoom (Size orthographic)
        // El 'Size' de la cámara orthográfica es la mitad de la altura de la pantalla.
        // Queremos que quepa el alto del grid o el ancho (lo que sea mayor).
        float targetSize;
        float screenRatio = (float)Screen.width / (float)Screen.height;

        if (screenRatio >= 1)
        { // Pantalla horizontal o cuadrada
            // El alto es el factor limitante
            targetSize = (height / 2f) + 1f; // +1f de margen extra
        }
        else
        { // Pantalla vertical (móvil, etc.)
            // El ancho es el factor limitante
            targetSize = ((width / screenRatio) / 2f) + 1f;
        }

        cam.orthographicSize = targetSize;
    }
}