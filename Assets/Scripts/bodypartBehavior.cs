using UnityEngine;
using System.Collections;
[RequireComponent(typeof(Rigidbody2D))]

public class bodypartBehavior : MonoBehaviour {
	public float speed;

	// Use this for initialization
	void Start () {
		float zRot = Random.Range(0.0f, 360.0f);
		transform.Rotate(0.0f, 0.0f, zRot);
		GetComponent<Rigidbody2D>().velocity = speed * (transform.rotation * Vector3.right);
		GetComponent<Rigidbody2D> ().angularVelocity += Random.Range (-1800, 1800);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
