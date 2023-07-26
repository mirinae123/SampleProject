using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Vector2 inputVec;

    public Rigidbody2D rigid;
    public float speed;

    SpriteRenderer spriter; // Mi2141 추가
    Animator anim; // Mi2141 추가

    public bool isMovable;
    public bool isAction;

    public static GameObject treasure;

    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        spriter = GetComponent<SpriteRenderer>(); // Mi2141 추가
        anim = GetComponent<Animator>(); // Mi2141 추가

        isMovable = true;
        isAction = false;

        treasure = null;
    }

    // Update is called once per frame
    void Update()
    {
        inputVec.x = Input.GetAxisRaw("Horizontal");
        inputVec.y = Input.GetAxisRaw("Vertical");

        if (Input.GetKey(KeyCode.Z) && !isAction)
        {
            isMovable = false;
            isAction = true;
            AudioManager.instance.PlaySfx(AudioManager.SFX.Dig);
            Invoke("Dig", 1);
        }
    }

    void FixedUpdate()
    {
        Vector2 nextVec = inputVec.normalized * speed * Time.fixedDeltaTime;

        if(isMovable)
            rigid.MovePosition(rigid.position + nextVec);
    }

    void Dig()
    {
        if (treasure)
        {
            Debug.Log("땅파기 성공");
            treasure.GetComponent<Treasure>().Find();
            AudioManager.instance.PlaySfx(AudioManager.SFX.Success);

            UI.instance.currentHealth += 30; // 보물 발견시 체력 회복
        }
        else
        {
            Debug.Log("땅파기 실패");
            AudioManager.instance.PlaySfx(AudioManager.SFX.Fail);
            UI.instance.currentHealth -= 10; // 실패하면 체력 감소
        }
        isAction = false;
        isMovable = true;
    }

    // Mi2141 추가
    void LateUpdate()
    {
        anim.SetFloat("Speed", inputVec.magnitude);

        if(inputVec.x != 0)
        {
            spriter.flipX = inputVec.x < 0;
        }
    }
}
