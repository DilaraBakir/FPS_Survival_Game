using UnityEngine;

public class PlayerAttack : MonoBehaviour {

    private WeaponManager weaponManager;

    public float fireRate = 15f;

    private float nextTimeToFire = 0f;

    public float damage = 20f;


    void Awake() {
        weaponManager = GetComponent<WeaponManager>();
    }

    void Start() {

    }


    void Update() {
        WeaponShoot();
    }

    void WeaponShoot() {
        //if we have assault rifle
        if (weaponManager.GetCurrentSelectedWeapon().fireType == WeaponFireType.Multiple) {

            //if we prress and hold left mouse button and if time now is more than next time to fire
            if (Input.GetMouseButton(0) && Time.time > nextTimeToFire) {

                nextTimeToFire = Time.time + 1f / fireRate;

                weaponManager.GetCurrentSelectedWeapon().ShootAnimation();
            }
        }
        else {

            //if we have a regular weapon that shoots single bullet
            if (Input.GetMouseButtonDown(0)) {
                //handle axe
                if (weaponManager.GetCurrentSelectedWeapon().tag == Tags.AXE_TAG) {
                    weaponManager.GetCurrentSelectedWeapon().ShootAnimation();

                    //BulletFired();
                }
                // handle shoot
                if (weaponManager.GetCurrentSelectedWeapon().bulletType == WeaponBulletType.Bullet) {
                    weaponManager.GetCurrentSelectedWeapon().ShootAnimation();

                    //BulletFired();
                }
                else {
                    //handle bow and arrow or spear
                }
            }
        }
    }
}
