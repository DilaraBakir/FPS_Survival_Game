using UnityEngine;

public class PlayerSprintAndCrouch : MonoBehaviour {

    private PlayerMovement playerMovement;

    public float sprintSpeed = 10f;

    public float moveSpeed = 5f;

    public float crouchSpeed = 2f;

    private Transform lookRoot;

    private float standHeight = 1.6f;

    private float crouchHeight = 1f;

    private bool isCrouching;


    void Awake() {
        playerMovement = GetComponent<PlayerMovement>();

        lookRoot = transform.GetChild(0);
    }

    void Update() {
        Sprint();
        Crouch();
    }

    void Sprint() {
        if (Input.GetKeyDown(KeyCode.LeftShift) && !isCrouching) {
            playerMovement.speed = sprintSpeed;
        }

        if (Input.GetKeyUp(KeyCode.LeftShift) && !isCrouching) {
            playerMovement.speed = moveSpeed;
        }
    }

    void Crouch() {
        if (Input.GetKeyDown(KeyCode.C)) {
            if (isCrouching) {
                //if we are crouching, stand up
                lookRoot.localPosition = new Vector3(0f, standHeight, 0f);
                playerMovement.speed = moveSpeed;
                isCrouching = false;
            }
            else {
                //if we are not crouching, crouch down
                lookRoot.localPosition = new Vector3(0f, crouchHeight, 0f);
                playerMovement.speed = crouchSpeed;
                isCrouching = true;
            }
        }
    }
}
