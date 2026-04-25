using UnityEngine;

public class Trash : MonoBehaviour
{
    void Start()
    {
        // Al aparecer, le avisa al GameManager de que existe
        GameManager.instance.RegistrarBasura();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // El jugador la recoge
            GameManager.instance.RecogerBasura();
            Destroy(gameObject); // Desaparece la basura

            // Opcional: Sonido o efecto de partículas
        }
    }
}