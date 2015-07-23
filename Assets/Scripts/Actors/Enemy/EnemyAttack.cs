using UnityEngine;
using System.Collections;

public class EnemyAttack : MonoBehaviour
{
    public int attackDamage = 10;
    public float timeBetweenAttacks = 0.5f;

    //private Animator animator;
    private GameObject player;
    private PlayerHealth playerHealth;
    //EnemyHealth enemyHealth;
    private bool playerInRange;
    private float timer;


    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerHealth = player.GetComponent<PlayerHealth>();
        //enemyHealth = GetComponent<EnemyHealth>();
        //animator = GetComponent<Animator>();
    }


    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == player)
        {
            playerInRange = true;
        }
    }


    void OnTriggerExit(Collider other)
    {
        if (other.gameObject == player)
        {
            playerInRange = false;
        }
    }


    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= timeBetweenAttacks && playerInRange/*&& enemyHealth.currentHealth > 0*/)
        {
            Attack();
        }

        /*if (playerHealth.currentHealth <= 0)
        {
            anim.SetTrigger("PlayerDead");
        }*/
    }


    void Attack()
    {
        timer = 0f;

        if (playerHealth.currentHealth > 0)
        {
            playerHealth.TakeDamage(attackDamage);
        }
    }
}
