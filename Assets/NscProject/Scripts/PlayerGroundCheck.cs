using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGroundCheck : MonoBehaviour
{
    public PlayerController scriptPlayerController;

    private void OnTriggerStay2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("Ground"))
        {
            scriptPlayerController.onGround = true;
            scriptPlayerController.GetComponent<Animator>().SetBool("isGround", true);
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            scriptPlayerController.onGround = false;
            scriptPlayerController.GetComponent<Animator>().SetBool("isGround", false);
        }
    }
}
