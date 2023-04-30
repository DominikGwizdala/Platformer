using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{
   [Header("Patrol Points")]
   [SerializeField] private Transform leftEdge;
   [SerializeField] private Transform rightEdge;
   [Header("Enemy")]
   [SerializeField] private Transform Enemy;
   [Header("Movments")]
   [SerializeField] private float speed;
    private Vector3 initScale;
    private bool movingleft;
    [Header("Idle behaviour")]
    [SerializeField] private float idleDuration;
    private float idleTimer;

    [Header("Animations")]
   [SerializeField] private Animator runninganimation;


    private void Awake()
    {
        initScale = Enemy.localScale;
        
    }
    private void OnDisable()
    {
        runninganimation.SetBool("moving", false);
    }
    private void Update()
    {
       
        if (movingleft)
        {
            if (Enemy.position.x >= leftEdge.position.x)
            {
                MoveInDirection(-1);
            }else
            {
                DirectionChange();
            }
            
        }
        else
        {
            if (Enemy.position.x <= rightEdge.position.x)
            {
                MoveInDirection(1);
            }
            else
            {
                DirectionChange();
            }
           
          
        }
        
    }

    private void DirectionChange()
    {
        runninganimation.SetBool("moving",false);

        idleTimer += Time.deltaTime;    

        if(idleTimer > idleDuration) 
        {
            movingleft = !movingleft;
        }
             
    }
    private void MoveInDirection(int _direction)
    {
        idleTimer = 0;
        runninganimation.SetBool("moving", true);
        //Kierunek wroga
        Enemy.localScale = new Vector3(Mathf.Abs(initScale.x )* _direction, initScale.y,initScale.z);
        //Poruszanie sie
        Enemy.position= new Vector3(Enemy.position.x + Time.deltaTime*_direction*speed,Enemy.position.y,Enemy.position.z); 


    }
}
