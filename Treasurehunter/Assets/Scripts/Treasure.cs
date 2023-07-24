using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Treasure : MonoBehaviour
{
    public Rigidbody2D target;      // Player(Audio Listener)
    public float distance;          // target���� �Ÿ�
    public AudioSource audioS;      // ȿ���� ����� ����� ������Ʈ

    Rigidbody2D rigid;              // ���������� �Ÿ� ����ϱ� ���� ����
    SpriteRenderer rend;            // Order in Layer �����ϱ� ���� ����
    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        rend = GetComponent<SpriteRenderer>();

        rend.sortingOrder = 0;      // �ʱ� Order in Layer�� 0 (���ʺ��� �Ʒ�)

        audioS.maxDistance = 6;
        audioS.minDistance = 0.6f;
        audioS.spatialBlend = 1;
        audioS.loop = true;
        audioS.rolloffMode = AudioRolloffMode.Linear;
        audioS.Play();
    }

    void Update()
    {
        Vector2 dirVec = target.position - rigid.position;                      // ������ ����
        distance = Mathf.Sqrt((dirVec.x * dirVec.x) + (dirVec.y * dirVec.y));   // ������ ũ��

        // �Ÿ��� ���� ������� �����̸� ����
        if (distance < 6 && distance > 4)
            audioS.pitch = 0.7f;
        else if (distance < 4 && distance > 1.5)
            audioS.pitch = 0.8f;
        else if (distance < 1.5 && distance > 0.6)
            audioS.pitch = 0.9f;
        else if (distance < 0.6)
            audioS.pitch = 1.1f;
    }

    public void Find() // �÷��̾ ���� ã�� ��ų�� ����ϸ� Treasure.instance.Find();
    {
        audioS.Stop();
        rend.sortingOrder = 2;    // Order in Layer�� 2 (���ʺ��� ��)
        UI.instance.AddTreasurePoint(transform.position);
        Destroy(this.gameObject, 2f);
    }
}