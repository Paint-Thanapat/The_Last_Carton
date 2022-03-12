using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScriptPlayerController : MonoBehaviour
{

    //CheckPoint
    public Vector3 savePosition;

    private bool isDead;

    //Support Item
    private float scaleX, scaleY;


    //Climb Rope
    private HingeJoint2D hj2d;

    public float pushForce = 10f;

    public bool attached;
    public Transform attachTo;
    private GameObject disregard;

    public GameObject pulleySelected = null;

    bool setRopeRotation;
    bool setInRope;
    //Casting
    public GameObject SpellCasting;

    private Transform castPoint;
    public GameObject spellPrefab;

    public float spellForce = 20f;

    public bool AngelLeft;
    //Walk & Jump
    private float JumpForce = 8f;
    private float HorizontalSpeed = 6f;
    //Check
    public bool canLadder = false;
    public bool isLadder = false;
    public bool onGround = false;
    public bool isCastSpell = false;
    public bool gotDamage = false;

    public bool intheBlock;

    public bool isGotPotion1;

    private Rigidbody2D rb2d;
    private SpriteRenderer spriteRenderer;
    //HP
    private ScripteUIController scripteUIController;
    private ScriptCameraController scriptCameraController;
    public GameObject[] Heart;

    public int maxHP;
    public int nowHP;

    public bool isDamage;

    public Sprite[] cast;
    public Sprite[] spell;

    private Animator anim;

    //Sound
    public AudioClip WalkSound;
    public AudioClip CastSound;
    public AudioClip HitSound;
    public AudioClip JumpSound;

    //Book
    public GameObject magicBook;
    Vector3 bookFirstPosition;
    Vector3 bookCastPosition;
    public bool goUp;
    private float bookSpeed = 0.6f;

    void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();

        hj2d = GetComponent<HingeJoint2D>();

        SpellCasting.SetActive(false);

        castPoint = GameObject.FindGameObjectWithTag("CheckRotation").transform;

        scripteUIController = GameObject.Find("UIcontroller").GetComponent<ScripteUIController>();
        scriptCameraController = GameObject.Find("Main Camera").GetComponent<ScriptCameraController>();

        maxHP = Heart.Length;
        nowHP = maxHP;

        Time.timeScale = 1;

        scaleX = transform.localScale.x;
        scaleY = transform.localScale.y;

        savePosition = transform.position;
    }

    void Update()
    {
        if (isCastSpell == false && gotDamage == false && !attached && !hj2d.enabled)
        {
            Jump();
            ClimbLadder();
        }

        if (onGround == true && !scripteUIController.isPause && !gotDamage && !isDamage && !hj2d.enabled)
        {
            CastSpell();
        }

        if (nowHP > maxHP)
        {
            nowHP--;
        }
        if (nowHP <= 0)
        {
            isDead = true;
            gotDamage = true;
            Invoke("Respawn", 2);                                                                                    //DEADDDDDDDDDDDDDDd!!!!!!!!!
            anim.SetBool("isDead", true);
        }

        if (anim.GetBool("isRun") && onGround)
        {
            ScriptSoundsManager.instance.walkSource.volume = 1;
        }
        else if (!anim.GetBool("isRun") || !onGround)
        {
            ScriptSoundsManager.instance.walkSource.volume = 0;
        }

        ClimbLadderButton();

        addForceRope();

        setHeart();

        GameMode();

        setRotationRope();
    }

    void FixedUpdate()
    {
        if (isCastSpell == false && gotDamage == false && !attached)
        {
            MoveHorizontal();
        }
        else
        {
            anim.SetBool("isRun", false);
        }

        BookController();
    }

    void MoveHorizontal()
    {
        if (Input.GetKey(KeyCode.A))
        {

            transform.localScale = new Vector3(-scaleX, scaleY, 1);
            Vector3 movement = new Vector3(-1, 0, 0);
            transform.position += movement * Time.deltaTime * HorizontalSpeed;
            //transform.Translate(movement * Time.deltaTime * HorizontalSpeed);

            anim.SetBool("isRun", true);

        }
        else if (Input.GetKey(KeyCode.D))
        {
            transform.localScale = new Vector3(scaleX, scaleY, 1);
            Vector3 movement = new Vector3(1, 0, 0);
            transform.position += movement * Time.deltaTime * HorizontalSpeed;

            anim.SetBool("isRun", true);
        }
        else
        {
            anim.SetBool("isRun", false);
        }

        if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.A))
        {
                ScriptSoundsManager.instance.PlayWalk(WalkSound);
        }

    }

    void Jump()
    {
        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.Space))
        {
            if (onGround == true && isLadder == false)
            {
                rb2d.AddForce(new Vector2(0, JumpForce), ForceMode2D.Impulse);
                anim.SetTrigger("isJump");

                ScriptSoundsManager.instance.PlaySingle(JumpSound);
            }
        }
    }

    void ClimbLadder()
    {
        if(Input.GetKey(KeyCode.W) && isLadder == true)
        {
            rb2d.velocity = new Vector2(0, 3);
            rb2d.gravityScale = 0;

            anim.SetBool("isClimbing", true);
        }
        else if (Input.GetKey(KeyCode.Space) && isLadder == true)
        {
            rb2d.velocity = new Vector2(0, 3);
            rb2d.gravityScale = 0;

            anim.SetBool("isClimbing", true);
        }
        else if(Input.GetKey(KeyCode.S) && isLadder == true)
        {
            rb2d.velocity = new Vector2(0, -3);
            rb2d.gravityScale = 0;

            anim.SetBool("isClimbing", true);
        }
        else if (Input.GetKey(KeyCode.LeftShift) && isLadder == true)
        {
            rb2d.velocity = new Vector2(0, -3);
            rb2d.gravityScale = 0;

            anim.SetBool("isClimbing", true);
        }
        else if(isLadder == true)
        {
            rb2d.velocity = new Vector2(0, 0);
            rb2d.gravityScale = 0;

            anim.SetBool("isClimbing", false);
        }
    }

    void ClimbLadderButton()
    {
        if (canLadder && !isLadder)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                isLadder = true;
                anim.SetBool("isClimb", true);
                gameObject.layer = 16;
            }
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                isLadder = false;
                anim.SetBool("isClimb", false);
                rb2d.gravityScale = 1;
                gameObject.layer = 12;
            }
        }
    }

    void CastSpell()
    {
        if(Input.GetMouseButtonDown(0) && isCastSpell == false)
        {
            if(AngelLeft)
            {
                transform.localScale = new Vector3(-scaleX, scaleY, 1);
            }
            else if (!AngelLeft)
            {
                transform.localScale = new Vector3(scaleX, scaleY, 1);
            }

            RandomSpell();
            SpellCasting.SetActive(true);
            isCastSpell = true;
            Invoke("Casted", 0.8f);

            anim.SetBool("isCast", true);

            ScriptSoundsManager.instance.PlaySingle(CastSound);
        }
    }

    void Casted()
    {
        if (!gotDamage)
        {
            GameObject spell = Instantiate(spellPrefab, castPoint.position, castPoint.rotation);
            Rigidbody2D rb2d = spell.GetComponent<Rigidbody2D>();
            rb2d.AddForce(castPoint.up * spellForce, ForceMode2D.Impulse);
        }
            isCastSpell = false;
            SpellCasting.SetActive(false);
            anim.SetBool("isCast", false);
    }

    void ChangeLayer()
    {
        if(transform.position.y > 1.9f)
        {
            gameObject.layer = 9;
        }
        else
        {
            gameObject.layer = 8;
        }
    }

    void DamageTaken()
    {
        isDamage = false;

        StopCoroutine("gotDamageFuncSprite");
        spriteRenderer.color = new Color(1, 1, 1, 1);
    }
    void gotDamageFunc()
    {
        gotDamage = false;
        //StopCoroutine("gotDamageFuncSprite");
        anim.SetBool("isHit", false);
        //spriteRenderer.color = new Color(1, 1, 1, 1);
    }
    void gotDamageFunc2()
    {
        //StopCoroutine("gotDamageFuncSprite");
        anim.SetBool("isHit", false);
        //spriteRenderer.color = new Color(1, 1, 1, 1);
    }                                      // No bool gotDamage. Only Change Color

    void RandomSpell()
    {
        int i = Random.Range(0, 2);
        SpellCasting.GetComponent<SpriteRenderer>().sprite = cast[i];
        spellPrefab.GetComponent<SpriteRenderer>().sprite = spell[i];
    }                                         // Random Spell

    void addForceRope()
    {
        if (Input.GetKey(KeyCode.A))
        {
            if (attached)
            {
                rb2d.AddRelativeForce(new Vector3(-1, 0, 0) * pushForce);
            }
        }
        if (Input.GetKey(KeyCode.D))
        {
            if (attached)
            {
                rb2d.AddRelativeForce(new Vector3(1, 0, 0) * pushForce);
            }
        }

        if (Input.GetKeyDown(KeyCode.Space) && attached)
        {
            Detach();
        }
    }

    public void Attach(Rigidbody2D ropeBone)
    {
        ropeBone.gameObject.GetComponent<TestRopeSegment>().isPlayerAttached = true;
        hj2d.connectedBody = ropeBone;
        hj2d.enabled = true;
        attached = true;
        attachTo = ropeBone.gameObject.transform.parent;

        anim.SetBool("isRope", true);

        setRopeRotation = true;
    }

    void Detach()
    {
        hj2d.connectedBody.gameObject.GetComponent<TestRopeSegment>().isPlayerAttached = false;
        attached = false;
        hj2d.enabled = false;
        hj2d.connectedBody = null;
        Invoke("resetRope", 1);

        anim.SetBool("isRope", false);

        setRopeRotation = false;
    }

    void setRotationRope()
    {
        if (setRopeRotation)
        {
            Transform rope = attachTo.GetChild(1).GetComponent<Transform>();
            transform.rotation = Quaternion.Euler(0, 0, rope.eulerAngles.z + 91);
            if(!setInRope)
            {
                setInRope = true;
            }
        }
        else
        {
            if (setInRope)
            {
                transform.rotation = Quaternion.Euler(0, 0, 0);
                if (transform.localScale.x > 0)
                {
                    bookFirstPosition = new Vector3(transform.position.x - 1, transform.position.y);
                }
                else
                {
                    bookFirstPosition = new Vector3(transform.position.x + 1, transform.position.y);
                }
                setInRope = false;
            }
        }
    }

    void resetRope()
    {
        if (!attached)
        {
            attachTo = null;
        }
    }

    void setHeart()
    {
        /*
        if (nowHP >= 6)
        {
            Heart[5].SetActive(true);
        }
        else
        {
            Heart[5].SetActive(false);
        }
        if (nowHP >= 5)
        {
            Heart[4].SetActive(true);
        }
        else
        {
            Heart[4].SetActive(false);
        }
        */
        if (nowHP >= 4)
        {
            Heart[3].SetActive(true);
        }
        else
        {
            Heart[3].SetActive(false);
        }
        if (nowHP >= 3)
        {
            Heart[2].SetActive(true);
        }
        else
        {
            Heart[2].SetActive(false);
        }
        if (nowHP >= 2)
        {
            Heart[1].SetActive(true);
        }
        else
        {
            Heart[1].SetActive(false);
        }
        if (nowHP >= 1)
        {
            Heart[0].SetActive(true);
        }
        else
        {
            Heart[0].SetActive(false);
        }
    }

    void takeDamage(Collision2D other, int Damage)
    {
        isDamage = true;
        gotDamage = true;
        Invoke("gotDamageFunc", 1);
        Invoke("DamageTaken", 2);

        //Damage
        nowHP -= Damage;


        StartCoroutine("gotDamageFuncSprite");
        anim.SetBool("isHit", true);
        //spriteRenderer.color = new Color(1, 0, 0, 1);              //CHANGE COLOR HEREEEEEEEEE

        ScriptSoundsManager.instance.PlaySingle(HitSound);

        if (hj2d.enabled == true)
        {
            Detach();
        }

        int i = 0;
        if (other == null)
        {
            i = 1;
        }
        if (i == 0)
        {
            if (transform.position.x > other.transform.position.x)
            {
                rb2d.velocity = new Vector2(5, 0);
            }
            else if (transform.position.x < other.transform.position.x)
            {
                rb2d.velocity = new Vector2(-5, 0);
            }
        }
    }

    void takeDamageTrigger(Collider2D other, int Damage)
    {
        isDamage = true;
        Invoke("gotDamageFunc2", 1);
        Invoke("DamageTaken", 2);

        //Damage
        nowHP -= Damage;

        StartCoroutine("gotDamageFuncSprite");
        anim.SetBool("isHit", true);
        //spriteRenderer.color = new Color(1, 0, 0, 1);              //CHANGE COLOR HEREEEEEEEEE

        ScriptSoundsManager.instance.PlaySingle(HitSound);

        if (hj2d.enabled == true)
        {
            Detach();
        }

        int i = 0;
        if (other == null)
        {
            i = 1;
        }
        if (i == 0)
        {
            if (transform.position.x > other.transform.position.x)
            {
                rb2d.velocity = new Vector2(5, 0);
            }
            else if (transform.position.x < other.transform.position.x)
            {
                rb2d.velocity = new Vector2(-5, 0);
            }
        }
    }

    void resetPotion1()
    {
        scaleX = scaleX * 2;
        scaleY = scaleY * 2;

        isGotPotion1 = false;

        JumpForce = 8f;
        HorizontalSpeed = 6f;
        pushForce = 10f;

        if (transform.localScale.x > 0)
        {
            transform.localScale = new Vector3(scaleX, scaleY, 1);
        }
        else if (transform.localScale.x < 0)
        {
            transform.localScale = new Vector3(-scaleX, scaleY, 1);
        }
    }

    void GetPotion1()
    {
        scaleX = scaleX / 2;
        scaleY = scaleY / 2;

        isGotPotion1 = true;
        Invoke("resetPotion1", 8);

        JumpForce = 6f;
        HorizontalSpeed = 4f;
        pushForce = 5f;

        if (transform.localScale.x > 0)
        {
            transform.localScale = new Vector3(scaleX, scaleY, 1);
        }
        else if (transform.localScale.x < 0)
        {
            transform.localScale = new Vector3(-scaleX, scaleY, 1);
        }
    }

    void BookController()
    {
        bookCastPosition = new Vector3(transform.position.x, transform.position.y + 2);
        //Debug.Log(bookCastPosition);

        if (transform.localScale.x > 0)
        {
            bookFirstPosition = new Vector3(transform.position.x - 1, transform.position.y);
        }
        else
        {
            bookFirstPosition = new Vector3(transform.position.x + 1, transform.position.y);
        }
        // Debug.Log(bookPosition.y + "////" + transform.position.y);


        if (!isCastSpell)
        {
            if (!goUp)
            {
                magicBook.transform.position += new Vector3(0, -1, 0) * bookSpeed * Time.deltaTime;
            }
            else
            {
                magicBook.transform.position += new Vector3(0, 1, 0) * bookSpeed * Time.deltaTime;
            }

            if (magicBook.transform.position.y < transform.position.y - 0.5f)
            {
                goUp = true;
            }
            if (magicBook.transform.position.y > transform.position.y + 0.5f)
            {
                goUp = false;
            }
        }
        else
        {
            magicBook.transform.position = bookCastPosition;
            magicBook.GetComponent<Animator>().SetBool("isCast", true);
            //magicBook.GetComponent<Rigidbody2D>().MovePosition(bookCastPosition * 5 * Time.deltaTime);
            //magicBook.transform.Translate(bookCastPosition);
            //Debug.Log(isCastSpell);

            StartCoroutine("BookGoToFirstPos", 1);
        }
    }

    void Respawn()
    {
        if (isDead)
        {
            scriptCameraController.ResetScene();

            gotDamage = false;
            isDead = false;
            anim.SetBool("isDead", false);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {

        if(other.gameObject.CompareTag("EnemySeePlayer"))
        {
            other.gameObject.GetComponentInParent<ScriptEnemyController>().seePlayer = true;
        }

        if (other.gameObject.CompareTag("Heart"))
        {
            if (nowHP < maxHP)
            {
                //Heal
                nowHP += 1;
                other.gameObject.GetComponent<Animator>().SetTrigger("isGet");
                Destroy(other.gameObject, 0.4f);
            }
        }

        if(other.gameObject.CompareTag("Key"))
        {
            scripteUIController.isGetKey = true;
        }

        if (!attached)                                                         //Check Trigger Wtih Rope
        {
            if (other.gameObject.CompareTag("Rope") || other.gameObject.CompareTag("RopeW2"))
            {
                if (attachTo != other.gameObject.transform.parent)
                {
                    if (disregard == null || other.gameObject.transform.parent.gameObject != disregard)
                    {
                        Attach(other.gameObject.GetComponent<Rigidbody2D>());
                    }
                }
            }
        }


        if (other.gameObject.CompareTag("BossSpell") && !isDamage)
        {
            takeDamageTrigger(other, 1);
        }

        if (other.gameObject.CompareTag("Potion1"))
        {
            if (!isGotPotion1)
            {
                GetPotion1();
            }
        }

        if(other.gameObject.CompareTag("CheckPoint"))
        {
            savePosition = other.gameObject.GetComponent<Transform>().position;
            other.gameObject.GetComponent<Animator>().SetTrigger("isPass");
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("Spite") && !isDamage)
        {
            takeDamageTrigger(other, 6);
        }

        if (other.gameObject.CompareTag("Ladder") || other.gameObject.CompareTag("LadderW2"))
        {
                canLadder = true;
        }

        if (other.gameObject.CompareTag("Block") || other.gameObject.CompareTag("Wall") || other.gameObject.CompareTag("Blockw2"))
        {
            if (other.gameObject.GetComponent<Collider2D>().isTrigger)
            {
                intheBlock = true;
            }
        }

        if(other.gameObject.CompareTag("EndScene"))
        {
            Scene sceneLoaded = SceneManager.GetActiveScene();
            SceneManager.LoadScene(sceneLoaded.buildIndex + 1);                  //Change to Next Scene
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Ladder") || other.gameObject.CompareTag("LadderW2"))
        {
            canLadder = false;
            isLadder = false;
            rb2d.gravityScale = 1;
            gameObject.layer = 12;

            anim.SetBool("isClimb", false);
        }

        if (other.gameObject.CompareTag("EnemySeePlayer"))
        {
            other.gameObject.GetComponentInParent<ScriptEnemyController>().seePlayer = false;
        }

        if (other.gameObject.CompareTag("Block") || other.gameObject.CompareTag("Wall") || other.gameObject.CompareTag("Blockw2"))
        {
            intheBlock = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.CompareTag("RockBall"))
        {
            other.gameObject.GetComponent<Collider2D>().isTrigger = true;
            takeDamage(null, 6);
        }
    }

    private void OnCollisionStay2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Enemy") && !isDamage && !other.gameObject.GetComponent<ScriptEnemyController>().isDead)
        {
            takeDamage(other,1);
        }

        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D))
        {
            if (other.gameObject.CompareTag("Block") && transform.position.y - 0.3f < other.transform.position.y)
            {
                anim.SetBool("isPush", true);
            }

            if (other.gameObject.CompareTag("Block12") && transform.position.y - 0.3f < other.transform.position.y)
            {
                anim.SetBool("isPush", true);
            }

            if (other.gameObject.CompareTag("Blockw2") && transform.position.y - 0.3f < other.transform.position.y)
            {
                anim.SetBool("isPush", true);
            }
        }
        else
        {
            anim.SetBool("isPush", false);
        }

        if(other.gameObject.CompareTag("Boss1") && !isDamage && !other.gameObject.GetComponent<TestBoss>().isAttacking)
        {
            takeDamage(other, 1);
        }
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Block") || other.gameObject.CompareTag("Block12"))
        {
            anim.SetBool("isPush", false);
        }
    }

    IEnumerator gotDamageFuncSprite()
    {
        while(true)
        {
            yield return new WaitForSeconds(0.1f);
            spriteRenderer.color = new Color(1, 0, 0, 1);
            yield return new WaitForSeconds(0.1f);
            spriteRenderer.color = new Color(1, 1, 1, 1);
        }
    }

    IEnumerator BookGoToFirstPos()
    {
        while (true)
        {
            yield return new WaitForSeconds(0);
            if (!isCastSpell)
            {
                magicBook.transform.position = bookFirstPosition;
                magicBook.GetComponent<Animator>().SetBool("isCast", false);
                //magicBook.GetComponent<Rigidbody2D>().MovePosition(bookFirstPosition * 5 * Time.deltaTime);
                StopCoroutine("BookGoToFirstPos");
            }
        }
    }


    void GameMode()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            nowHP++;
        }
        if (Input.GetKeyDown(KeyCode.K))
        {
            takeDamage(null, 1);
        }
        if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            if (!isGotPotion1)
            {
                GetPotion1();
            }
        }

    }



}


