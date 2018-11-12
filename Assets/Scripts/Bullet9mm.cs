using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet9mm : MonoBehaviour {
    public float bulletSpeed = 100;
    private Rigidbody rd;

	// Use this for initialization
	void Start () {
        rd = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {
        rd.AddRelativeForce(Vector3.forward * bulletSpeed);
	}
}
