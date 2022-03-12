using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScriptSpellController : MonoBehaviour
{
    
    void Start()
    {
        Destroy(gameObject, 5);
    }
    
    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject && !other.gameObject.GetComponent<Collider2D>().isTrigger)
        {
            Destroy(gameObject);
        }
    }
    /*
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("BossSpell"))
        {
            Destroy(gameObject);
        }
    }
    */

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject)
        {
            Destroy(gameObject);
        }
    }
}
