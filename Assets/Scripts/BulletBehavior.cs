using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody2D))]
public class BulletBehavior : MonoBehaviour {
    public float Speed;
	// Use this for initialization
	void Start () {
        GetComponent<Rigidbody2D>().velocity = Speed * (transform.rotation * Vector3.down);
	}

    void OnCollisionEnter2D(Collision2D collider)
    {
        IDamageable other = collider.gameObject.GetComponent<IDamageable>();
        if (other != null)
            other.Damage();
        Destroy(gameObject);
    }
}
