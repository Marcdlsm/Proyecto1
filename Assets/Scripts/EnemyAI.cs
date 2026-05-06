using UnityEngine;

public partial class EnemyAI : MonoBehaviour
{
    public float velocidad = 3f;
    public float rangoMovimiento = 4f;
    public GameObject balaPrefab;
    public float cadenciaDisparo = 2f;

    private Vector2 puntoDestino;
    private float tiempoSiguienteDisparo;

    void Start()
    {
        ActualizarPuntoDestino();
    }

    void Update()
    {
        // Movimiento
        transform.position = Vector2.MoveTowards(transform.position, puntoDestino, velocidad * Time.deltaTime);

        if (Vector2.Distance(transform.position, puntoDestino) < 0.2f)
        {
            ActualizarPuntoDestino();
        }

        // Disparo
        if (Time.time > tiempoSiguienteDisparo)
        {
            Disparar();
            tiempoSiguienteDisparo = Time.time + cadenciaDisparo;
        }
    }

    void ActualizarPuntoDestino()
    {
        puntoDestino = new Vector2(Random.Range(-rangoMovimiento, rangoMovimiento), Random.Range(-rangoMovimiento, rangoMovimiento));
    }

    void Disparar()
    {
        Instantiate(balaPrefab, transform.position, Quaternion.identity);
    }
}