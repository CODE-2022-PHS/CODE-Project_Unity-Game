using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GhoulAI : MonoBehaviour
{
    //Variables
    public NavMeshAgent agent;
    public Transform player;
    public LayerMask whatIsGround, whatIsPlayer;
    public float health;
    public GameObject ghoul;

    //float runSpeed;
    
    //Patrolling Variables
    public Vector3 walkPoint;
    bool walkPointSet;
    public float walkPointRange;

    //Attacking Variables
    public float timeBetweenAttacks;
    bool alreadyAttacked;

    //AI States
    public float attackRange;
    //bool sightRange;
    public bool playerInSightRange, playerInAttackRange;

    private void Awake()
    {
        player = GameObject.Find("Player").transform;

        agent = GetComponent<NavMeshAgent>();
    }

    /*
    // Start is called before the first frame update
    void Start()
    {
        sightRange = GetComponent<FieldOfView>().canSeePlayer;
    }*/

    // Update is called once per frame
    void Update()
    {
        //playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);
        playerInSightRange = GetComponent<FieldOfView>().canSeePlayer;
        //playerInAttackRange = attackRange.canSeePlayer;

        if (!playerInSightRange && !playerInAttackRange)
        {
            agent.speed = 3.5f;
            Patrolling();
        }
        if(!playerInSightRange && playerInAttackRange)
        {
            agent.speed = 3.5f;
            Patrolling();
        }
        if(playerInSightRange && !playerInAttackRange)
        {
            agent.speed = 10;
            ChasePlayer();
        }
        if(playerInSightRange && playerInAttackRange)
        {
            AttackPlayer();
        }
    }
    private void Patrolling()
    {
        if(!walkPointSet)
        {
            SearchWalkPoint();
        }

        if(walkPointSet)
        {
            agent.SetDestination(walkPoint);
        }

        Vector3 distanceToWalkPoint = transform.position - walkPoint;

        if(distanceToWalkPoint.magnitude < 1f)
        {
            walkPointSet = false; 
        }
    }

    private void SearchWalkPoint()
    {
        //calculate a random range to walk
        float randomZ = Random.Range(-walkPointRange, walkPointRange);
        float randomX = Random.Range(-walkPointRange, walkPointRange);

        walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);

        if(Physics.Raycast(walkPoint, -transform.up, 2f, whatIsGround))
        {
            walkPointSet = true;
        }
    }

    private void ChasePlayer()
    {
        agent.SetDestination(player.position);
    }

    private void AttackPlayer()
    {
        //Enemy will stop moving when attacking
        agent.SetDestination(transform.position);

        //transform.LookAt(player);
        LookAtPlayer();
        

        if(!alreadyAttacked)
        {
            //Attack code
            if(GetComponent<Animation>().IsPlaying("Attack1"))
            {
                player.GetComponent<PlayerHealth>().Damage(5);
            }

            alreadyAttacked = true;
            Invoke(nameof(ResetAttack), timeBetweenAttacks);
        }
    }
    private void LookAtPlayer()
    {
        Vector3 direction = (player.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 25f);
    }

    private void ResetAttack()
    {
        alreadyAttacked = false;
    }

    private void TakeDamage(int damage)
    {
        health -= damage;

        if(health <= 0)
        {
            Invoke(nameof(DestroyEnemy), 0.5f);
        }
    }

    private void DestroyEnemy()
    {
        Destroy(gameObject);
    }
    
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
        //Gizmos.color = Color.yellow;
        //Gizmos.DrawWireSphere(transform.position, sightRange);
    }
    
}
