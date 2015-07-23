using UnityEngine;
using System.Collections;

public class PlayerHealth : Actor, IHealth<int>
{
    // Determines if we're dead or not
    public bool IsDead { get; private set; }

    // If this is set on a frame, we'll play the damaged animation
    public bool Damaged { get; private set; }

    // Responsible for all the animations
    private Animator animator;

    // Reference to the players movement
    private IMovable playerMovement;

    public override void Awake()
    {
        base.Awake();
        animator = GetComponent<Animator>();
        playerMovement = GetComponent<IMovable>();
    }

    void Update()
    {
        UpdateHealthBarPosition();

        if (Damaged)
        {
            // TODO: play damaged animation
        }

        Damaged = false;
    }

    public void IncreaseHealth(int amount)
    {
        currentHealth += amount;
        healthSlider.value = currentHealth / (float)startingHealth;
    }

    public void TakeDamage(int amount)
    {
        Damaged = true;

        currentHealth -= amount;
        healthSlider.value = currentHealth / (float)startingHealth;

        Debug.Log("Player taking " + amount + " damage, current health is " + currentHealth);

        if (currentHealth <= 0 && !IsDead)
        {
            Die();
        }
    }

    public void Die()
    {
        IsDead = true;

        animator.SetTrigger("Die");

        // disable movement when dead
        playerMovement.IsMovable(false);
    }
}
