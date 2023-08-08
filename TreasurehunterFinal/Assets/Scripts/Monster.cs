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
        animator = GetComponent<Animator>();
        rigid = GetComponent<Rigidbody2D>();
        render = GetComponent<SpriteRenderer>();
    }
    public void OnEnable()
    {
        target = GameObject.Find("Player").GetComponent<Rigidbody2D>();
        Live = true;
        col.enabled = true;
        rigid.simulated = true;
        render.sortingOrder = 2;
        animator.SetBool("Dead", false);
        health = maxHealth;
    }
    public void Init(SpawnStat stat)
    {
        animator.runtimeAnimatorController = anime[stat.type];
        speed = stat.Speed;
        maxHealth = stat.Health;
        health = stat.Health;
    }

    public void FixedUpdate()
    {
        if (!Live || animator.GetCurrentAnimatorStateInfo(0).IsName("Hit"))
            return;

        Vector2 dirVec = target.position - rigid.position;
        Vector2 nextVec = dirVec.normalized * speed * Time.fixedDeltaTime;
        rigid.MovePosition(rigid.position + nextVec);
        rigid.velocity = Vector2.zero;
    }
    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (Live && collision.gameObject.name == "Player")
        {
            GameObject.Find("Player").GetComponent<Player>().TakeDamage(2);
        }
        else
            return;
    }
    private void LateUpdate()
    {
        render.flipX = target.position.x < rigid.position.x;
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
            AudioManager.instance.PlaySfx(AudioManager.SFX.Die);
            UI.instance.score += 20;
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

