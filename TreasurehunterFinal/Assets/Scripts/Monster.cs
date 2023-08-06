using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Rendering;

public class Monster : MonoBehaviour
{
    public float speed;
    public float health;
    public float maxHealth;
    public Rigidbody2D target;

    public RuntimeAnimatorController[] anime;

    bool Live;
    Rigidbody2D rigid;
    Collider2D col;
    SpriteRenderer render;
    Animator animator;
    WaitForFixedUpdate wait;
    // Start is called before the first frame update
    void Awake()
    {
        wait = new WaitForFixedUpdate();
        col = GetComponent<Collider2D>();
        target = GameObject.Find("Player").GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        rigid = GetComponent<Rigidbody2D>();
        render = GetComponent<SpriteRenderer>(); 
        health = maxHealth;
        Live = true;
        
    }
    public void OnEnable()
    {
        

    }
    public void Init(SpawnStat stat)
    {
        animator.runtimeAnimatorController = anime[stat.type];
        speed = stat.Speed;
        maxHealth = stat.Health;
        health = stat.Health;
    }

    private void FixedUpdate()
    {
       
            Vector2 dirVec = target.position - rigid.position;
            Vector2 nextVec = dirVec.normalized * speed * Time.fixedDeltaTime;
            rigid.MovePosition(rigid.position + nextVec);
            rigid.velocity = Vector2.zero;
        
    }

    private void LateUpdate()
    {
        render.flipX = target.position.x < rigid.position.x;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            GetComponent<UI>().Hit();
        }
    }
    public void TakeDamage(int damage)
    {

        health -= damage;
        if (health > 0)
        {
            animator.SetTrigger("Hit");
            StartCoroutine(KnockBack());
        }
        else
        {
            Live = false;
            col.enabled = false;
            rigid.simulated = false;
            render.sortingOrder = 1;
            animator.SetBool("Dead", true);
            Dead();
        }
    }

    IEnumerator KnockBack()
    {
        yield return wait;
        Vector3 playerpos = GameObject.Find("Player").GetComponent<Rigidbody2D>().transform.position;
        Vector3 direcVec = transform.position - playerpos;
        rigid.AddForce(direcVec.normalized * 2, ForceMode2D.Impulse);
    }

    void Dead()
    {
        gameObject.SetActive(false);
    }
}
