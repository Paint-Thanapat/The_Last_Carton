using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestBoss : MonoBehaviour
{

    private Rigidbody2D rb2d;

    //movement Boss
    private GameObject player;
    private ScriptPlayerController scriptPlayerController;
    public float moveSpeed;
    float scaleX;
    float scaleY;

    // Bool Attack
    public bool basicAttack;
    public bool isAttacking;

    //Shooting Spell
    public GameObject spellPrefabs;
    public GameObject aimPlayerObj;

    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        scriptPlayerController = GameObject.FindGameObjectWithTag("Player").GetComponent<ScriptPlayerController>();
        rb2d = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        scaleX = transform.localScale.x;
        scaleY = transform.localScale.y;

        StartCoroutine("spellAttack");
    }

    void Update()
    {

    }

    void FixedUpdate()
    {
        if (!basicAttack && !isAttacking)
        {
            BossMove();
        }
    }

    void BossMove()
    {
        if (player.transform.position.x > transform.position.x + 0.2f)
        {
            transform.position += new Vector3(1, 0, 0) * moveSpeed * Time.deltaTime;
            transform.localScale = new Vector3(scaleX,scaleY,1);
        }
        if (player.transform.position.x < transform.position.x - 0.2f)
        {
            transform.position += new Vector3(-1, 0, 0) * moveSpeed * Time.deltaTime;
            transform.localScale = new Vector3(-scaleX, scaleY, 1);
        }
    }

    void BossSpellAttack()
    {
            GameObject clone1 = Instantiate(spellPrefabs, aimPlayerObj.transform.position, aimPlayerObj.transform.rotation);
            clone1.GetComponent<TestEnemySpell>().topDownPlayer[0] = true;
            GameObject clone2 = Instantiate(spellPrefabs, aimPlayerObj.transform.position, aimPlayerObj.transform.rotation);
            clone2.GetComponent<TestEnemySpell>().topDownPlayer[1] = true;
            GameObject clone3 = Instantiate(spellPrefabs, aimPlayerObj.transform.position, aimPlayerObj.transform.rotation);
            clone3.GetComponent<TestEnemySpell>().topDownPlayer[2] = true;
    }

    void BossSpellAttack2()
    {
            GameObject clone2 = Instantiate(spellPrefabs, aimPlayerObj.transform.position, aimPlayerObj.transform.rotation);
            clone2.GetComponent<TestEnemySpell>().topDownPlayer[1] = true;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.CompareTag("Player") && isAttacking)
        {
            basicAttack = true;
            Invoke("SetBasicAttack", 2);
        }

        if(other.gameObject.CompareTag("Bullet"))
        {
            rb2d.velocity = new Vector2(0, 0);
        }
    }

    void SetBasicAttack()
    {
        basicAttack = false;
    }

    void SetIsAttacking()
    {
        isAttacking = false;
    }

    void Attack1()
    {
        Invoke("BossSpellAttack", 0.5f);
        Invoke("BossSpellAttack", 0.9f);
        Invoke("BossSpellAttack", 1.9f);
        Invoke("BossSpellAttack", 2.3f);

        Invoke("SetIsAttacking", 3.3f);
    }

    void Attack2()
    {
        Invoke("BossSpellAttack2", 0.5f);
        Invoke("BossSpellAttack2", 0.9f);
        Invoke("BossSpellAttack2", 1.3f);

        Invoke("SetIsAttacking", 2.3f);
    }

    void Attack3()
    {
        Invoke("BossSpellAttack", 0.5f);
        Invoke("BossSpellAttack2", 0.9f);
        Invoke("BossSpellAttack2", 1.3f);

        Invoke("SetIsAttacking", 2.3f);
    }

    IEnumerator spellAttack()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(8, 13));

            int i = Random.Range(1, 4);
            isAttacking = true;

            if (i == 1)
            {
                Attack1();
            }
            if (i == 2)
            {
                Attack2();
            }
            if (i == 3)
            {
                Attack3();
            }
        }
    }

}
