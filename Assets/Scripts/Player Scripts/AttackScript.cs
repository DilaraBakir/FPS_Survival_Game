using UnityEngine;

public class AttackScript : MonoBehaviour {

    public float damage = 2f;
    public float radius = 1f;
    public LayerMask layermask;



    void Update() {
        Collider[] hits = Physics.OverlapSphere(transform.position, radius, layermask);

        if (hits.Length > 0) {
            hits[0].gameObject.GetComponent<HealthScript>().ApplyDamage(damage);
            gameObject.SetActive(false);
        }
    }
}
