using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public Transform projectilePoint;
    public GameObject projectile;
    public Transform hitPosition;
    public LayerMask otherLayer;
    
    private HealthScript healthScript;
    public float attackRange = 10f;   
    public int attackDamage = 1;

    private Animator anim;

    void Start() 
    {
        anim = GetComponent<Animator>();
        healthScript = GetComponent<HealthScript>();
    }

    void Update()
    {
        if(healthScript.isPlayer == true)
        {
            if(Input.GetKeyDown(KeyCode.X))
            {
                Shoot();
            }

            if(Input.GetKeyDown(KeyCode.Z))
            {
                Hit();
            }
        }
    }
    
    void Shoot()
    {
        Instantiate(projectile, projectilePoint.position, projectilePoint.rotation);
        anim.SetTrigger("Shoot");
    }

    public void Hit()
    {     
        anim.SetTrigger("Attack");
        // массив всех коллайдеров объектов, 
        // которые попали в радиус атаки,
        // у которых соответствубщий otherLayers слой  
        Collider[] hit = Physics.OverlapSphere(hitPosition.position, attackRange, otherLayer);

        foreach (Collider other in hit) // пиздим их всех
        {
            // берем скрипт объекта, в который попали, 
            // и обращаемся к его методу получения урона
           other.GetComponent<HealthScript>().TakeDamage(attackDamage);
        }
        //hitCollider.enabled = false;

    }
 

}
