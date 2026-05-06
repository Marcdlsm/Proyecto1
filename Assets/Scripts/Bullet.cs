using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float velocidadBala = 5f;
    private Vector2 direccion;

    void Start()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            direccion = (player.transform.position - transform.position).normalized;
        }
        Destroy(gameObject, 4f); // Se destruye si no da a nada
    }

    void Update()
    {
        transform.Translate(direccion * velocidadBala * Time.deltaTime);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // Llamamos a la función de recibir daño del jugador
            other.GetComponent<PlayerHealth>().RecibirDaño(1);
            Destroy(gameObject);
        }
    }
}