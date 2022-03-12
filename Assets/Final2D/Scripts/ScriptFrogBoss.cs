using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScriptFrogBoss : MonoBehaviour
{
    [SerializeField] private float jumpForce;
    [SerializeField] private float moveSpeed;
    public bool onGround;

    private Rigidbody2D rb2d;

    void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        StartCoroutine("timeJump");

        onGround = true;
    }

    void Update()
    {
        if(!onGround)
        {
            transform.position += new Vector3(1, 0, 0) * moveSpeed * Time.deltaTime;
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.CompareTag("Ground"))
        {
            onGround = true;
        }

        if(other.gameObject)
        {
            if(!other.gameObject.CompareTag("Ground") && !other.gameObject.CompareTag("Player") && !other.gameObject.CompareTag("Boss1"))
            {
                Destroy(other.gameObject);
            }
        }
    }

    IEnumerator timeJump()
    {
        while(true)
        {
            yield return new WaitForSeconds(5);

            onGround = false;
            rb2d.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);

        }
    }
}
