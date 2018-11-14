using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pistol : MonoBehaviour {
    public AudioClip shoot;
    public GameObject bullet;
    public GameObject firePlace;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
        Debug.Log(firePlace);
	}
    void Fire()
    {
        Instantiate(bullet, firePlace.transform.position, gameObject.transform.rotation * bullet.transform.rotation);
        AudioSource.PlayClipAtPoint(shoot, transform.position);
    }
}
