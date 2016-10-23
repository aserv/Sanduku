using UnityEngine;
using System.Collections;

public class MidWallSpikeBehavior : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnCollisionEnter2D(Collision2D collider)
    {
        IDamageable other = collider.gameObject.GetComponent<IDamageable>();
        if (other != null)
            other.Damage();
    }
}
