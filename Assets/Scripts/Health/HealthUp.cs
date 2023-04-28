using UnityEngine;

public class HealthUp : MonoBehaviour
{
    [SerializeField] private float cure;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
           
            collision.GetComponent<Health>().HpUp(cure);
            gameObject.SetActive(false); 
        }

    }
}

