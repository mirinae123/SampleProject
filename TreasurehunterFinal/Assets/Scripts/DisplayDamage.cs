using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DisplayDamage : MonoBehaviour
{
    public float moveSpeed;     // 텍스트 이동 속도
    public float alphaSpeed;    // 투명도 변화 속도
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
        alpha = text.color;                     // Color.a 값이 0에 가까울수록 투명
        
    }

    void Update()
    {
        transform.Translate(new Vector3(0, moveSpeed * Time.deltaTime, 0));
        alpha.a = Mathf.Lerp(alpha.a, 0, alphaSpeed * Time.deltaTime);
        text.color = alpha;
        Invoke("DestroyObject", destroyTime);
    }


    private void DestroyObject()
    {
        Destroy(gameObject);
    }
}
