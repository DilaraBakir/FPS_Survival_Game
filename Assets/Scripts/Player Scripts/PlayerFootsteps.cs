using UnityEngine;

public class PlayerFootsteps : MonoBehaviour {

    private AudioSource footstepSound;

    [SerializeField] private AudioClip[] footstepClips;

    private CharacterController characterController;

    [HideInInspector] public float volumeMin, volumeMax;

    private float accumulatedDistance;

    [HideInInspector] public float stepDistance;

    void Awake() {
        footstepSound = GetComponent<AudioSource>();
        characterController = GetComponentInParent<CharacterController>();
    }


    void Update() {
        CheckToPlayFootstepSound();
    }

    void CheckToPlayFootstepSound() {
        //if we are not grounded, return
        if (!characterController.isGrounded)
            return;

        //if we are moving
        if (characterController.velocity.sqrMagnitude > 0) {
            //accumulated distance is the value how far we can go before playing the next footstep sound
            accumulatedDistance += Time.deltaTime;
            //if we have moved enough distance
            if (accumulatedDistance > stepDistance) {
                //play footstep sound
                footstepSound.volume = Random.Range(volumeMin, volumeMax);
                footstepSound.clip = footstepClips[Random.Range(0, footstepClips.Length)];
                footstepSound.Play();

                accumulatedDistance = 0f;
            }
        }
        else {
            accumulatedDistance = 0f;
        }
    }
}