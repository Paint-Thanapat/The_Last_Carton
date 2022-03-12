using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScriptRotationSpellPoint : MonoBehaviour
{
    private ScriptPlayerController ScriptPlayerController;

    private GameObject Player;

    private Rigidbody2D rb2d;
    private Camera cam;

    Vector2 mousePos;

    void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
        Player = GameObject.FindGameObjectWithTag("Player");
        cam = GameObject.Find("Main Camera").GetComponent<Camera>();
        ScriptPlayerController = GameObject.FindGameObjectWithTag("Player").GetComponent<ScriptPlayerController>();
    }
    void Update()
    {
        if (ScriptPlayerController.isCastSpell == false)
        {
            mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
        }

        gameObject.transform.position = new Vector2(Player.transform.position.x, Player.transform.position.y + 1.5f);
    }
    void FixedUpdate()
    {
        RotatePoint();
    }
    void RotatePoint()
    {
        Vector2 lookDir = mousePos - rb2d.position;
        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg - 90f;
        rb2d.rotation = angle;
       // Debug.Log(angle);
        if (angle <= 0 && angle > -180)
        {
            ScriptPlayerController.AngelLeft = false;
        }
        else
        {
            ScriptPlayerController.AngelLeft = true;
        }
    }
}
