using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreasureB : Treasure
{
    public override void Dis()
    {
        audioS.maxDistance = 8;
        audioS.minDistance = 1;
    }
    // Update is called once per frame
    public override void Update()
    {
        Vector2 dirVec = target.position - rigid.position;                      // 벡터의 방향
        distance = Mathf.Sqrt((dirVec.x * dirVec.x) + (dirVec.y * dirVec.y));   // 벡터의 크기

        if (distance < 10 && distance > 7)
        {
            audioS.pitch = 0.7f;
            Spawner.list.Add(this.rigid.position);
        }
        else if (distance < 7 && distance > 4)
            audioS.pitch = 0.8f;
        else if (distance < 4 && distance > 1)
            audioS.pitch = 0.9f;
        else if (distance < 1)
            audioS.pitch = 1.1f;

        if (!Player.isAlive)    // 게임 종료시 소리 멈춤
            audioS.Stop();
    }
    private new void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            Debug.Log("충돌a");
            Player.treasure = gameObject;
            Spawner.list.Add(rigid.position);
            UI.instance.AddTreasurePoint(transform.position);
        }
    }
    private new void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            Debug.Log("충돌 나감a");
            Player.treasure = null;
        }
    }

    public override void Find() 
    {
        base.Find();
    }
}
