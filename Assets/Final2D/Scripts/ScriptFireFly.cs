using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScriptFireFly : MonoBehaviour
{

    float moveSpeed;
    int moveX;
    int moveY;
    float Xspeed;
    float Yspeed;

    float scale;

    float c = 0.01f;
    bool isDone;

    bool maxLighting;
    SpriteRenderer spriteRenderer;
    void Awake()
    {
        scale = Random.Range(1, 2.1f);

        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    void Start()
    {
        StartCoroutine("changeMove");
        moveX = Random.Range(1, 5);
        moveY = Random.Range(1, 5);

        Xspeed = 1;
        Yspeed = 1;
        moveSpeed = 1;

        transform.localScale = new Vector3(scale, scale, scale);

        isDone = false;
        Invoke("FFDone", 15);
    }

    void Update()
    {
        if(moveX >= 3)
        {
            transform.position += new Vector3(Xspeed, 0) * moveSpeed * Time.deltaTime;
        }
        else
        {
            transform.position += new Vector3(-Xspeed, 0) * moveSpeed * Time.deltaTime;
        }

        if (moveY >= 3)
        {
            transform.position += new Vector3(0, Yspeed) * moveSpeed * Time.deltaTime;
        }
        else
        {
            transform.position += new Vector3(0, -Yspeed) * moveSpeed * Time.deltaTime;
        }

        if(isDone)
        {
            c -= 0.2f * Time.deltaTime;
        }

        if(c <= 0)
        {
            Destroy(gameObject);
        }

        spriteRenderer.color = new Color(1, 1, 1, c);

        if (!maxLighting)
        {
            c += 0.4f * Time.deltaTime;
        }

        if (c >= 1)
        {
            maxLighting = true;
        }
    }

    void FFDone()
    {
        isDone = true;
    }
    IEnumerator changeMove()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(0.5f,2));
            moveX = Random.Range(1, 5);
            moveY = Random.Range(1, 5);

            Xspeed = Random.Range(0, 1.5f);
            Yspeed = Random.Range(0, 1.5f);
        } 
    }

    IEnumerator changeSpeed()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(0.2f, 3));
            moveSpeed = Random.Range(0.4f, 1.6f);


        }    
    }
}
