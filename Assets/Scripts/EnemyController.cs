using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public Transform eyesTransform;

    public Transform target;

    public bool targetSeen = false;

    public float fov = 60f;
    public float viewDistance = 50f;

    public LayerMask viewMask;

    Vector3 targetPosition;
    Vector3 targetDirection;
    Vector3 viewDirection;

    public float rotationSpeed = 90f;
    public float movementSpeed = 1f;

    CharacterController controller;

    void Awake()
    {
        controller = GetComponent<CharacterController>();
    }

    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        CheckIfTargetVisible();

        if (targetSeen)
        {
            //Immediate rotation
            transform.forward = targetDirection;

            transform.forward = Vector3.RotateTowards(transform.forward, targetDirection, Mathf.Deg2Rad * rotationSpeed * Time.deltaTime, 0f);

            controller.SimpleMove(targetDirection * movementSpeed);
        }
    }

    void CheckIfTargetVisible()
    {
        targetPosition = target.position;
        targetPosition.y = eyesTransform.position.y;

        viewDirection = eyesTransform.forward;
        targetDirection = (targetPosition - eyesTransform.position).normalized;

        //Debug.DrawRay(eyesTransform.position, viewDirection * viewDistance, Color.green);
        //Debug.DrawRay(eyesTransform.position, Quaternion.AngleAxis(fov / 2f, Vector3.up) * viewDirection * viewDistance, Color.red);
        //Debug.DrawRay(eyesTransform.position, Quaternion.AngleAxis(-fov / 2f, Vector3.up) * viewDirection * viewDistance, Color.red);

        float targetDistance = Vector3.Distance(eyesTransform.position, targetPosition);

        if (targetDistance < viewDistance)
        {
            float targetAngle = Vector3.Angle(viewDirection, targetDirection);

            if (targetAngle < fov / 2f)
            {
                bool hitSomething = Physics.Linecast(eyesTransform.position, target.position + Vector3.up * 1.5f, viewMask);

                if (!hitSomething)
                {
                    targetSeen = true;
                    return;
                }
            }
        }

        targetSeen = false;
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawRay(eyesTransform.position, targetDirection);

        Gizmos.color = Color.green;
        Gizmos.DrawRay(eyesTransform.position, viewDirection * viewDistance);

        Gizmos.color = Color.red;
        Gizmos.DrawRay(eyesTransform.position, Quaternion.AngleAxis(fov / 2f, Vector3.up) * viewDirection * viewDistance);
        Gizmos.DrawRay(eyesTransform.position, Quaternion.AngleAxis(-fov / 2f, Vector3.up) * viewDirection * viewDistance);
    }
}
