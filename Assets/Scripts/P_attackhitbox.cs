using UnityEngine;

public class P_attackhitbox : MonoBehaviour
{
    public int attackDamage =1;
    private void OnTriggerEnter2D(Collider2D collision)
    {

    
            enemy foe = collision.GetComponent<enemy>();
            if (foe != null)
        {
                
                foe.TakeDamage(attackDamage);
            }

            // Only hit once per activation
    
        
    }
}
