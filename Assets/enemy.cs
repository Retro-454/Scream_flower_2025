using System.Collections;
using UnityEngine;
using UnityEngine.UIElements;

public class enemy : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public float movespeed= 2f;
    Rigidbody2D rb;
    Transform target;
    Vector2 moveDirection;
    public float health = 50;
    public float maxHealth = 50;
    public int attackDamage = 15;
     private SpriteRenderer sprite;
    public Color flashColor = Color.red;
    public float flashDuration = 0.1f;
    private Color originalColor;
    public GameObject Vase;
    private void Awake()
    {
      rb = GetComponent<Rigidbody2D>();   
      sprite = GetComponent<SpriteRenderer>();
    }

 

    void Start()
    {
    target = GameObject.FindGameObjectWithTag("Player").transform;
    health= maxHealth;
    if(sprite != null){
        originalColor = sprite.color;
    }

    }

    // Update is called once per frame
    void Update()
    {
        if(target){
            Vector3 direction = (target.position - transform.position).normalized;
            moveDirection = direction;
        }

    }
    private void FixedUpdate()
    {
        rb.linearVelocity= new Vector2(moveDirection.x,moveDirection.y)*movespeed;
    }
    public void TakeDamge(float damage){
        health -= damage;
        StartCoroutine(Flash());

        if(health <=0){
            DropItem();
            Destroy(gameObject);
        }
    }
    private void DropItem(){
        if(Vase != null){
            Instantiate(Vase, transform.position, Quaternion.identity);
        }
    }
    private IEnumerator Flash(){
        if(sprite != null){
            sprite.color  = flashColor;
            yield return new WaitForSeconds(flashDuration);
            sprite.color = originalColor;
        }
    }
    private void OnCollisionEnter2D(Collision2D collision){
    if (collision.gameObject.CompareTag("Player")){
        player playerScript = collision.gameObject.GetComponent<player>();
        if (playerScript != null){
            playerScript.TakeDamage(attackDamage);
        }
    }
}


}
    
