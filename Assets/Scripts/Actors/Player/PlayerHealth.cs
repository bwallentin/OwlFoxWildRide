using UnityEngine;
using System.Collections;

public class PlayerHealth : Actor
{
    private Animator animator;
    private PlayerMovement playerMovement;

    public override void Awake()
    {
        base.Awake();
        animator = GetComponent<Animator>();
        playerMovement = GetComponent<PlayerMovement>();
    }

    void Update()
    {
        UpdateHealthBarPosition();

        if (damaged)
        {
            // TODO: play damaged animation
        }

        damaged = false;
    }

    public void TakeDamage(int amount)
    {
        damaged = true;

        currentHealth -= amount;
        healthSlider.value = currentHealth / (float)startingHealth;

        if (currentHealth <= 0 && !isDead)
        {
            Die();
        }
    }

    protected override void Die()
    {
        isDead = true;

        animator.SetTrigger("Die");

        // Stop moving
        playerMovement.Stop();

        // disable movement when dead
        playerMovement.enabled = false;
    }
}
