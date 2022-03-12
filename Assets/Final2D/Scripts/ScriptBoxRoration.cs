using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScriptBoxRoration : MonoBehaviour
{
    bool ran;
    float randomRo;
    bool RoBack;

    void Update()
    {
        if(gameObject.GetComponent<Collider2D>().isTrigger)
        {
            if (!ran)
            {
                randomRo = Random.Range(-0.6f, 0.6f);
                ran = true;
                RoBack = false;
            }
            transform.Rotate(0, 0, randomRo);
        }
        else
        {
            ran = false;
            if(!RoBack)
            {
                transform.rotation = new Quaternion(0, 0, 0, 0);
                RoBack = true;
            }
        }
    }
}
