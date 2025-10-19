using UnityEngine;

public class PlayerAttack : MonoBehaviour {

    private WeaponManager weaponManager;

    public float fireRate = 15f;

    private float nextTimeToFire = 0f;

    public float damage = 20f;

    private Animator zoomCameraAnim;

    private bool zoomed;

    private Camera mainCam;

    private GameObject crosshair;

    private bool isAiming;

    [SerializeField] private GameObject arrowPrefab, spearPrefab;

    [SerializeField] private Transform arrowBowStartPosition;


    void Awake() {
        weaponManager = GetComponent<WeaponManager>();

        zoomCameraAnim = transform.Find(Tags.LOOK_ROOT).transform.Find(Tags.ZOOM_CAMERA).GetComponent<Animator>();

        crosshair = GameObject.FindWithTag(Tags.CROSSHAIR);

        mainCam = Camera.main;
    }

    void Start() {

    }


    void Update() {
        WeaponShoot();

        ZoomInAndOut();
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

                    BulletFired();
                }
                // handle shoot
                if (weaponManager.GetCurrentSelectedWeapon().bulletType == WeaponBulletType.Bullet) {
                    weaponManager.GetCurrentSelectedWeapon().ShootAnimation();

                    BulletFired();
                }
                else {
                    //handle bow and arrow or spear
                    if (isAiming) {

                        weaponManager.GetCurrentSelectedWeapon().ShootAnimation();

                        if (weaponManager.GetCurrentSelectedWeapon().bulletType == WeaponBulletType.Arrow) {
                            ThrowArrowOrSpear(true);
                        }
                        else if (weaponManager.GetCurrentSelectedWeapon().bulletType == WeaponBulletType.Spear) {
                            ThrowArrowOrSpear(false);
                        }
                    }
                }
            }
        }
    }
    void ZoomInAndOut() {
        //we are going to aim with our camera on the weapon
        if (weaponManager.GetCurrentSelectedWeapon().weaponAim == WeaponAim.Aim) {

            //if we press and hold right mouse button
            if (Input.GetMouseButtonDown(1)) {
                zoomCameraAnim.Play(AnimationTags.ZOOM_IN_ANIM);
                crosshair.SetActive(false);
            }

            //when we release right mouse button
            if (Input.GetMouseButtonUp(1)) {
                zoomCameraAnim.Play(AnimationTags.ZOOM_OUT_ANIM);
                crosshair.SetActive(true);
            }
        }

        if (weaponManager.GetCurrentSelectedWeapon().weaponAim == WeaponAim.SelfAim) {

            if (Input.GetMouseButtonDown(1)) {
                weaponManager.GetCurrentSelectedWeapon().Aim(true);
                isAiming = true;
            }

            if (Input.GetMouseButtonUp(1)) {
                weaponManager.GetCurrentSelectedWeapon().Aim(false);
                isAiming = false;
            }
        }
    }

    void ThrowArrowOrSpear(bool throwArrow) {
        if (throwArrow) {
            //create arrow object
            GameObject arrow = Instantiate(arrowPrefab);
            //position the arrow at the bow start position
            arrow.transform.position = arrowBowStartPosition.position;
            //get the ArrowBowScript component of the arrow and launch the arrow from the main camera
            arrow.GetComponent<ArrowSpearScript>().Launch(mainCam);

        }
        else {
            GameObject spear = Instantiate(spearPrefab);
            spear.transform.position = arrowBowStartPosition.position;
            spear.GetComponent<ArrowSpearScript>().Launch(mainCam);
        }
    }

    void BulletFired() {
        RaycastHit hit;

        if (Physics.Raycast(mainCam.transform.position, mainCam.transform.forward, out hit)) {
            print("We hit " + hit.transform.name);
            //we check if we hit an enemy

        }
    }
}