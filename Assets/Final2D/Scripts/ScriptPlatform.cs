using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScriptPlatform : MonoBehaviour
{
    public GameObject Player;

    private BoxCollider2D boxcollider2D;

    void Awake()
    {
        boxcollider2D = GetComponent<BoxCollider2D>();
    }
    void Start()
    {
        
    }


    void Update()
    {
        if(gameObject.transform.position.y > Player.transform.position.y -1.2f)
        {
            boxcollider2D.enabled = false;
        }
        else if(gameObject.transform.position.y <= Player.transform.position.y -1.2f)
        {
            boxcollider2D.enabled = true;
        }
    }
}
