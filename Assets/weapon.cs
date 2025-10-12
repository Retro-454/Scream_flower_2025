using UnityEngine;

public class weapon : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public float damage =10 ;
    void OnTriggerEnter2D(Collider2D collision)
    {
        enemy enemy1 = collision.GetComponent<enemy>();
        if(enemy1 != null){
            enemy1.TakeDamge(damage);
        }
    }
}
