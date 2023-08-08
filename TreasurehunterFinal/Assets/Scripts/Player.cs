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
    public bool isHealth;

    public static bool isAlive;

    public static GameObject treasure;
    public GameObject hudDamageText;    // 데미지 표시할 오브젝트
    public Transform hudPos;            // 데미지 표시할 위치

    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        spriter = GetComponent<SpriteRenderer>(); // Mi2141 추가
        anim = GetComponent<Animator>(); // Mi2141 추가

        isMovable = true;
        isAction = false;
        isAlive = true;

        treasure = null;

        //hudPos.transform.position = transform.position; // 데미지 표기 위치 초기화
    }

    // Update is called once per frame
    void Update()
    {
        if (!isAlive)
            return;

        inputVec.x = Input.GetAxisRaw("Horizontal");
        inputVec.y = Input.GetAxisRaw("Vertical");

        if (Input.GetKey(KeyCode.Z) && !isAction)
        {
            isMovable = false;
            isAction = true;
            AudioManager.instance.PlaySfx(AudioManager.SFX.Dig);
            Invoke("Dig", 1);
        }
        // 게임 종료 판정
        if (UI.instance.currentHealth > UI.instance.maxHealth)
        {
            UI.instance.currentHealth = UI.instance.maxHealth;  // 체력 상한선 조정
        }
        else if (UI.instance.currentHealth < 0)
        {
            UI.instance.GameOver();   // 체력 소진시 게임종료 함수 호출
            anim.SetTrigger("Dead");  // Dead 애니메이션 호출
            isAlive = false;
            AudioManager.instance.StopBgm();

        }
    }
    void FixedUpdate()
    {
        if (!isAlive)
            return;
        Vector2 nextVec = inputVec.normalized * speed * Time.fixedDeltaTime;

        if (isMovable)
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

    public void TakeDamage(int damage)
    {
        isHealth = false;

        if(!isHealth)
        {
            GameObject hudText = Instantiate(hudDamageText);    // 데미지를 여러번 받을 수 있으므로 복제
            hudText.transform.position = transform.position;
            hudText.GetComponent<DisplayDamage>().damage = damage;
            UI.instance.currentHealth -= damage;
            OnDamaged();
        }
        
    }
    public void OnDamaged()
    {
        gameObject.layer = 11;
        StartCoroutine(Damaged());
        StartCoroutine(Change());
    }

    IEnumerator Damaged()
    {
        while(!isHealth)
        {
            yield return new WaitForSeconds(0.1f);
            spriter.color = new Color(1,1,1,0.4f);
            yield return new WaitForSeconds(0.1f);
            spriter.color = new Color(1, 1, 1, 1);
        }
    }
    IEnumerator Change()
    {
        yield return new WaitForSeconds(1.5f);
        gameObject.layer = 6;
        isHealth = true;
    }
    // Mi2141 추가
    void LateUpdate()
    {
        anim.SetFloat("Speed", inputVec.magnitude);

        if (inputVec.x != 0)
        {
            spriter.flipX = inputVec.x < 0;
        }
    }
}

