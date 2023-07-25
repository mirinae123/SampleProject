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
        Vector2 dirVec = target.position - rigid.position;                      // ∫§≈Õ¿« πÊ«‚
        distance = Mathf.Sqrt((dirVec.x * dirVec.x) + (dirVec.y * dirVec.y));   // ∫§≈Õ¿« ≈©±‚

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
    }
    public override void Find() 
    {
        base.Find();
    }
}
