using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScriptCameraController : MonoBehaviour
{

    //Camera

    private Transform target;
    private ScriptPlayerController ScriptPlayerController;
    public float smoothSpeed = 0.125f;
    private Vector3 offset;

    public GameObject lightTL, lightTR, lightDL, lightDR;

    float cropPosition = 12f;

    Vector3 TL, TR, DL, DR;

    public GameObject[] blockPrefabs;
    public GameObject[] blockw2Prefabs;
    public GameObject[] block12Prefabs;
    public GameObject[] Ladder;
    public GameObject[] LadderW2;
    public GameObject[] Rope;
    public GameObject[] RopeW2;

    public GameObject[] enemy;

    public GameObject realWorld;
    bool isWorld2;

    public GameObject[] Platforms;


    public Sprite bgw1;

    public Sprite bgw2;

    //gameobject
    public GameObject[] BackGround;
    public GameObject[] grass;
    public GameObject bg;

    private float speedLight = 22.5f;

    public bool onbutton;

    public bool isChanging = false;

    public Vector3[] keepBoxFirstPositionW1, keepBoxFirstPositionW2, keepBoxFirstPositionW12;

    void Awake()
    {
        BackGround     = GameObject.FindGameObjectsWithTag("BG");

        blockPrefabs   = GameObject.FindGameObjectsWithTag("Block");
        blockw2Prefabs = GameObject.FindGameObjectsWithTag("Blockw2");
        block12Prefabs = GameObject.FindGameObjectsWithTag("Block12");

        keepBoxFirstPositionW1  = new Vector3[blockPrefabs.Length];
        keepBoxFirstPositionW2  = new Vector3[blockw2Prefabs.Length];
        keepBoxFirstPositionW12 = new Vector3[block12Prefabs.Length];

        Rope           = GameObject.FindGameObjectsWithTag("Rope");
        RopeW2         = GameObject.FindGameObjectsWithTag("RopeW2");

        Ladder         = GameObject.FindGameObjectsWithTag("Ladder");
        LadderW2       = GameObject.FindGameObjectsWithTag("LadderW2");
        ScriptPlayerController = GameObject.FindGameObjectWithTag("Player").GetComponent<ScriptPlayerController>();
        target         = GameObject.FindGameObjectWithTag("Player").transform;

        Platforms      = GameObject.FindGameObjectsWithTag("PFworld1");

        enemy          = GameObject.FindGameObjectsWithTag("Enemy");

        // keepBoxFirstPositionW1 = GameObject.FindGameObjectsWithTag("Block").transform.position;

        saveFirstPositon(blockPrefabs  , keepBoxFirstPositionW1 );
        saveFirstPositon(blockw2Prefabs, keepBoxFirstPositionW2 );
        saveFirstPositon(block12Prefabs, keepBoxFirstPositionW12);

        offset = new Vector3(0, 0, -10);

        lightTL.SetActive(true);
        lightTR.SetActive(true);
        lightDL.SetActive(true);
        lightDR.SetActive(true);

        realWorld.SetActive(true);
    }

    void Start()
    {
        worldOne();
    }

    void Update()
    {
        if (ScriptPlayerController.onGround && !ScriptPlayerController.isCastSpell && !ScriptPlayerController.GetComponent<HingeJoint2D>().enabled)
        {
            lightMove();
        }

        changing();

        if(Input.GetKeyDown(KeyCode.R) && ScriptPlayerController.onGround)
        {
            isChanging = true;
            ScriptPlayerController.isCastSpell = true;
            Invoke("changefalse", 1.5f);

            ScriptPlayerController.GetComponent<Animator>().SetBool("isCast", true);

            ScriptPlayerController.SpellCasting.GetComponent<SpriteRenderer>().sprite = ScriptPlayerController.cast[2];
            ScriptPlayerController.SpellCasting.SetActive(true);
            Invoke("Casting", 0.8f);

            ScriptSoundsManager.instance.PlaySingle(ScriptPlayerController.CastSound);
            Invoke("ResetScene",0.8f);
            Invoke("worldOne", 0.8f);
        }
    }

    void FixedUpdate()
    {
        if (!onbutton)
        {
            moveCamera();
        }
    }

    void moveCamera()
    {
        if (target.localScale.x > 0)
        {
            offset = new Vector3(0.5f, 0, -10);
        }
        else if (target.localScale.x < 0)
        {
            offset = new Vector3(-0.5f, 0, -10);
        }

        Vector3 desiredPosition = target.position + offset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        transform.position = smoothedPosition;
    }

    void lightMove()
    {
        if (Input.GetKeyDown(KeyCode.V) && !isChanging && !ScriptPlayerController.intheBlock)
        {
            isChanging = true;
            ScriptPlayerController.isCastSpell = true;
            Invoke("changefalse", 1.5f);

            ScriptPlayerController.GetComponent<Animator>().SetBool("isCast", true);

            ScriptPlayerController.SpellCasting.GetComponent<SpriteRenderer>().sprite = ScriptPlayerController.cast[2];
            ScriptPlayerController.SpellCasting.SetActive(true);
            Invoke("Casting", 0.8f);

            ScriptSoundsManager.instance.PlaySingle(ScriptPlayerController.CastSound);
            if (isWorld2)
            {
                Invoke("worldOne", 0.8f);
            }
            else if(!isWorld2)
            {
                Invoke("worldTwo", 0.8f);
            }
        }
    }

    void changefalse()
    {
        isChanging = false;
        ScriptPlayerController.isCastSpell = false;
        ScriptPlayerController.GetComponent<Animator>().SetBool("isCast", false);
    }

    void changing()
    {

        TL = new Vector3(gameObject.transform.position.x - cropPosition, gameObject.transform.position.y + cropPosition);
        TR = new Vector3(gameObject.transform.position.x + cropPosition, gameObject.transform.position.y + cropPosition);
        DL = new Vector3(gameObject.transform.position.x - cropPosition, gameObject.transform.position.y - cropPosition);
        DR = new Vector3(gameObject.transform.position.x + cropPosition, gameObject.transform.position.y - cropPosition);

        if (isChanging)
        {
            lightTL.transform.position += new Vector3(speedLight, -speedLight, 0) * Time.deltaTime;
            lightTR.transform.position += new Vector3(-speedLight, -speedLight, 0) * Time.deltaTime;
            lightDL.transform.position += new Vector3(speedLight, speedLight, 0) * Time.deltaTime;
            lightDR.transform.position += new Vector3(-speedLight, speedLight, 0) * Time.deltaTime;
        }
        else
        {
            lightTL.transform.position = TL;
            lightTR.transform.position = TR;
            lightDL.transform.position = DL;
            lightDR.transform.position = DR;
        }
    }

    void worldTwo()
    {
        realWorld.SetActive(false);
        isWorld2 = true;

        changeSpriteArray(BackGround, true);

        changeSprite(bg, bgw2);


        setActiveBlock(blockPrefabs,0.4f,true, 0);
        setActiveBlock(blockw2Prefabs, 1, false, 1);

        setActiveLadder(Ladder, 0.2f, false);
        setActiveLadder(LadderW2, 1, true);

        Debug.Log("isChangeToWorldTwo");

        SetActivePlatforms(false);

        setActiveRope(Rope, 0.2f, false);
        setActiveRope(RopeW2, 1f, true);
    }

    void worldOne()
    {
        realWorld.SetActive(true);
        isWorld2 = false;

        changeSpriteArray(BackGround, false);

        changeSprite(bg, bgw1);

        setActiveBlock(blockPrefabs,1, false,1);
        setActiveBlock(blockw2Prefabs, 0.2f, true, 0);

        setActiveLadder(Ladder, 1, true);
        setActiveLadder(LadderW2, 0.2f, false);
        Debug.Log("isChangeToWorldOne");

        SetActivePlatforms(true);

        setActiveRope(Rope, 1f, true);
        setActiveRope(RopeW2, 0.2f, false);
    }

    void changeSpriteArray(GameObject []bg , bool bgchange)
    {
        for (int i = 0; i < bg.Length; i++)
        {
            bg[i].GetComponent<Animator>().SetBool("ChangeToW2",bgchange);
        }
    }

    void changeSprite(GameObject bg, Sprite bgchange)
    {
        bg.GetComponent<SpriteRenderer>().sprite = bgchange;
    }

    void setActiveBlock(GameObject []block,float fcolor,bool setTrigger ,int gravity)               //spriterenderer /collider is trigger / rb2d.enable 
    {
        for (int i = 0; i < block.Length; i++)
        {
            int j = 0;
            if (block[i] == null)
            {
                j = 1;
            }
            if (j == 0)
            {
                block[i].GetComponent<SpriteRenderer>().color = new Color(fcolor, fcolor, fcolor, fcolor);
                block[i].GetComponent<Collider2D>().isTrigger = setTrigger;
                block[i].GetComponent<Rigidbody2D>().gravityScale = gravity;
                block[i].GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
            }
        }
    }

    void setActiveLadder(GameObject []Ladder , float fcolor , bool enable)
    {
        for(int i = 0;i<Ladder.Length; i++)
        {
            Ladder[i].GetComponent<SpriteRenderer>().color = new Color(fcolor, fcolor, fcolor, fcolor);
            Ladder[i].GetComponent<Collider2D>().enabled = enable;
        }
    }

    void setActiveRope(GameObject[] Rope, float fcolor, bool enable)
    {
        for (int i = 0; i < Rope.Length; i++)
        {
            Rope[i].GetComponentInParent<SpriteRenderer>().color = new Color(fcolor, fcolor, fcolor, fcolor);
            Rope[i].GetComponent<Collider2D>().enabled = enable;
        }
    }

    public void WoodCam(Transform wood)
    {
        Vector3 desiredPosition = wood.position + offset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        transform.position = smoothedPosition;
    }

    void SetActivePlatforms(bool set)
    {
        for (int i = 0; i < Platforms.Length; i++)
        {
            Platforms[i].SetActive(set);
        }
    }
    void Casting()
    {
        ScriptPlayerController.SpellCasting.SetActive(false);
    }

    void saveFirstPositon(GameObject[] objects, Vector3[] firstPosition)
    {
        for (int i = 0; i < objects.Length; i++)
        {
            firstPosition[i] = objects[i].transform.position;
        }
    }

    void ResetPuzzle(GameObject[] objects, Vector3[] firstPosition)
    {
        for (int i = 0; i < objects.Length; i++)
        {
            objects[i].transform.position = firstPosition[i];
        }
    }

    void ResetEnemy()
    {
        for (int i = 0; i < enemy.Length;i++)
        {
            enemy[i].SetActive(true);
            enemy[i].GetComponent<ScriptEnemyController>().HP = 2;
            enemy[i].GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);
            enemy[i].GetComponent<ScriptEnemyController>().isDead = false;
        }
    }

    public void ResetScene()
    {
        ResetPuzzle(blockPrefabs,   keepBoxFirstPositionW1) ;
        ResetPuzzle(blockw2Prefabs, keepBoxFirstPositionW2) ;
        ResetPuzzle(block12Prefabs, keepBoxFirstPositionW12);
        target.position = ScriptPlayerController.savePosition;
        ScriptPlayerController.nowHP = ScriptPlayerController.maxHP;
        ResetEnemy();
    }

}
