using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dude : MonoBehaviour {
    public float playerSpeed = 10f;
    public float rotationSpeed = 100f;
    public float fireRate = 5f;

    public int health = 5;

    public float movehorizontal;
    public float movevertical;

    public Vector3 moveInput;
    public Vector3 moveVelocity;

    private Rigidbody rb;

    private Camera mainCamera;

    public AudioClip shoot;
    public AudioClip death;
    public AudioClip takeDamage;
    public AudioClip emptyHand;

    public GameObject weapon;
    public GameObject noWeapon;
    public GameObject rightHand;

    // Use this for initialization
    void Start () {
        rightHand = GameObject.FindGameObjectWithTag("PlayerShotPlace");
        rb = GetComponent<Rigidbody>();
        mainCamera = FindObjectOfType<Camera>();
	}
	
	// Update is called once per frame
	void Update () {
        
        LookAt();
        Shoot();
    }

    private void FixedUpdate()
    {
        Movement();
    }

    void Movement()
    {

        movehorizontal = Input.GetAxisRaw("Horizontal");
        movevertical = Input.GetAxisRaw("Vertical");
        Vector3 movement = new Vector3(movehorizontal * playerSpeed * Time.deltaTime, 0, movevertical * playerSpeed * Time.deltaTime);

        rb.MovePosition(transform.position+movement);
        /*
        if (Input.GetKey(KeyCode.UpArrow))
        {
            transform.Translate(new Vector3(0,0,1) * playerSpeed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            transform.Translate(new Vector3(0, 0,-1) * playerSpeed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            transform.Translate(Vector3.left * playerSpeed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.Translate(Vector3.right * playerSpeed * Time.deltaTime);
        }
        */
    }

    void LookAt()
    {
        Ray cameraRay = mainCamera.ScreenPointToRay(Input.mousePosition);
        Plane groundPlane = new Plane(Vector3.up, Vector3.zero);
        float rayLength;

        if(groundPlane.Raycast(cameraRay, out rayLength))
        {
            Vector3 PointToLook = cameraRay.GetPoint(rayLength);
            Debug.DrawLine(cameraRay.origin, PointToLook, Color.blue);

            transform.LookAt(new Vector3(PointToLook.x, transform.position.y,PointToLook.z));
        }
    }

    void Shoot()
    {
        if (Input.GetMouseButtonDown(0))
        {
            InvokeRepeating("FireProjectile", 0.000001f, fireRate);

        }
        if (Input.GetMouseButtonUp(0))
        {
            CancelInvoke("FireProjectile");
        }
    }

    void FireProjectile()
    {
        if (weapon == null) {
            AudioSource.PlayClipAtPoint(emptyHand, transform.position);
            Instantiate(noWeapon, rightHand.transform.position, gameObject.transform.rotation * noWeapon.transform.rotation);
        }
        else { 
        Instantiate(weapon, rightHand.transform.position, gameObject.transform.rotation * weapon.transform.rotation);
        AudioSource.PlayClipAtPoint(shoot, transform.position);
        }
    }

    void LoseHealth(int damage)
    {
        AudioSource.PlayClipAtPoint(takeDamage, transform.position);
        health -= damage;
        if (health < 0)
        {
            Die();
        }
    }

    void Die()
    {
        AudioSource.PlayClipAtPoint(death, transform.position);
        Destroy(this);
    }
}
