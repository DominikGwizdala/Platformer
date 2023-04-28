using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float damage;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            print("This is a print message");
            collision.GetComponent<Health>().TakeDamage(damage);
        }

    }
}
