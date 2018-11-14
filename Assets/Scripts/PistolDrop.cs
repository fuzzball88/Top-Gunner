using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PistolDrop : MonoBehaviour {
    public GameObject gun; 

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Dude player = other.gameObject.GetComponent<Dude>();
            player.weapon = gun;
            player.EquipWeapon();
        }
        Destroy(gameObject, 1f);
    }

}
