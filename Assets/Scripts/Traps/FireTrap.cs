using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireTrap : MonoBehaviour
{
    [SerializeField] private float damage;

    [Header("FireTrap Timers")]
    [SerializeField] private float activationDelay;
    [SerializeField] private float activeTime;
    private Animator fireTrapAnimation;
    private SpriteRenderer spriteRenderer;

    private bool triggered;
    private bool active;

    private Health playerHealth;

    private void Awake()
    {
        fireTrapAnimation = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    private void Update()
    {
        if(playerHealth != null && active)
        {
            playerHealth.TakeDamage(damage);
        }        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            playerHealth = collision.GetComponent<Health>();

            if (!triggered)
                StartCoroutine(ActivateFireTrap());
            if (active)
                collision.GetComponent<Health>().TakeDamage(damage);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            playerHealth = null;
        }
        


    }
    private IEnumerator ActivateFireTrap()
    {
        //uruchamianie pu³apki, zmiana koloru na czerwony dla czytelnoœci
        triggered = true;
        spriteRenderer.color = Color.red;
        yield return new WaitForSeconds(activationDelay);
        //zmiana koloru na oryginalny i aktywacja pu³apki
        spriteRenderer.color = Color.white;
        active = true;
        fireTrapAnimation.SetBool("active", true);
        yield return new WaitForSeconds(activeTime);
        //deaktywacja pu³aplki
        active = false;
        triggered = false;
        fireTrapAnimation.SetBool("active", false);
    }
}
