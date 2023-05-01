using UnityEngine;

public class HealthUp : MonoBehaviour
{
    [SerializeField] private float cure;
    [Header("SFX")]
    [SerializeField] private AudioClip pickUpSound;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            SoundManager.instance.PlaySound(pickUpSound);
            collision.GetComponent<Health>().HpUp(cure);
            gameObject.SetActive(false); 
        }

    }
}

