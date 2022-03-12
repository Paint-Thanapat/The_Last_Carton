using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationPoint : MonoBehaviour
{
    private PlayerController scriptPlayerController;
    private GameObject Player;
    private Rigidbody2D rb2d;
    private Camera cam;

    Vector2 mousePos;
    void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
        Player = GameObject.FindGameObjectWithTag("Player");
        cam = GameObject.Find("Main Camera").GetComponent<Camera>();
        scriptPlayerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
    }

    void Update()
    {
        if (scriptPlayerController.isCastSpell == false)
        {
            mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
        }

        gameObject.transform.position = new Vector2(Player.transform.position.x, Player.transform.position.y + 1.5f);
    }

    void FixedUpdate()
    {
        Vector2 lookDir = mousePos - rb2d.position;
        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg - 90f;
        rb2d.rotation = angle;
        Debug.Log(angle);
        if (angle <= 0 && angle > -180)
        {
            scriptPlayerController.AngelLeft = false;
        }
        else
        {
            scriptPlayerController.AngelLeft = true;
        }
    }
}
