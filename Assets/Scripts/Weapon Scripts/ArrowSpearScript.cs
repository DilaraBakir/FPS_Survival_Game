using UnityEngine;

public class ArrowSpearScript : MonoBehaviour {

    private Rigidbody myBody;

    public float speed = 30f;

    public float deactivateTimer = 3f;

    public float damage = 50f;

    void Awake() {
        myBody = GetComponent<Rigidbody>();
    }

    void Start() {
        Invoke("DeactivateGameObject", deactivateTimer);
    }

    public void Launch(Camera mainCamera) {
        //we set the velocity of the arrow in the forward direction of the camera
        myBody.velocity = mainCamera.transform.forward * speed;

        transform.LookAt(transform.position + myBody.velocity);
    }

    void DeactivateGameObject() {
        if (gameObject.activeInHierarchy) {
            gameObject.SetActive(false);
        }
    }

    void OnTriggerEnter(Collider target) {
        //after we touch an enemy we deactivate the arrow   
        if (target.tag == Tags.ENEMY_TAG) {
            target.GetComponent<HealthScript>().ApplyDamage(damage);
            gameObject.SetActive(false);
        }
    }
}
