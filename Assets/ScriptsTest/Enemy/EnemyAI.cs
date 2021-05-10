using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    private string currentState = "IdleState";
    private Transform target;

    public float chaseRange = 5;
    public float attackRange = 2;
    public float speed = 3;

    public int health;
    public int maxHealth;

    public Animator enemyAnim;

    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
        health = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerManager.gameOver)
        {
            enemyAnim.enabled = false;
            this.enabled = false;
        }

        float distance = Vector3.Distance(transform.position, target.position);

        if(currentState == "IdleState")
        {
            if (distance < chaseRange)
                currentState = "ChaseState";
        }
        else if(currentState == "ChaseState")
        {
            //play the run animation
            enemyAnim.SetTrigger("Walking");
            enemyAnim.SetBool("Attack", false);

            if(distance < attackRange)
                currentState = "AttackState";

            //move towards the player
            if(target.position.x > transform.position.x)
            {
                //move right
                transform.Translate(transform.right * speed * Time.deltaTime);
                transform.rotation = Quaternion.Euler(0, 180, 0);
            }
            else
            {
                //move left
                transform.Translate(-transform.right * speed * Time.deltaTime);
                transform.rotation = Quaternion.identity;
            }

        }
        else if(currentState == "AttackState")
        {
            enemyAnim.SetBool("Attack", true);

            if (distance > attackRange)
                currentState = "ChaseState";
        }
    }

    public void TakeDamage(int damage)
    {
        health -= damage;

        enemyAnim.SetTrigger("Hurt");

        if(health <= 0)
        {
            Die();
            GameController.score += 10;
        }
    }

    void Die()
    {
        //Debug.Log("Enemy died");


        enemyAnim.SetBool("IsDead", true);
        
        GetComponent<Rigidbody>().useGravity = true;
        GetComponent<Collider>().enabled = false;
        Destroy (gameObject, 3);
        
        this.enabled = false;
        

    }

}
