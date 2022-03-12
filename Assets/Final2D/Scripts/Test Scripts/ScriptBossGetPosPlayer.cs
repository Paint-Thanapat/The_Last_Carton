using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScriptBossGetPosPlayer : MonoBehaviour
{
    private GameObject Boss;

    private Rigidbody2D rb2d;
    private Camera cam;

    Vector2 PlayerPos;

    Transform PlayerTarget;

    void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
        Boss = GameObject.FindGameObjectWithTag("Boss1");
        cam = GameObject.Find("Main Camera").GetComponent<Camera>();

        PlayerTarget = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }
    void Update()
    {
        PlayerPos = PlayerTarget.position;

        gameObject.transform.position = new Vector2(Boss.transform.position.x, Boss.transform.position.y + 1.5f);
    }
    void FixedUpdate()
    {
        RotatePoint();
    }
    void RotatePoint()
    {
        Vector2 lookDir = PlayerPos - rb2d.position;
        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg - 90f;
        rb2d.rotation = angle;
    }
}
