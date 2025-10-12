using UnityEngine;

public class Person : MonoBehaviour
{
    public Person() { }
    public int takeDamage(int damage, int defense)
    {
        int damageTaken = damage - defense;
        if (damageTaken > 0)
        { 
            return damageTaken;
        }
        else
        {
            return 0;
        }
    }

    public int healDamage(int health, int maxHealth, int recoveryPoints)
    {
        int healthRecovered = health + recoveryPoints;

        if (healthRecovered > maxHealth)
        {
            healthRecovered = maxHealth;
        }
        return healthRecovered;

    }

}
