using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedProjectile : MonoBehaviour
{
    public float lifetime;

    void Update()
    {
        lifetime -= Time.deltaTime;

        if (lifetime < 0) gameObject.SetActive(false);
    }

    void Explode()
    {
        RaycastHit2D[] hits = Physics2D.CircleCastAll(transform.position, 2f, Vector2.zero, 0f);

        foreach(RaycastHit2D hit in hits)
        {
            if (hit.collider.CompareTag("Monster"))
            {
                GameObject.Find("Monster").GetComponent<Monster>().TakeDamage(1);
                // 몬스터에게 피해 입힘
            }
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Monster"))
        {
            GameObject.Find("Monster").GetComponent<Monster>().TakeDamage(1);
            // 몬스터에게 피해 입힘
        }
    }
}
