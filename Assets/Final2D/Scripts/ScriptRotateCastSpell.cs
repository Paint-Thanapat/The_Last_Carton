using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScriptRotateCastSpell : MonoBehaviour
{

    public GameObject Player;

    Vector3 firstScale;

    void Start()
    {
        firstScale = transform.localScale;
    }


    void FixedUpdate()
    {
        gameObject.transform.Rotate(0, 0, 3);

        if(gameObject.activeSelf)
        {
            gameObject.transform.localScale += Vector3.one / 20;
        }

    }

    void OnDisable()
    {
        transform.localScale = firstScale;
    }
}
