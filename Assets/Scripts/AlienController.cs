using UnityEngine;
using UnityEngine.AI;

public class AlienController : MonoBehaviour
{
    [SerializeField] float attackRange = 1.5f;
    [SerializeField] float moveSpeed = 3f;
    Transform target;
    NavMeshAgent navMeshAgent;
    float distanceToPlayer;
    Animator animator;

    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        navMeshAgent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, target.position);

        if (distanceToPlayer <= attackRange)
        {
            navMeshAgent.isStopped = true;
            animator.SetBool("Move", false);
            animator.SetBool("Attack", true);
        }
        else if (distanceToPlayer <= 6f)
        {
            navMeshAgent.isStopped = false;
            animator.SetBool("Move", true);
            animator.SetBool("Attack", false);
            navMeshAgent.SetDestination(target.position);
        }

        transform.forward = Camera.main.transform.position - transform.position;
    }

    public void DamagePlayer()
    {
        target.GetComponent<HealthManager>().TakeDamage(2f);
    }
}
