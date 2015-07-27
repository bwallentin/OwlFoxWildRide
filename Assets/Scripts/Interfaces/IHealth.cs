public interface IHealth<T>
{
    bool IsDead { get; }
    bool Damaged { get; }

    void TakeDamage(int damageTaken);
    void Die();
}
