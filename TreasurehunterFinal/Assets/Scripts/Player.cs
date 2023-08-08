using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Vector2 inputVec;

    public Rigidbody2D rigid;
    public float speed;

    SpriteRenderer spriter; // Mi2141 �߰�
    Animator anim; // Mi2141 �߰�

    public bool isMovable;
    public bool isAction;
    public bool isHealth;

    public static bool isAlive;

    public static GameObject treasure;
    public GameObject hudDamageText;    // ������ ǥ���� ������Ʈ
    public Transform hudPos;            // ������ ǥ���� ��ġ

    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        spriter = GetComponent<SpriteRenderer>(); // Mi2141 �߰�
        anim = GetComponent<Animator>(); // Mi2141 �߰�

        isMovable = true;
        isAction = false;
        isAlive = true;

        treasure = null;

        //hudPos.transform.position = transform.position; // ������ ǥ�� ��ġ �ʱ�ȭ
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
        // ���� ���� ����
        if (UI.instance.currentHealth > UI.instance.maxHealth)
        {
            UI.instance.currentHealth = UI.instance.maxHealth;  // ü�� ���Ѽ� ����
        }
        else if (UI.instance.currentHealth < 0)
        {
            UI.instance.GameOver();   // ü�� ������ �������� �Լ� ȣ��
            anim.SetTrigger("Dead");  // Dead �ִϸ��̼� ȣ��
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
            Debug.Log("���ı� ����");
            treasure.GetComponent<Treasure>().Find();
            AudioManager.instance.PlaySfx(AudioManager.SFX.Success);

            UI.instance.currentHealth += 30; // ���� �߽߰� ü�� ȸ��
        }
        else
        {
            Debug.Log("���ı� ����");
            AudioManager.instance.PlaySfx(AudioManager.SFX.Fail);
            UI.instance.currentHealth -= 10; // �����ϸ� ü�� ����
        }
        isAction = false;
        isMovable = true;
    }

    public void TakeDamage(int damage)
    {
        isHealth = false;

        if(!isHealth)
        {
            GameObject hudText = Instantiate(hudDamageText);    // �������� ������ ���� �� �����Ƿ� ����
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
    // Mi2141 �߰�
    void LateUpdate()
    {
        anim.SetFloat("Speed", inputVec.magnitude);

        if (inputVec.x != 0)
        {
            spriter.flipX = inputVec.x < 0;
        }
    }
}

