using UnityEngine;

public class EnemyAttackHitbox : MonoBehaviour
{
    private int attackDamage =1;
    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.CompareTag("Player"))
        {
            player player1 = collision.GetComponent<player>();
            if (player1 != null)
            {
                player1.TakeDamage(attackDamage);
            }

            // Only hit once per activation
    
        }
    }
}

