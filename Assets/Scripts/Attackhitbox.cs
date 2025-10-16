using UnityEngine;

public class EnemyAttackHitbox : MonoBehaviour
{
    private int attackDamage;
    private bool canDamage = false;

    public void Activate(int damage)
    {
        attackDamage = damage;
        canDamage = true;
    }

    public void Deactivate()
    {
        canDamage = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!canDamage) return;

        if (collision.CompareTag("Player"))
        {
            player playerScript = collision.GetComponent<player>();
            if (playerScript != null)
            {
                playerScript.TakeDamage(attackDamage);
            }

            // Only hit once per activation
            canDamage = false;
        }
    }
}

