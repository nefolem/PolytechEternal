using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthScript : MonoBehaviour
{
    public int health = 10;
    [HideInInspector] public int currentHealth;

    private Animator anim;
    private EnemyMove enemyMove;

    [HideInInspector] public bool characterDied;
    public bool isPlayer;

    void Awake() 
    {
        anim = GetComponent<Animator>();
        enemyMove = GetComponent<EnemyMove>();
        
    }
    void Start() 
    {
        currentHealth = health;
    }



    public void TakeDamage(int attackDamage)
    {
        if(characterDied)
            return;
        
        currentHealth -= attackDamage; // отнимаем от текущего хп входящий урон
        Debug.Log("HP = " + health);
        anim.SetTrigger("Hurt"); // триггерим анимацию получения урона

        if(currentHealth <= 0) // если хп меньше или равно нулю
        {
            characterDied = true;
            Die(); // смэрть
            
            if(isPlayer)
            {
                // если это игрок, то деактивируем скрипт врага
                //currentHealth = health;
                Debug.Log("Player Died!");
            }
            else
            {
                Debug.Log("Enemy Died!");
                enemyMove.followPlayer = false; // запрещаем преследовать
                GameController.score += 10; // прибавляем игроку по 10 очков за каждого убитого
            }
        }
    }
    void Die()
    {
        anim.SetBool("IsDead", true); // триггерим анимацию смерти
        
        // убираем флажок гравитаци с компонента Rigidbody, чтобы враг не провалился через пол
        GetComponent<Rigidbody>().useGravity = false; 
        // вырубаем коллайдер, чтобы игрок мог проходить сквозь труп врага
        GetComponent<Collider>().enabled = false; 
        Destroy (this.gameObject, 3); // уничтожаем объект в игре через 3 секунды     
        this.enabled = false; // выключаем его

    }
}
