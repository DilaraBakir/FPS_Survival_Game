using UnityEngine;

public enum WeaponAim {
    None,
    SelfAim,
    Aim,
}

public enum WeaponFireType {
    Single,
    Multiple,
}

public enum WeaponBulletType {
    Bullet,
    Arrow,
    Spear,
    None,
}

public class WeaponHandler : MonoBehaviour {

    private Animator animator;

    public WeaponAim weaponAim;

    [SerializeField] private GameObject muzzleFlash;

    [SerializeField] private AudioSource shootSound, reloadSound;

    public WeaponFireType fireType;

    public WeaponBulletType bulletType;

    public GameObject attackPoint;


    void Awake() {
        animator = GetComponent<Animator>();
    }


    public void ShootAnimation() {
        animator.SetTrigger(AnimationTags.SHOOT_TRIGGER);
    }

    public void Aim(bool canAim) {
        animator.SetBool(AnimationTags.AIM_PARAMETER, canAim);
    }

    void TurnOnMuzzleFlash() {
        muzzleFlash.SetActive(true);
    }

    void TurnOffMuzzleFlash() {
        muzzleFlash.SetActive(false);
    }

    void PlayShootSound() {
        shootSound.Play();
    }

    void PlayReloadSound() {
        reloadSound.Play();
    }

    void TurnOnAttackPoint() {
        attackPoint.SetActive(true);
    }

    void TurnOffAttackPoint() {
        if (attackPoint.activeInHierarchy) {
            attackPoint.SetActive(false);
        }
    }
}