using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScriptBGMove : MonoBehaviour
{
    public float newPos;
    public float speed;
    private void Update()
    {
        transform.position += new Vector3(-1, 0, 0) * speed * Time.deltaTime;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if(other.CompareTag("OutScene"))
        {
            Debug.Log("sssssssssssssss");
            transform.position += new Vector3(40
                , 0, 0);
        }
    }
}
