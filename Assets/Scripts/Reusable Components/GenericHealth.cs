using UnityEngine;

public class GenericHealth : MonoBehaviour
{
    public IntValue maxHealth;
    [SerializeField] protected int currentHealth;


    public virtual void Heal(int amount)
    {
        currentHealth += amount;

        if (currentHealth > maxHealth.runTimeValue)
        {
            currentHealth = maxHealth.runTimeValue;
        }
    }


    public virtual void FullHeal()
    {
        currentHealth = maxHealth.runTimeValue;
    }


    public virtual void Damage(int amount)
    {
        currentHealth -= amount;

        if (currentHealth < 0)
        {
            currentHealth = 0;
        }
    }


    public virtual void Die()
    {
        currentHealth = 0;
    }


    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth.runTimeValue;
    }
}
