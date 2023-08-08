using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DisplayDamage : MonoBehaviour
{
    public float moveSpeed;     // �ؽ�Ʈ �̵� �ӵ�
    public float alphaSpeed;    // ���� ��ȭ �ӵ�
    public float destroyTime;
    public int damage;
    TextMeshPro text;
    Color alpha;

    void Start()
    {
        moveSpeed = 2.0f;
        alphaSpeed = 2.0f;
        destroyTime = 2.0f;
        text = GetComponent<TextMeshPro>();
        text.text = damage.ToString();
        alpha = text.color;                     // Color.a ���� 0�� �������� ����
        
    }

    void Update()
    {
        transform.Translate(new Vector3(0, moveSpeed * Time.deltaTime, 0));
        alpha.a = Mathf.Lerp(alpha.a, 0, alphaSpeed * Time.deltaTime);
        text.color = alpha;
        Invoke("DestroyObject", destroyTime);
    }

    public static void Display(Vector3 position, int damage)
    {
        //text.text = damage.ToString();
        //transform.position = position;
        //Invoke("DestroyObject", destroyTime);   // ���� �ð� �� �ؽ�Ʈ �ı�
    }

    private void DestroyObject()
    {
        Destroy(gameObject);
    }
}
