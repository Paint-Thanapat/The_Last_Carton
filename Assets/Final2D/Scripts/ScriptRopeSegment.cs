using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScriptRopeSegment : MonoBehaviour
{

    public GameObject connectedAbove, connentedBelow;
    public bool isPlayerAttached;
    void Start()
    {
        ResetAnchor();
    }

    void ResetAnchor()
    {
        connectedAbove = GetComponent<HingeJoint2D>().connectedBody.gameObject;
        TestRopeSegment aboveSegment = connectedAbove.GetComponent<TestRopeSegment>();
        if (aboveSegment != null)
        {
            aboveSegment.connentedBelow = gameObject;
            float spriteButton = connectedAbove.GetComponent<SpriteRenderer>().bounds.size.y;
            GetComponent<HingeJoint2D>().connectedAnchor = new Vector2(0, spriteButton * -1);
        }
        else
        {
            GetComponent<HingeJoint2D>().connectedAnchor = new Vector2(0, 0);
        }
    }
}
