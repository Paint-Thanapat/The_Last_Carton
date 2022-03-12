using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestEnemySpell : MonoBehaviour
{
    float moveSpeed = 5f;

    Rigidbody2D rb2d;

    Transform player;

    Vector2 moveDirection;

    public bool[] topDownPlayer;


    void Awake()
    {
        topDownPlayer = new bool[3];
    }
    void Start()
    {

        rb2d = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        if (topDownPlayer[0])
        {
            moveDirection = (new Vector3(player.position.x, player.position.y - 5, player.position.z) - transform.position).normalized * moveSpeed;
        }
        if (topDownPlayer[1])
        {
            moveDirection = (player.position - transform.position).normalized * moveSpeed;
        }
        if (topDownPlayer[2])
        {
            moveDirection = (new Vector3(player.position.x, player.position.y + 5, player.position.z) - transform.position).normalized * moveSpeed;
        }
        rb2d.velocity = new Vector2(moveDirection.x, moveDirection.y);

        //transform.LookAt(player);

        //transform.rotation = Quaternion.Euler(0, 0,tr );

        Destroy(gameObject, 7);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject && !other.gameObject.GetComponent<Collider2D>().isTrigger)
        {
            Destroy(gameObject);
            //Debug.Log(other.transform.name);                           Check Name Other Gameobject
        }
    }
}
