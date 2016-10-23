using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody2D))]
public class BulletBehavior : MonoBehaviour {
    public float Speed;
	// Use this for initialization
	void Start () {
        GetComponent<Rigidbody2D>().velocity = Speed * (transform.rotation * Vector3.down);
        Destroy(gameObject, 5);
	}

    void OnTriggerEnter2D(Collider2D collider)
    {
        Debug.Log("Bullet colliding");
        IDamageable other = collider.gameObject.GetComponent<IDamageable>();
        if (other != null)
            other.Damage();
    }
}
