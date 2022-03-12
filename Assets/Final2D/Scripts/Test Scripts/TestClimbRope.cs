using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestClimbRope : MonoBehaviour
{
    public Rigidbody2D rb2d;
    private HingeJoint2D hj2d;

    public float pushForce = 10f;

    public bool attached;
    public Transform attachTo;
    private GameObject disregard;

    public GameObject pulleySelected = null;

    void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
        hj2d = GetComponent<HingeJoint2D>();
    }

    void Update()
    {
        CheckKeyBoardInputs();
    }

    void CheckKeyBoardInputs()
    {
        if(Input.GetKey(KeyCode.A))
        {
            if(attached)
            {
                rb2d.AddRelativeForce(new Vector3(-1, 0, 0) * pushForce);
            }
        }
        if (Input.GetKey(KeyCode.D))
        {
            if (attached)
            {
                rb2d.AddRelativeForce(new Vector3(1, 0, 0) * pushForce);
            }
        }

        if(Input.GetKeyDown(KeyCode.W) && attached)
        {

        }
        if (Input.GetKeyDown(KeyCode.S) && attached)
        {

        }
        if(Input.GetKeyDown(KeyCode.Space))
        {
            Detach();
        }
    }

    public void Attach(Rigidbody2D ropeBone)
    {
        ropeBone.gameObject.GetComponent<TestRopeSegment>().isPlayerAttached = true;
        hj2d.connectedBody = ropeBone;
        hj2d.enabled = true;
        attached = true;
        attachTo = ropeBone.gameObject.transform.parent;


    }

    void Detach()
    {
        hj2d.connectedBody.gameObject.GetComponent<TestRopeSegment>().isPlayerAttached = false;
        attached = false;
        hj2d.enabled = false;
        hj2d.connectedBody = null;
        Invoke("resetRope", 1);
    }

    void resetRope()
    {
        attachTo = null;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(!attached)
        {
            if(other.gameObject.CompareTag("Rope"))
            {
                if(attachTo != other.gameObject.transform.parent)
                {
                    if(disregard == null || other.gameObject.transform.parent.gameObject != disregard)
                    {
                        Attach(other.gameObject.GetComponent<Rigidbody2D>());
                    }
                }
            }
        }
    }
}
