using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 5f;
    Vector2 direction;

    void Start()
    {
        // Busca al jugador al aparecer y fija la dirección
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            direction = (player.transform.position - transform.position).normalized;
        }
        Destroy(gameObject, 3f); // Se destruye a los 3 segundos para no llenar la memoria
    }

    void Update()
    {
        transform.Translate(direction * speed * Time.deltaTime);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // Lógica de muerte (ej: reiniciar escena)
            UnityEngine.SceneManagement.SceneManager.LoadScene(0);
        }
    }
}