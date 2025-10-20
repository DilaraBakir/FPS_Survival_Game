using UnityEngine;

public class EnemyAudio : MonoBehaviour {

    private AudioSource audioSource;

    [SerializeField] private AudioClip screamClip, dieClip;

    [SerializeField] private AudioClip[] attackClips;

    void Awake() {
        audioSource = GetComponent<AudioSource>();
    }

    public void PlayScreamSound() {
        audioSource.clip = screamClip;
        audioSource.Play();
    }

    public void PlayAttackSound() {
        int randomIndex = Random.Range(0, attackClips.Length);
        audioSource.clip = attackClips[randomIndex];
        audioSource.Play();
    }

    public void PlayDieSound() {
        audioSource.clip = dieClip;
        audioSource.Play();
    }
}
