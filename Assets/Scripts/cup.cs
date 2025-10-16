using UnityEngine;

public class cup : MonoBehaviour
{

    public int recoveryPoints = 20;

    public cup() { }

    public int Heal(int health, int maxHealth, Person person)
    {
        int new_health = person.healDamage(health, maxHealth, recoveryPoints);
        gameObject.SetActive(false);
        return new_health;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            player player = other.GetComponent<player>();

            if (player != null)
            {
                player.health = player.p.healDamage(player.health, player.maxHealth, recoveryPoints);
                Debug.Log("Curent health" + player.health);
                Destroy(gameObject);
            }

        }
    }

}
