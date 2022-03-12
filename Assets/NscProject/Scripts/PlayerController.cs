using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Spell && Cast
    public Sprite[] magic;
    public Sprite[] spell;

    public float spellForce = 20f;

    public GameObject magicCasting;
    Vector3 firstMagicScale;

    public GameObject spellPrefab;
    private Transform castPoint;

    public bool AngelLeft;                             // Check Rotation Point is Left or Right

    // Bool check
    public bool onGround;
    public bool isClimb;
    public bool canClimb;
    public bool isCastSpell;
    
    

    // Player's move speed & JumpForce
    private float moveSpeed = 6;
    private float JumpForce = 8;

    // Basic
    private Rigidbody2D rb2d;
    private Animator anim;

    void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

        castPoint = GameObject.FindGameObjectWithTag("RotationPoint").transform;
    }
    void Start()
    {
        firstMagicScale = magicCasting.transform.localScale;                    // Magic Scale
    }

    void Update()
    {
        Jump();

        ClimbLadderButton();

        CastSpell();

    }

    void FixedUpdate()
    {

        MoveHorizontal();
        
        ClimbLadder();

        RotationMagic();
    }

    void MoveHorizontal()
    {
        if (Input.GetKey(KeyCode.A))
        {

            transform.localScale = new Vector3(-0.15f, 0.15f, 1);
            Vector3 movement = new Vector3(-1, 0, 0);
            transform.position += movement * Time.deltaTime * moveSpeed;
            //transform.Translate(movement * Time.deltaTime * moveSpeed);

            anim.SetBool("isRun", true);

        }
        else if (Input.GetKey(KeyCode.D))
        {
            transform.localScale = new Vector3(0.15f, 0.15f, 1);
            Vector3 movement = new Vector3(1, 0, 0);
            transform.position += movement * Time.deltaTime * moveSpeed;

            anim.SetBool("isRun", true);
        }
        else
        {
            anim.SetBool("isRun", false);
        }
    }

    void Jump()
    {
        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.Space))
        {
            if (onGround && !isClimb)
            {
                rb2d.AddForce(new Vector2(0, JumpForce), ForceMode2D.Impulse);
                anim.SetTrigger("isJump");
            }
        }
    }

    void ClimbLadder()
    {
        if (isClimb)
        {
            if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.Space))
            {
                if (isClimb)
                {
                    rb2d.velocity = new Vector2(0, 3);
                    rb2d.gravityScale = 0;
                    anim.SetBool("isClimbing", true);
                }
            }
            else if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.LeftShift))
            {
                if (isClimb)
                {
                    rb2d.velocity = new Vector2(0, -3);
                    rb2d.gravityScale = 0;
                    anim.SetBool("isClimbing", true);
                }
            }
            else
            {
                rb2d.velocity = new Vector2(0, 0);
                rb2d.gravityScale = 0;
                anim.SetBool("isClimbing", false);
            }
        }
    }

    void ClimbLadderButton()
    {
        if(canClimb && !isClimb)
        {
            if(Input.GetKeyDown(KeyCode.E))
            {
                isClimb = true;
                anim.SetBool("isClimb", true);
                gameObject.layer = 13;
            }
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                isClimb = false;
                anim.SetBool("isClimb", false);
                rb2d.gravityScale = 1;
                gameObject.layer = 12;
            }
        }
    }

    void CastSpell()
    {
        if (Input.GetMouseButtonDown(0) && isCastSpell == false)
        {
            if (AngelLeft)
            {
                transform.localScale = new Vector3(-0.15f, 0.15f, 1);
            }
            else if (!AngelLeft)
            {
                transform.localScale = new Vector3(0.15f, 0.15f, 1);
            }

            RandomSpell();
            magicCasting.SetActive(true);
            isCastSpell = true;
            Invoke("Casted", 0.8f);

            anim.SetBool("isCast", true);

        }


    }

    void Casted()
    {
        // if (!gotDamage)
        
            GameObject spell = Instantiate(spellPrefab, castPoint.position, castPoint.rotation);
            Rigidbody2D rb2d = spell.GetComponent<Rigidbody2D>();
            rb2d.AddForce(castPoint.up * spellForce, ForceMode2D.Impulse);
        
        isCastSpell = false;
        magicCasting.transform.localScale = firstMagicScale;
        magicCasting.SetActive(false);
        anim.SetBool("isCast", false);
    }

    void RandomSpell()
    {
        int i = Random.Range(0, 2);
        magicCasting.GetComponent<SpriteRenderer>().sprite = magic[i];
        spellPrefab.GetComponent<SpriteRenderer>().sprite = spell[i];
    }                                         // Random Spell

    void RotationMagic()
    {
        magicCasting.transform.Rotate(0, 0, 3);

        if (magicCasting.activeSelf)
        {
            magicCasting.transform.localScale += Vector3.one / 20;
        }
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("Ladder"))
        {
            canClimb = true;
        }
    }
   
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Ladder"))
        {
            canClimb = false;
            isClimb = false;
            rb2d.gravityScale = 1;
            anim.SetBool("isClimb", false);
            gameObject.layer = 12;
        }
    }
}
