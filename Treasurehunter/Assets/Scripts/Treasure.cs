using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.ReorderableList.Element_Adder_Menu;
using UnityEngine;

public class Treasure : MonoBehaviour
{
    public Rigidbody2D target;      // Player(Audio Listener)
    public float distance;          // target과의 거리
    public AudioSource audioS;      // 효과음 재생할 오디오 컴포넌트

    public Rigidbody2D rigid;              // 물리적으로 거리 계산하기 위한 변수
    public SpriteRenderer rend;            // Order in Layer 변경하기 위한 변수

    public Animator anim; // Mi2141 추가

    public virtual void Dis()
    {
        audioS.maxDistance = 6;
        audioS.minDistance = 0.6f;
    }
    public void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        rend = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>(); // Mi2141 추가

        rend.sortingOrder = 0;      // 초기 Order in Layer는 0 (배경맵보다 아래)
        Dis();
        audioS.spatialBlend = 1;
        audioS.loop = true;
        audioS.rolloffMode = AudioRolloffMode.Linear;
        audioS.Play();
    }
    public virtual void Update()
    {
        Vector3 dirVec = target.position - rigid.position;                      // 벡터의 방향
        distance = Mathf.Sqrt((dirVec.x * dirVec.x) + (dirVec.y * dirVec.y));   // 벡터의 크기

        // 거리에 따라 재생음의 높낮이를 변경
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

    public virtual void Find() // 플레이어가 보물 찾기 스킬을 사용하면 Treasure.instance.Find();
    {
        audioS.Stop();
        rend.sortingOrder = 2;    // Order in Layer는 2 (배경맵보다 위)
        anim.SetTrigger("Open"); // Mi2141 추가
        Destroy(this.gameObject, 2f);
        Spawner.list.Remove(this.rigid.position);
        GameObject.Find("Spawner").GetComponent<Spawner>().Spawn();
    }
}