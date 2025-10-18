using UnityEngine;

public class WeaponManager : MonoBehaviour {

    [SerializeField] private WeaponHandler[] weapons;

    private int currentWeaponIndex;


    void Start() {
        currentWeaponIndex = 0;
        weapons[currentWeaponIndex].gameObject.SetActive(true);
    }


    void Update() {

        if (Input.GetKeyDown(KeyCode.Alpha1)) {
            TurnOnSelectedWeapon(0);
        }

        if (Input.GetKeyDown(KeyCode.Alpha2)) {
            TurnOnSelectedWeapon(1);
        }

        if (Input.GetKeyDown(KeyCode.Alpha3)) {
            TurnOnSelectedWeapon(2);
        }

        if (Input.GetKeyDown(KeyCode.Alpha4)) {
            TurnOnSelectedWeapon(3);
        }

        if (Input.GetKeyDown(KeyCode.Alpha5)) {
            TurnOnSelectedWeapon(4);
        }

        if (Input.GetKeyDown(KeyCode.Alpha6)) {
            TurnOnSelectedWeapon(5);
        }
    }

    void TurnOnSelectedWeapon(int weaponIndex) {
        //turn off current weapon
        weapons[currentWeaponIndex].gameObject.SetActive(false);
        //turn on selected weapon
        weapons[weaponIndex].gameObject.SetActive(true);
        //update current weapon index
        currentWeaponIndex = weaponIndex;
    }

    public WeaponHandler GetCurrentSelectedWeapon() {
        return weapons[currentWeaponIndex];
    }
}
