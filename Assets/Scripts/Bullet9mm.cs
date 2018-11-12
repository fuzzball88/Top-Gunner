using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet9mm : MonoBehaviour {
    public float bulletSpeed = 50;
    private Rigidbody rd;

	// Use this for initialization
	void Start () {
        rd = GetComponent<Rigidbody>();
        Shoot();

        //rd.velocity = transform.forward * bulletSpeed;
    }
	
	// Update is called once per frame
	void Update () {
        
    }

    void Shoot()
    {
        rd.AddForce(transform.forward * bulletSpeed);
    }

    private void OnCollisionEnter(Collision collision)
    {
        Destroy(gameObject);
    }
}
