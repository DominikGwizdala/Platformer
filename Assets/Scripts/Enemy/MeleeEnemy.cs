
using Unity.Burst.CompilerServices;
using UnityEngine;

public class MeleeEnemy : MonoBehaviour
{
    [Header("Attack Parametrs")]
    [SerializeField]private float attackCooldown;
    [SerializeField] private float range;
    [SerializeField]private int damage;

    [Header("Collider Parametrs")]
    [SerializeField] private float coliderDistance;
    [SerializeField]private BoxCollider2D boxCollider;

    [Header("Player Layer")]
    [SerializeField] private LayerMask playerLayer;
    private float cooldownTimer = Mathf.Infinity;

    [Header("SFX")]
    [SerializeField] private AudioClip attackSound;

    private Health playerHealth;
    private Animator MeleeAnimation;
    private EnemyPatrol enemyPatrol;

    private void Awake()
    {
        MeleeAnimation = GetComponent<Animator>();
        enemyPatrol = GetComponentInParent<EnemyPatrol>();
    }
    private void Update()
    {
        cooldownTimer += Time.deltaTime;
         if (PlayerInSight()) 
        {
            if (cooldownTimer >= attackCooldown && playerHealth.currentHealth > 0)
            {
                cooldownTimer = 0;
                MeleeAnimation.SetTrigger("meleeAttack");
                SoundManager.instance.PlaySound(attackSound);
            }
        }
        
         if (enemyPatrol !=null)    
            enemyPatrol.enabled = !PlayerInSight(); 
    }

    private bool PlayerInSight() 
    {
        RaycastHit2D hit=Physics2D.BoxCast(boxCollider.bounds.center + transform.right * range * transform.localScale.x * coliderDistance,
           new Vector3(boxCollider.bounds.size.x * range, boxCollider.bounds.size.y,boxCollider.bounds.size.z),
            0,Vector2.left,0,playerLayer);

        if(hit.collider != null)
            playerHealth = hit.transform.GetComponent<Health>();
        return hit.collider !=null;
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(boxCollider.bounds.center + transform.right * range* transform.localScale.x* coliderDistance,
           new Vector3(boxCollider.bounds.size.x * range, boxCollider.bounds.size.y, boxCollider.bounds.size.z));
    }

    private void DamagePlayer()
    {
        if(PlayerInSight())
        {

            playerHealth.TakeDamage(damage);
        }
    }
}
