using UnityEngine;

public class Trash : MonoBehaviour
{
    void Start()
    {
        if (GameManager.instance != null)
        {
            GameManager.instance.RegistrarBasura();
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (GameManager.instance != null)
            {
                GameManager.instance.RecogerBasura();
            }

            Destroy(gameObject);
        }
    }
}