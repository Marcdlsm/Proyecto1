using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public float speed = 3f;
    public Transform player;
    public GameObject bulletPrefab;
    public float fireRate = 2f;

    void Start()
    {
        InvokeRepeating("Shoot", 1f, fireRate);
    }

    void Update()
    {
        // Moverse hacie el jug
        transform.position = Vector2.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
    }

    void Shoot()
    {
        Instantiate(bulletPrefab, transform.position, Quaternion.identity);
    }
}