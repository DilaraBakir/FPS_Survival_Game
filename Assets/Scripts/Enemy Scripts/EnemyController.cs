using UnityEngine;
using UnityEngine.AI;

public enum EnemyState {
    PATROL,
    CHASE,
    ATTACK
}

public class EnemyController : MonoBehaviour {

    private EnemyAnimator enemyAnimator;

    private NavMeshAgent navMeshAgent;

    private EnemyState enemyState;

    public float walkSpeed = 0.5f;
    public float runSpeed = 4f;

    public float chaseDistance = 7f;
    private float currentChaseDistance;
    public float attackDistance = 1.5f;
    public float chaseAfterAttackDistance = 2f;

    public float patrolRadiusMin = 20f, patrolRadiusMax = 60f;
    public float patrolForThisTime = 15f;
    private float patrolTimer;

    public float waitBeforeAttack = 2f;
    private float attackTimer;

    private Transform target;

    public GameObject attackPoint;

    private EnemyAudio enemyAudio;

    void Awake() {
        enemyAnimator = GetComponent<EnemyAnimator>();
        navMeshAgent = GetComponent<NavMeshAgent>();
        target = GameObject.FindWithTag(Tags.PLAYER_TAG).transform;

        enemyAudio = GetComponentInChildren<EnemyAudio>();
    }


    void Start() {

        enemyState = EnemyState.PATROL;
        patrolTimer = patrolForThisTime;
        //when the enemy starts the game, it will wait for some time before attacking
        attackTimer = waitBeforeAttack;
        //memorize the value of chase distance to reset it later
        currentChaseDistance = chaseDistance;
    }

    void Update() {

        if (enemyState == EnemyState.PATROL) {
            Patrol();
        }

        if (enemyState == EnemyState.CHASE) {
            Chase();
        }

        if (enemyState == EnemyState.ATTACK) {
            Attack();
        }


    }

    void Patrol() {
        //tell nav agent to walk
        navMeshAgent.isStopped = false;
        navMeshAgent.speed = walkSpeed;

        //add to the patrol timer
        patrolTimer += Time.deltaTime;

        if (patrolTimer > patrolForThisTime) {
            SetNewRandomDestination();
            patrolTimer = 0f;
        }

        if (navMeshAgent.velocity.sqrMagnitude > 0) {
            enemyAnimator.Walk(true);
        }
        else {
            enemyAnimator.Walk(false);
        }

        //check the distance between enemy and player
        if (Vector3.Distance(transform.position, target.position) <= chaseDistance) {

            enemyAnimator.Walk(false);
            enemyState = EnemyState.CHASE;

            //play spotted audio
            enemyAudio.PlayScreamSound();
        }
    }

    void Chase() {
        navMeshAgent.isStopped = false;
        navMeshAgent.speed = runSpeed;

        //set the destination of the nav mesh agent to the player's position
        navMeshAgent.SetDestination(target.position);

        if (navMeshAgent.velocity.sqrMagnitude > 0) {
            enemyAnimator.Run(true);
        }
        else {
            enemyAnimator.Run(false);
        }

        if (Vector3.Distance(transform.position, target.position) <= attackDistance) {
            enemyAnimator.Run(false);
            enemyAnimator.Walk(false);
            enemyState = EnemyState.ATTACK;
            //reset the chase distance to its original value
            if (chaseDistance != currentChaseDistance) {
                chaseDistance = currentChaseDistance;
            }

        }
        else if (Vector3.Distance(transform.position, target.position) > chaseDistance) {
            enemyAnimator.Run(false);
            enemyState = EnemyState.PATROL;
            patrolTimer = patrolForThisTime;

            if (chaseDistance != currentChaseDistance) {
                chaseDistance = currentChaseDistance;
            }
        }

    }

    void Attack() {
        navMeshAgent.velocity = Vector3.zero;
        navMeshAgent.isStopped = true;

        attackTimer += Time.deltaTime;

        if (attackTimer > waitBeforeAttack) {
            enemyAnimator.Attack();
            attackTimer = 0f;

            //play attack sound
            enemyAudio.PlayAttackSound();
        }

        if (Vector3.Distance(transform.position, target.position) > attackDistance + chaseAfterAttackDistance) {
            enemyState = EnemyState.CHASE;

        }

    }

    void SetNewRandomDestination() {
        float randRadius = Random.Range(patrolRadiusMin, patrolRadiusMax);

        Vector3 randDirection = Random.insideUnitSphere * randRadius;
        randDirection += transform.position;

        NavMeshHit navHit;
        NavMesh.SamplePosition(randDirection, out navHit, randRadius, -1);
        navMeshAgent.SetDestination(navHit.position);
    }

    void TurnOnAttackPoint() {
        attackPoint.SetActive(true);
    }

    void TurnOffAttackPoint() {
        if (attackPoint.activeInHierarchy) {
            attackPoint.SetActive(false);
        }
    }

    public EnemyState EnemyState {
        get; set;
    }
}