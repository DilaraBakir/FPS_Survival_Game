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

    private PlayerFootsteps playerFootsteps;

    private float sprintVolume = 1f;

    private float crouchVolume = 0.1f;

    private float walkVolumeMin = 0.2f, walkVolumeMax = 0.6f;

    private float walkStepDistance = 0.4f;

    private float sprintStepDistance = 0.25f;

    private float crouchStepDistance = 0.5f;

    void Awake() {
        playerMovement = GetComponent<PlayerMovement>();

        lookRoot = transform.GetChild(0);

        playerFootsteps = GetComponentInChildren<PlayerFootsteps>();
    }

    void Start() {
        playerFootsteps.volumeMin = walkVolumeMin;
        playerFootsteps.volumeMax = walkVolumeMax;
        playerFootsteps.stepDistance = walkStepDistance;
    }

    void Update() {
        Sprint();
        Crouch();
    }

    void Sprint() {
        if (Input.GetKeyDown(KeyCode.LeftShift) && !isCrouching) {
            playerMovement.speed = sprintSpeed;

            playerFootsteps.stepDistance = sprintStepDistance;
            playerFootsteps.volumeMin = sprintVolume;
            playerFootsteps.volumeMax = sprintVolume;
        }

        if (Input.GetKeyUp(KeyCode.LeftShift) && !isCrouching) {
            playerMovement.speed = moveSpeed;

            playerFootsteps.stepDistance = walkStepDistance;
            playerFootsteps.volumeMin = walkVolumeMin;
            playerFootsteps.volumeMax = walkVolumeMax;
        }
    }

    void Crouch() {
        if (Input.GetKeyDown(KeyCode.C)) {
            if (isCrouching) {
                //if we are crouching, stand up
                lookRoot.localPosition = new Vector3(0f, standHeight, 0f);
                playerMovement.speed = moveSpeed;

                playerFootsteps.stepDistance = walkStepDistance;
                playerFootsteps.volumeMin = walkVolumeMin;
                playerFootsteps.volumeMax = walkVolumeMax;

                isCrouching = false;
            }
            else {
                //if we are not crouching, crouch down
                lookRoot.localPosition = new Vector3(0f, crouchHeight, 0f);
                playerMovement.speed = crouchSpeed;

                playerFootsteps.stepDistance = crouchStepDistance;
                playerFootsteps.volumeMin = crouchVolume;
                playerFootsteps.volumeMax = crouchVolume;

                isCrouching = true;
            }
        }
    }
}
