using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScriptEnemyController : MonoBehaviour
{
    private GameObject Player;

    private ScriptCameraController scriptCameraController;

    public bool isleft;
    public bool isright;
    public bool isDead;
    public bool seePlayer;

    private float moveSpeed = 2f;

    Vector3 StartPosition;

    [HideInInspector]public int HP = 2;
    private SpriteRenderer spriteRenderer;
    private Animator anim;

    float scaleX, scaleY;
    public float maxLeft, maxRight;

    void Awake()
    {
        //Find Player by coding
        Player = GameObject.FindGameObjectWithTag("Player");
        scriptCameraController = GameObject.Find("Main Camera").GetComponent<ScriptCameraController>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
    }
    void Start()
    {
        isleft = true;
        isDead = false;

        transform.localScale = new Vector3(0.25f, 0.25f, 1);
        Vector3 movement = new Vector3(1, 0, 0);
        transform.position += movement * Time.deltaTime * moveSpeed;

        StartPosition = gameObject.transform.position;

        scaleX = transform.localScale.x;
        scaleY = transform.localScale.y;
    }

    void FixedUpdate()
    {
        if (!isDead && scriptCameraController.isChanging == false)
        {
            EnemyMove();
        }

        if(HP <= 0)
        {
            isDead = true;
            spriteRenderer.color = new Color(1, 0, 0, 1);
            Invoke("SetFalse", 2);
            anim.SetBool("isDead", true);
        }

        if (transform.position.x > StartPosition.x + maxRight)
        {
            isleft = true;
            isright = false;
        }
        if (transform.position.x < StartPosition.x - maxLeft)
        {
            isright = true;
            isleft = false;
        }
    }

    void SetFalse()
    {
        gameObject.SetActive(false);
    }

    void EnemyMove()
    {
        if (seePlayer == true)
        {
            if (transform.position.x <= Player.transform.position.x)
            {
                transform.localScale = new Vector3(-scaleX, scaleY, 1);
                Vector3 movement = new Vector3(1, 0, 0);
                transform.position += movement * Time.deltaTime * moveSpeed;
            }
            else if (transform.position.x > Player.transform.position.x)
            {
                transform.localScale = new Vector3(scaleX, scaleY, 1);
                Vector3 movement = new Vector3(-1, 0, 0);
                transform.position += movement * Time.deltaTime * moveSpeed;
            }
        }
        else if (seePlayer == false)
        {
            if (isright == true)
            {
                transform.localScale = new Vector3(-scaleX, scaleY, 1);
                Vector3 movement = new Vector3(1, 0, 0);
                transform.position += movement * Time.deltaTime * moveSpeed;
            }
            else if (isleft == true)
            {
                transform.localScale = new Vector3(scaleX, scaleY, 1);
                Vector3 movement = new Vector3(-1, 0, 0);
                transform.position += movement * Time.deltaTime * moveSpeed;
            }
        }
    }



    private void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.CompareTag("Bullet"))
        {
            HP--;
        }

        if (other.gameObject.CompareTag("Player"))
        {
            anim.SetTrigger("isAtk");
        }
    }

    private void OnCollisionStay2D(Collision2D other)
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        
    }
}
