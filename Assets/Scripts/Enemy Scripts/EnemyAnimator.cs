using UnityEngine;

public class EnemyAnimator : MonoBehaviour {

    private Animator enemyAnimator;

    void Start() {
        enemyAnimator = GetComponent<Animator>();
    }

    public void Walk(bool walk) {
        enemyAnimator.SetBool(AnimationTags.WALK_PARAMETER, walk);
    }

    public void Run(bool run) {
        enemyAnimator.SetBool(AnimationTags.RUN_PARAMETER, run);
    }

    public void Attack() {
        enemyAnimator.SetTrigger(AnimationTags.ATTACK_TRIGGER);
    }

    public void Dead() {
        enemyAnimator.SetTrigger(AnimationTags.DEAD_TRIGGER);
    }
}