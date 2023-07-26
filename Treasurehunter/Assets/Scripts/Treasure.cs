using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.ReorderableList.Element_Adder_Menu;
using UnityEngine;

public class Treasure : MonoBehaviour
{
    public Rigidbody2D target;      // Player(Audio Listener)
    public float distance;          // target���� �Ÿ�
    public AudioSource audioS;      // ȿ���� ����� ����� ������Ʈ

    public Rigidbody2D rigid;              // ���������� �Ÿ� ����ϱ� ���� ����
    public SpriteRenderer rend;            // Order in Layer �����ϱ� ���� ����

    public Animator anim; // Mi2141 �߰�

    public virtual void Dis()
    {
        audioS.maxDistance = 6;
        audioS.minDistance = 0.6f;
    }
    public void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        rend = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>(); // Mi2141 �߰�

        rend.sortingOrder = 0;      // �ʱ� Order in Layer�� 0 (���ʺ��� �Ʒ�)
        Dis();
        audioS.spatialBlend = 1;
        audioS.loop = true;
        audioS.rolloffMode = AudioRolloffMode.Linear;
        audioS.Play();
    }
    public virtual void Update()
    {
        Vector3 dirVec = target.position - rigid.position;                      // ������ ����
        distance = Mathf.Sqrt((dirVec.x * dirVec.x) + (dirVec.y * dirVec.y));   // ������ ũ��

        // �Ÿ��� ���� ������� �����̸� ����
        if (distance < 6 && distance > 4)
        {
            audioS.pitch = 0.7f;
            Spawner.list.Add(this.rigid.position);
        }
            
        else if (distance < 4 && distance > 1.5)
            audioS.pitch = 0.8f;
        else if (distance < 1.5 && distance > 0.6)
            audioS.pitch = 0.9f;
        else if (distance < 0.6)
            audioS.pitch = 1.1f;
    }

    public virtual void Find() // �÷��̾ ���� ã�� ��ų�� ����ϸ� Treasure.instance.Find();
    {
        audioS.Stop();
        rend.sortingOrder = 2;    // Order in Layer�� 2 (���ʺ��� ��)
        anim.SetTrigger("Open"); // Mi2141 �߰�
        Destroy(this.gameObject, 2f);
        Spawner.list.Remove(this.rigid.position);
        GameObject.Find("Spawner").GetComponent<Spawner>().Spawn();
    }
}