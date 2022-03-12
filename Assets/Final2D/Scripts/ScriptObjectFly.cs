using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScriptObjectFly : MonoBehaviour
{
    Vector3 firstPosition;

    bool isUp;

    void Start()
    {
        firstPosition = transform.position;
    }

    void Update()
    {
        if(transform.position.y - 0.5f >= firstPosition.y)
        {
            isUp = false;
        }
        else if(transform.position.y + 0.5f < firstPosition.y)
        {
            isUp = true;
        }

        if(isUp)
        {
            transform.position += new Vector3(0, 1) * Time.deltaTime;
        }
        else
        {
            transform.position += new Vector3(0, -1) * Time.deltaTime;
        }
    }
}
