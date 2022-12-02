using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hoverboard : MonoBehaviour
{
    private Rigidbody rb;
    private RaycastHit[] hits = new RaycastHit[4];
    private float moveInput;
    private float rotateInput;

    [SerializeField] private float moveForce, turnForce, multiplier;
    [SerializeField] private Transform[] anchors = new Transform[4];
    [SerializeField] private LayerMask canFloatOn;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        moveInput = Input.GetAxis("Vertical");
        rotateInput = Input.GetAxis("Horizontal");
    }

    void FixedUpdate()
    {
        for (int i = 0; i < 4; i++)
        {       
            //Anchors send raycast down
            if (Physics.Raycast(anchors[i].position, Vector3.down, out hits[i], Mathf.Infinity, canFloatOn))
            {
                // Anchor pushes upwards the closer the ray is to the ground
                float anchorForce = Mathf.Abs(1 / (hits[i].point.y - anchors[i].position.y));
                rb.AddForceAtPosition(transform.up * anchorForce * multiplier, anchors[i].position, ForceMode.Acceleration);
            }
        }

        //Movement through force
        rb.AddForce(moveInput * moveForce * transform.forward, ForceMode.Acceleration);

        //Rotation through torque
        rb.AddTorque(rotateInput * turnForce * transform.up, ForceMode.Acceleration);
    }
}
