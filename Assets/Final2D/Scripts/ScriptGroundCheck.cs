using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScriptGroundCheck : MonoBehaviour
{
    public ScriptPlayerController ScriptPlayerController;

    public GameObject Player;

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Ground") || other.gameObject.CompareTag("Block") || other.gameObject.CompareTag("Block12") || other.gameObject.CompareTag("Blockw2"))
        {
            if (!other.gameObject.GetComponent<Collider2D>().isTrigger)
            {
                ScriptPlayerController.onGround = true;
                ScriptPlayerController.GetComponent<Animator>().SetBool("isGround", true);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Ground") || other.gameObject.CompareTag("Block") || other.gameObject.CompareTag("Block12") || other.gameObject.CompareTag("Blockw2"))
        {
            if (!other.gameObject.GetComponent<Collider2D>().isTrigger)
            {
                ScriptPlayerController.onGround = false;
                ScriptPlayerController.GetComponent<Animator>().SetBool("isGround", false);
            }
        }
    }
}
