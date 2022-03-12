using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScriptShootingSpell : MonoBehaviour
{
    public ScriptPlayerController ScriptPlayerController;

    public Transform castPoint;
    public GameObject spellPrefab;

    public float spellForce = 20f;

    void Awake()
    {

    }

    void Update()
    {
        if(Input.GetMouseButtonDown(0) && ScriptPlayerController.isCastSpell == false)
        {
            Invoke("cast", 1);
        }
    }

    void cast()
    {
        GameObject spell = Instantiate(spellPrefab, castPoint.position, castPoint.rotation);
        Rigidbody2D rb2d = spell.GetComponent<Rigidbody2D>();
        rb2d.AddForce(castPoint.up * spellForce, ForceMode2D.Impulse);
    }
}
