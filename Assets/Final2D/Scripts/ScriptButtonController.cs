using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScriptButtonController : MonoBehaviour
{
    public GameObject DoorPrefabs;

    public bool isOpen;
    public bool Onbutton;
    public bool canChangetoDoor;
    public bool PlayerOnButton;
    Vector3 firstPosition;

    private SpriteRenderer spriteRenderer;
    public Sprite buttonUp;
    public Sprite buttonDown;

    private ScriptCameraController scriptCameraController;

    public float maxOpen;

    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        scriptCameraController = GameObject.Find("Main Camera").GetComponent<ScriptCameraController>();
    }
    void Start()
    {
        firstPosition = DoorPrefabs.transform.position;
        spriteRenderer.sprite = buttonUp;
    }

    void Update()
    {
        if(firstPosition.y <= DoorPrefabs.transform.position.y - maxOpen)
        {
            isOpen = true;
        }
        else if (firstPosition.y > DoorPrefabs.transform.position.y - maxOpen)
        {
            isOpen = false;
        }

        if (firstPosition.y >= DoorPrefabs.transform.position.y)
        {
            
        }
        else if(Onbutton == false)
        {
            DoorPrefabs.transform.position = new Vector3(DoorPrefabs.transform.position.x, DoorPrefabs.transform.position.y - 2 * Time.deltaTime, DoorPrefabs.transform.position.z);
        }
        #region command
        /*
        if (canChangetoDoor)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                if (Onbutton)
                {
                    scriptCameraController.onbutton = true;
                }
                else
                {
                    scriptCameraController.onbutton = false;
                }
            }
        }
        else
        {
            scriptCameraController.onbutton = false;
        }
        */
        #endregion

        if (Input.GetKeyDown(KeyCode.E) && Onbutton && PlayerOnButton)
        {
            if (!canChangetoDoor)
            {
                canChangetoDoor = true;
                scriptCameraController.onbutton = true;
            }
            else
            {
                canChangetoDoor = false;
                scriptCameraController.onbutton = false;
            }
        }
    }

    private void FixedUpdate()
    {
        if (canChangetoDoor && PlayerOnButton)
        {
            scriptCameraController.WoodCam(DoorPrefabs.transform);
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {

    }



    private void OnCollisionStay2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Block") || other.gameObject.CompareTag("Player") || other.gameObject.CompareTag("Block12") || other.gameObject.CompareTag("Blockw2"))
        {
            if (isOpen == false)
            {
                Onbutton = true;
                spriteRenderer.sprite = buttonDown;
                DoorPrefabs.transform.position = new Vector3(DoorPrefabs.transform.position.x, DoorPrefabs.transform.position.y + 2 * Time.deltaTime, DoorPrefabs.transform.position.z);
            }
        }

        if (other.gameObject.CompareTag("Player"))
        {
            PlayerOnButton = true;
        }

    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Block") || other.gameObject.CompareTag("Player") || other.gameObject.CompareTag("Block12") || other.gameObject.CompareTag("Blockw2"))
        {
            Debug.Log("OutButton");
            Onbutton = false;
            spriteRenderer.sprite = buttonUp;
        }

        if (other.gameObject.CompareTag("Player"))
        {
            canChangetoDoor = false;
            PlayerOnButton = false;
            scriptCameraController.onbutton = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Block") || other.gameObject.CompareTag("Player") || other.gameObject.CompareTag("Block12") || other.gameObject.CompareTag("Blockw2"))
        {
            if (!other.gameObject.GetComponent<Collider2D>().isTrigger)
            {
                Debug.Log("onButton");
                Onbutton = true;
            }
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Block") || other.gameObject.CompareTag("Player") || other.gameObject.CompareTag("Block12") || other.gameObject.CompareTag("Blockw2"))
        {
            if (!other.gameObject.GetComponent<Collider2D>().isTrigger)
            {
                if (isOpen == false)
                {
                    Onbutton = true;
                    spriteRenderer.sprite = buttonDown;
                    DoorPrefabs.transform.position = new Vector3(DoorPrefabs.transform.position.x, DoorPrefabs.transform.position.y + 2 * Time.deltaTime, DoorPrefabs.transform.position.z);
                }
            }
            else if (other.gameObject.GetComponent<Collider2D>().isTrigger)
            {
                 Onbutton = false;
                 spriteRenderer.sprite = buttonUp;
            }

            if (other.gameObject.CompareTag("Player"))
            {
                PlayerOnButton = true;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Block") || other.gameObject.CompareTag("Player") || other.gameObject.CompareTag("Block12") || other.gameObject.CompareTag("Blockw2"))
        {
            Debug.Log("OutButton");
            Onbutton = false;
            spriteRenderer.sprite = buttonUp;
        }

        if (other.gameObject.CompareTag("Player"))
        {
            canChangetoDoor = false;
            PlayerOnButton = false;
            scriptCameraController.onbutton = false;
        }
    }
    private void outButton()
    {
            scriptCameraController.onbutton = false;
    }

}
