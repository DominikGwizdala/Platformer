using UnityEngine;
using System.Collections;

public class Health : MonoBehaviour
{
    [Header ("Health")]    
    [SerializeField] private float startingHealth;
    public float currentHealth { get; private set; }
    private Animator anim;
    private bool dead;

    [Header("iFrames")]
    [SerializeField] private float iFramesDuration;
    [SerializeField] private int   numbersOfFlashes;
    private SpriteRenderer spriteRend;

    [Header("Components")]
    [SerializeField] private Behaviour[] components;

    [Header("SFX")]
    [SerializeField] private AudioClip hitSound;
    [SerializeField] private AudioClip deathSound;

    private bool invulnerble;
    private void Awake() 
    {
        currentHealth = startingHealth;
        anim = GetComponent<Animator>();
        spriteRend = GetComponent<SpriteRenderer>();
    }
    public void TakeDamage(float _damage)
    {
        if (invulnerble) return;

        currentHealth = Mathf.Clamp(currentHealth - _damage, 0, startingHealth);

        if (currentHealth > 0)
        {
            anim.SetTrigger("hurt");
            StartCoroutine(Invunerability());
            SoundManager.instance.PlaySound(hitSound);
        }
        else
        {
            if(!dead)
            {
                anim.SetTrigger("die");
               

                foreach (Behaviour component in components)
                    component.enabled = false;

                dead = true;
                SoundManager.instance.PlaySound(deathSound);
            }
            

        }
    }
   public void HpUp(float _health)
    {
        currentHealth = Mathf.Clamp(currentHealth + _health, 0, startingHealth);

    }

    private IEnumerator Invunerability()
    {
        invulnerble = true;
        Physics2D.IgnoreLayerCollision(10,11,true);
        for (int i = 0; i < numbersOfFlashes; i++)
        {
            spriteRend.color = new Color(1, 1, 0, 0.5f);
            yield return new WaitForSeconds(iFramesDuration/(numbersOfFlashes * 2));
            spriteRend.color = Color.white;
            yield return new WaitForSeconds(iFramesDuration / (numbersOfFlashes * 2));
        }
        Physics2D.IgnoreLayerCollision(10, 11,false);
        invulnerble = false;
    }
    private void OnDestroy()
    {
        gameObject.SetActive(false);
    }

}
