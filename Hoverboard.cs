using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hoverboard : MonoBehaviour
{

    Rigidbody rb;
    public float moveForce, turnForce, multiplier;

    public Transform[] anchors = new Transform[4];
    RaycastHit[] hits = new RaycastHit[4];
    public LayerMask canFloatOn;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void FixedUpdate()
    {
        for (int i = 0; i < 4; i++)
        {       
            // Anchords send raycast down
            if (Physics.Raycast(anchors[i].position, Vector3.down, out hits[i], Mathf.Infinity, canFloatOn))
            {
                // Anchor pushes upwards the closer the ray is to the ground
                float anchorForce = Mathf.Abs(1 / (hits[i].point.y - anchors[i].position.y));
                rb.AddForceAtPosition(transform.up * anchorForce * multiplier, anchors[i].position, ForceMode.Acceleration);
            }
        }

        // Basic movement controls
        rb.AddForce(Input.GetAxis("Vertical") * moveForce * transform.forward, ForceMode.Acceleration);
        rb.AddTorque(Input.GetAxis("Horizontal") * turnForce * transform.up, ForceMode.Acceleration);
    }
}
