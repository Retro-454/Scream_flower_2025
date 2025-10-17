using System.Collections;

using UnityEngine;
using UnityEngine.InputSystem;
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

    //
    public float attRange = 0.1f;
    public float attCool = 2f;
    private bool isAttacking = false;
    public EnemyAttackHitbox attackHitbox;
    public float attduration = 0.5f;
    private Vector3 originalScale;

    public WaveSpawner spawner;

    //stun and knockback
    public float stunDuration = 0.3f;
     private bool isStunned = false;
    private int hitcount = 0;



    private void Awake()
    {
      rb = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
      originalScale = transform.localScale; 
    }



    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
        health = maxHealth;
        if (sprite != null)
        {
            originalColor = sprite.color;
        }

    }


    // Update is called once per frame
   void Update()
{
    if (target == null || isStunned) return;

    FlipTowardsPlayer();
    float distance = Vector2.Distance(transform.position, target.position);

    if (!isAttacking)
    {
        if (distance > attRange)
        {
            Vector3 direction = (target.position - transform.position).normalized;

            // 🧠 Separation force: avoid other enemies
            Vector3 separation = Vector3.zero;
            float separationRadius = 1.5f; // distance to keep between enemies
            float separationStrength = 2f; // how strongly to push away

            foreach (enemy other in FindObjectsOfType<enemy>())
            {
                if (other == this) continue;
                float dist = Vector2.Distance(transform.position, other.transform.position);
                if (dist < separationRadius)
                {
                    Vector3 away = (transform.position - other.transform.position).normalized / dist;
                    separation += away * (separationRadius - dist);
                }
            }

            // Apply separation
            direction += separation * separationStrength;
            direction.Normalize();

            moveDirection = direction;
        }
        else
        {
            moveDirection = Vector2.zero;
            StartCoroutine(Attack());
        }
    }
}

    private void FixedUpdate()
    {
        if (isStunned){
            rb.linearVelocity = Vector2.zero;
            return;
        }
        rb.linearVelocity= new Vector2(moveDirection.x,moveDirection.y)*movespeed;
    }
    public void TakeDamage(float damage)
    {
        health -= damage;
        hitcount++;
        StartCoroutine(Flash());

        rb.linearVelocity = Vector2.zero;

        if(hitcount >= 1)
        {
            hitcount = 0;
            StartCoroutine(Counter());
        }
        else
        {
            StartCoroutine(Stun());
        }
        if (health <= 0)
        {
            Die();
        }
    }
    private IEnumerator Attack()
    {
        if (isStunned) yield break;
        isAttacking = true;
        Debug.Log("Enemy Attacks");
        if (attackHitbox != null)
        {
            attackHitbox.gameObject.SetActive(true);
        }
        yield return new WaitForSeconds(attduration);
        if (attackHitbox != null)
        attackHitbox.gameObject.SetActive(false);

    // Wait for cooldown before next attack
        yield return new WaitForSeconds(attCool);
        isAttacking = false;

    }

    private IEnumerator Flash(){
        if(sprite != null){
            sprite.color  = flashColor;
            yield return new WaitForSeconds(flashDuration);
            sprite.color = originalColor;
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        
            player playerS = collision.gameObject.GetComponent<player>();
            if (playerS != null)
        {
                
                Vector2 hitDir = (playerS.transform.position - transform.position).normalized;
                playerS.TakeDamage(attackDamage);
            
        }
    }
    private void FlipTowardsPlayer()
    {
        if (target == null) return;

        float direction = target.position.x - transform.position.x;

        if (direction > 0) // player is to the right
            transform.localScale = new Vector3(Mathf.Abs(originalScale.x), originalScale.y, originalScale.z);
        else if (direction < 0) // player is to the left
            transform.localScale = new Vector3(-Mathf.Abs(originalScale.x), originalScale.y, originalScale.z);
    }
    private IEnumerator Stun()
    {
        isStunned = true;
        rb.linearVelocity = Vector2.zero;
        yield return new WaitForSeconds(stunDuration);
        Debug.Log("Enemy stunned");
        isStunned = false;
    }
    private IEnumerator Counter()
    {
        isStunned = true;
        rb.linearVelocity = Vector2.zero;
        Debug.Log("Counter begins");

        if(sprite != null)
        {
            sprite.color = Color.yellow;
        }
        yield return new WaitForSeconds(0.5f);
        isStunned = false;
        yield return StartCoroutine(Attack());
        isStunned = false;
        Debug.Log("counter done");
    }


public void Die()
    {
        if (spawner != null)
        {
            spawner.EnemyDied();
            
        }
        Destroy(gameObject);
    }
}
    
