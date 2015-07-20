using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : Actor
{
    public int expValue = 10;

    private Animator animator;
    private ParticleSystem hitParticles;
    private CapsuleCollider capsuleCollider;

    public override void Awake()
    {
        base.Awake();
        animator = GetComponent<Animator>();
        hitParticles = GetComponentInChildren<ParticleSystem>();
        capsuleCollider = GetComponent<CapsuleCollider>();
    }

    void Update()
    {
        UpdateHealthBarPosition();
    }

    public void TakeDamage(int amount)
    {
        if (isDead)
        {
            return;
        }

        currentHealth -= amount;
        healthSlider.value = currentHealth;

        hitParticles.Play();

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    protected override void Die()
    {
        isDead = true;

        // remove obstacle by setting it to a trigger
        capsuleCollider.isTrigger = true;

        animator.SetTrigger("Dead");

        // Destroy game object after 2 sec
        Destroy(gameObject, 2f);
    }
}
