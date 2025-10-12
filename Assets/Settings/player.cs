using UnityEngine;
using UnityEngine.SceneManagement;

public class player : MonoBehaviour
{
    public float movementSpeed = 2f;
    public Person p;
    private Rigidbody2D rb;
    private Vector2 movementDirection;
    public Transform Aim;

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
        Animate();
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
        if(iswalking){
            Vector3 vector3 = Vector3.left * movementDirection.x + Vector3.down * movementDirection.y;
            Aim.rotation = Quaternion.LookRotation(Vector3.forward,vector3); 
        }

        
    }

    void ProcessInputs()
    {
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");

        // Track last moved direction when there's no input
        if (moveX== 0 && moveY == 0 && (movementDirection.x != 0 || movementDirection.y != 0))
        {
            iswalking = false; 
            lastMoved = movementDirection;
                        Vector3 vector3 = Vector3.left * lastMoved.x + Vector3.down * lastMoved.y;
            Aim.rotation = Quaternion.LookRotation(Vector3.forward,vector3); 
        }else if(moveX != 0 || moveY !=0){
            iswalking = true;  
        }

        movementDirection.x = moveX;
        movementDirection.y = moveY ;

        // Normalize to prevent diagonal movement being faster
        if (movementDirection.magnitude > 1)
        {
            movementDirection.Normalize();
        }
    }

    void Animate()
    {
        anim.SetFloat("move x", movementDirection.x);
        anim.SetFloat("move y", movementDirection.y);
        anim.SetFloat("movemag", movementDirection.magnitude);
        anim.SetFloat("lastmove x", lastMoved.x);
        anim.SetFloat("lastmove y", lastMoved.y);
    }
    public void TakeDamage(int damage)
{
    if (isInvincible) return; // Ignore if player is invincible

    health -= damage;
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

}
