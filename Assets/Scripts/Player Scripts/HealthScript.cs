using UnityEngine;
using UnityEngine.AI;

public class HealthScript : MonoBehaviour {

    private EnemyAnimator enemyAnimator;
    private NavMeshAgent navMeshAgent;
    private EnemyController enemyController;

    public float health = 100f;

    public bool isPlayer, isBoar, isCannibal;

    private bool isDead;

    void Awake() {
        if (isBoar || isCannibal) {
            enemyAnimator = GetComponent<EnemyAnimator>();
            enemyController = GetComponent<EnemyController>();
            navMeshAgent = GetComponent<NavMeshAgent>();
            //get enemy audio
        }

        if (isPlayer) {
            //get player audio
        }
    }


    public void ApplyDamage(float damage) {
        if (isDead)
            return;
        health -= damage;

        if (isPlayer) {
            //display the health ui
        }

        if (isBoar || isCannibal) {
            if (enemyController.EnemyState == EnemyState.PATROL) {
                enemyController.chaseDistance = 50f;
            }
        }

        if (health <= 0f) {
            PlayerDied();
            isDead = true;
        }
    }

    void PlayerDied() {

        if (isCannibal) {
            GetComponent<Animator>().enabled = false;
            GetComponent<BoxCollider>().isTrigger = false;
            GetComponent<Rigidbody>().AddTorque(-transform.forward * 5f);

            enemyController.enabled = false;
            navMeshAgent.enabled = false;
            enemyAnimator.enabled = false;

            //start coroutine

            //enemy manager spawn more enemies
        }

        if (isBoar) {
            navMeshAgent.velocity = Vector3.zero;
            navMeshAgent.isStopped = true;
            enemyController.enabled = false;

            enemyAnimator.Dead();

            // start coroutine

            //enemy manager spawn more enemies

        }

        if (isPlayer) {
            GameObject[] enemies = GameObject.FindGameObjectsWithTag(Tags.ENEMY_TAG);
            for (int i = 0; i < enemies.Length; i++) {
                enemies[i].GetComponent<EnemyController>().enabled = false;
            }

            //call enemy manager to stop spawning enemies

            GetComponent<PlayerMovement>().enabled = false;
            GetComponent<PlayerAttack>().enabled = false;
            GetComponent<WeaponManager>().GetCurrentSelectedWeapon().gameObject.SetActive(false);
        }

        if (tag == Tags.PLAYER_TAG) {
            Invoke("RestartGame", 3f);
        }
        else {
            Invoke("TurnOffGameObject", 3f);
        }
    }

    void RestartGame() {
        UnityEngine.SceneManagement.SceneManager.LoadScene("SampleScene");
    }

    void TurnOffGameObject() {
        gameObject.SetActive(false);
    }
}
