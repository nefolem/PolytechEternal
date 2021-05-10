using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMove : MonoBehaviour
{
    // [HideInInspector] скрывает поле от юнити,
    // но при этом оно остается публичным
    [HideInInspector] public bool followPlayer, attackPlayer = false; // способность следовать/атаковать игрока

    public float attackRadius = 2f; // радиус на котором враг сможет атаковать
    private float chaseAfterAttack = 1f; // гонится за игроком после того, как ударил

    private float currentAttackTime; // время между атаками
    private float defaultAttackTime = 2f;  

    //передвижение
    public int speed = 6; // нид фор спид
    public float followRadius = 10f; // радиус на котором враг начнет следовать
    
    // [SerializeField] делает поле видимым в юнити,
    // но при этом оно остается приватным.
    // Так делать круто и правильно, но дрочено, 
    // поэтому мы можем просто делать поля public
    [SerializeField] Transform playerTransform; 
    [SerializeField] Animator enemyAnim; 

    private PlayerAttack hitPlayer; // переменная для обращения к скрипту атаки
    private Rigidbody rb;

    // компонент Sprite Renderer, в котором есть флажок 
    // для поворота спрайта по оси х, это нам и нужно
    SpriteRenderer enemySR; 


    void Awake()
    {
        // находит местоположение объекта с тэгом "Player"
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        // берем компоненты
        enemySR = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody>();
        hitPlayer = GetComponent<PlayerAttack>();
        enemyAnim = GetComponent<Animator>();
    }

    void Start() {
        followPlayer = true;
        currentAttackTime = defaultAttackTime;
        
    }

    void Update()
    {
        Attack();
    }

    // не спрашивай почему одно в апдейт, а другое в фиксед апдейт, так надо
    void FixedUpdate()
    {
        FollowTarget();
    }

    void FollowTarget()
    {
        if(!followPlayer)
            return;
        
        // если дистанция между врагом и игроком больше, чем радиус атаки врага
        if(Vector3.Distance(transform.position, playerTransform.position) > attackRadius)
        {
            // тут танцы с бубнами для следования за игроком по оси z
            if (playerTransform.position.z < transform.position.z) // если ниже
                this.transform.position += new Vector3(0f, 0f, -speed * Time.deltaTime);

            else if (playerTransform.position.z > transform.position.z) // если выше
                this.transform.position += new Vector3(0f, 0f, speed * Time.deltaTime);

            // тут танцы с бубнами для следования за игроком по оси x
            if (playerTransform.position.x < transform.position.x) // если слева
            {
                enemySR.flipX = true; // поворачиваем спрайт
                this.transform.position += new Vector3(-speed * Time.deltaTime, 0f, 0f);
            }
            else if (playerTransform.position.x > transform.position.x) // если справа
            {
                enemySR.flipX = false; // разворачиваем спрайт
                this.transform.position += new Vector3(speed * Time.deltaTime, 0f, 0f);
            }

            if(rb.velocity.sqrMagnitude != 0) // если враг сдвинулся с мертвой точки
            {
                enemyAnim.SetBool("Walking", true); // подрубаем анимацию движения
            }
        }
        // если дистанция между врагом и игроком меньше, чем радиус атаки врага
        else if(Vector3.Distance(transform.position, playerTransform.position) <= attackRadius)
        {
            rb.velocity = Vector3.zero; // останавливаем врага
            enemyAnim.SetBool("Walking", false); // отрубаем анимацию движения
            attackPlayer = true; // даем разрешение атаковать
        }
    }

    void Attack()
    {
        // если не атакуем, то выход из функции
        if(!attackPlayer) 
            return;
        
        // тайм.дельтатайм - это что-то типа времени, за которое прошел последний кадр
        // чтобы при разных фпс все движения были плавные,
        // тут к времени атаки прибавляется это значение
         currentAttackTime += Time.deltaTime;

        if(currentAttackTime > defaultAttackTime)
        {
            enemyAnim.SetBool("Attack", true); // анимация атаки
            hitPlayer.Hit(); // обращаемся к методу удара
            currentAttackTime = 0f;
            
        }
        // если расстояние между врагом и игроком больше, 
        // чем радиус атаки, то атака прекращается и включается преследование.
        // Прибавление к радиусу атаки длины преследования после атаки
        // дает игроку немного времени до того, как враг начнет его преследовать
        if(Vector3.Distance(transform.position, playerTransform.position) > 
                            attackRadius + chaseAfterAttack)
        {
            enemyAnim.SetBool("Attack", false); // отрубаем анимацию атаки
            attackPlayer = false; // запрещаем атаковать
            followPlayer = true; // разрешаем следовать
        }

    }

    

    
    
}
