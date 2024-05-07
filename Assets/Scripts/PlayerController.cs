using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float speed = 5f;
    private Animator animator;
    [SerializeField] float attackRadius = 2f;
    [SerializeField] LayerMask enemyLayer;
    private Vector3 movementInput;
    Rigidbody rb;
    int kills = 0;

    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        // Get player input for movement
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        float verticalInput = Input.GetAxisRaw("Vertical");

        // Calculate movement direction
        movementInput = new Vector3(horizontalInput, 0f, verticalInput).normalized;

        // Update animator parameters
        animator.SetFloat("MovementX", horizontalInput);
        animator.SetFloat("MovementZ", verticalInput);

        rb.velocity = movementInput * speed;

        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            animator.SetBool("Attack", true);
        }
    }

    void Attack()
    {
        // Create a sphere around the player
        Collider[] hitEnemies = Physics.OverlapSphere(transform.position, attackRadius, enemyLayer);

        // Check for enemy colliders
        foreach (Collider enemy in hitEnemies)
        {
            // You can add logic here to damage or interact with the enemy
            kills++;
            enemy.gameObject.SetActive(false);
            if(GetKills() % 25 == 0)
            GetComponent<HealthManager>().AddHealth(25f);
            GetComponent<HealthManager>().UpdateHealthBar();
        }
        animator.SetBool("Attack", false);
    }

    public int GetKills()
    {
        return kills;
    }

    void OnDrawGizmosSelected()
    {
        // Draw the attack radius gizmo in the Unity Editor
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRadius);
    }
}