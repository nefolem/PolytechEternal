using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileMovement : MonoBehaviour
{
    
    public float speed = 5f; 
    public Rigidbody rb;
    public GameObject damageEffect; // эффект попадания
    //public GameObject fireEffect;
    //public GameObject firePoint;
    public int attackDamage = 1;
    
    void Start()
    {
        //движение стрелы с заданной скоростью + 
        //поворот в противоположную сторону в зависимости от поворота игрока
        rb.velocity = speed * transform.right; 
    }
  
    // когда соприкасается с другим объектом
    private void OnTriggerEnter(Collider other) 
    {
        if(other.tag == "Enemy") // если тэг объекта "Enemy"
        {
            //создаем эффект попадания
            Instantiate(damageEffect, transform.position, damageEffect.transform.rotation);
            
            //Debug.Log(other.name); // в консоль пишется имя объекта, в который попали

            // берем скрипт объекта, в который попали, 
            // и обращаемся к его методу получения урона
            other.GetComponent<HealthScript>().TakeDamage(attackDamage);
          
            Destroy(gameObject); // уничтожаем сигу при попадании
        }
        // если ни во что не попадает, то уничтожается через 5 сек
        else Destroy(gameObject, 5);
    }
   
}
