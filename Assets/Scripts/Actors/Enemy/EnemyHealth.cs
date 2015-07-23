using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : Actor, IHealth<int>
{
    // Determines if we're dead or not
    public bool IsDead { get; private set; }

    // If this is set on a frame, we'll play the damaged animation
    public bool Damaged { get; private set; }

    public int expValue = 10;

    private Animator animator;
    private CapsuleCollider capsuleCollider;
    private IDetectCombat detectCombat;

    public override void Awake()
    {
        base.Awake();
        animator = GetComponent<Animator>();
        capsuleCollider = GetComponent<CapsuleCollider>();
        detectCombat = GetComponent<IDetectCombat>();
    }

    void Update()
    {
        UpdateHealthBarPosition();
    }

    public void TakeDamage(int amount)
    {
        currentHealth -= amount;
        healthSlider.value = currentHealth / (float)startingHealth;

        Debug.Log("Enemy taking " + amount + " damage, current health is " + currentHealth);

        if (currentHealth <= 0 && !IsDead)
        {
            Die();
        }
    }

    public void Die()
    {
        IsDead = true;

        // remove obstacle by setting it to a trigger
        capsuleCollider.isTrigger = true;

        detectCombat.DetectEnemies = false;
   
        animator.SetTrigger("Dead");

        // Destroy game object after 2 sec
        Destroy(gameObject, 2f);
    }
}
