using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Treasure : MonoBehaviour
{
    public static Treasure instance;

    public Rigidbody2D target;      // Player(Audio Listener)
    public float distance;          // target과의 거리
    public AudioSource audioS;      // 효과음 재생할 오디오 컴포넌트

    Rigidbody2D rigid;              // 물리적으로 거리 계산하기 위한 변수
    SpriteRenderer rend;            // Order in Layer 변경하기 위한 변수
    void Awake()
    {
        instance = this;
        rigid = GetComponent<Rigidbody2D>();
        rend = GetComponent<SpriteRenderer>();

        rend.sortingOrder = 0;      // 초기 Order in Layer는 0 (배경맵보다 아래)

        audioS.maxDistance = 6;
        audioS.minDistance = 0.6f;
        audioS.spatialBlend = 1;
        audioS.loop = true;
        audioS.rolloffMode = AudioRolloffMode.Linear;
        //audioS.Play(); // Debug 용 코드
    }

    void Update()
    {
        Vector2 dirVec = target.position - rigid.position;                      // 벡터의 방향
        distance = Mathf.Sqrt((dirVec.x * dirVec.x) + (dirVec.y * dirVec.y));   // 벡터의 크기

        // 거리에 따라 재생음의 높낮이를 변경
        if (distance < 6 && distance > 4)
            audioS.pitch = 0.7f;
        else if (distance < 4 && distance > 1.5)
            audioS.pitch = 0.8f;
        else if (distance < 1.5 && distance > 0.6)
            audioS.pitch = 0.9f;
        else if (distance < 0.6)
            audioS.pitch = 1.1f;


    }


    public void Play() // Player가 보물 탐지 스킬을 사용하면 Treasure.instance.Play();
    {
        audioS.Play();
    }
    public void Stop() // Player가 보물 탐지 스킬을 사용중지하면 Treasure.instance.Stop();
    {
        audioS.Stop();
    }

    public void Find() // 플레이어가 보물 찾기 스킬을 사용하면 Treasure.instance.Find();
    {
        audioS.Stop();
        if (distance < 0.6) // 거리를 통한 충돌 여부
        {
            rend.sortingOrder = 2;    // Order in Layer는 2 (배경맵보다 위)
            Destroy(this.gameObject, 2f);
            //AudioManager.instance.PlaySfx(AudioManager.SFX.Win);
        }
        else
        {
            //AudioManager.instance.PlaySfx(AudioManager.SFX.Lose);
        }
    }
}