using UnityEditor.Tilemaps;
using UnityEngine;
using UnityEngine.SceneManagement;

public class player : MonoBehaviour
{
    public float movementSpeed = 2f;
    public Person p;
    private Rigidbody2D rb;
    private Vector2 movementDirection;
    

    public int health = 30;
    bool iswalking = false;
    public int maxHealth = 50;
    public int defense = 10;
    public int attackDamage = 15;
    Animator anim;
    private Vector2 lastMoved;
     private bool isInvincible = false;
    public float invincibilityDuration = 1f; // 1 second of invincibility after getting hit
    private float invincibilityTimer;
    private SpriteRenderer spriteRenderer;
    public float flashInterval = 0.1f; // How fast to flash
    private float flashTimer;
    private bool isfacingright;

  



    void Start()
    {
        p = GetComponent<Person>();
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();

    }

    void Update()
    {
        ProcessInputs();
        if (movementDirection.x>0 && isfacingright)
        {
            Flip();
        }else if (movementDirection.x<0 && !isfacingright)
        {
            Flip();
        }
        if (isInvincible){   
        invincibilityTimer -= Time.deltaTime;
        flashTimer -= Time.deltaTime;

        if (flashTimer <= 0f){
        Flash();
        flashTimer = flashInterval;
        }

        if (invincibilityTimer <= 0f){
        isInvincible = false;
        spriteRenderer.enabled = true; // Make sure player is visible again
        }
     }

    }

    void FixedUpdate()
    {
        rb.linearVelocity = movementDirection * movementSpeed;
        

        
    }

    void ProcessInputs()
    {
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");

        movementDirection = new Vector2(moveX, moveY);

        // Track last moved direction when there's no input
        if (movementDirection.magnitude > 1)
        {
        movementDirection.Normalize();
     }

        if (movementDirection != Vector2.zero)
     {
        iswalking = true;
        lastMoved = movementDirection; 
     }
        else
        {
        iswalking = false;
     }
    }


    public void TakeDamage(int damage)
{
    if (isInvincible) return; // Ignore if player is invincible

        health -= damage;
    
    Rigidbody2D rb = GetComponent<Rigidbody2D>();
    rb.linearVelocity = Vector2.zero;
    
    if (health <= 0)
    {
        Die();
    }
    else
    {
        isInvincible = true;
        invincibilityTimer = invincibilityDuration;
    }
}


private void Die()
{
    Debug.Log("Player has died!");
    // Here you could also add animations, disable controls, etc.
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
}

    private void Flash()
    {
        if (spriteRenderer != null)
        {
            spriteRenderer.enabled = !spriteRenderer.enabled; // Toggle visibility
        }
    }
private void Flip()
{
    isfacingright = !isfacingright;
    Vector3 localScale = transform.localScale;
    localScale.x *= -1; // Flip horizontally
    transform.localScale = localScale;
}


}
