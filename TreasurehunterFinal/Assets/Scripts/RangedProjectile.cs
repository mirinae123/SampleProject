using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedProjectile : MonoBehaviour
{
    public float lifetime;

    void Update()
    {
        lifetime -= Time.deltaTime;

        if (lifetime < 0)
        {
            Explode();
        }
    }

    void Explode()
    {
        RaycastHit2D[] hits = Physics2D.CircleCastAll(transform.position, 2f, Vector2.zero, 0f);

        foreach(RaycastHit2D hit in hits)
        {
            if (hit.collider.CompareTag("Monster"))
            {
                hit.collider.gameObject.GetComponent<Monster>().TakeDamage(1);
                AudioManager.instance.PlaySfx(AudioManager.SFX.Hit);
            }
        }

        gameObject.SetActive(false);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Monster"))
        {
            collision.gameObject.GetComponent<Monster>().TakeDamage(1);
            AudioManager.instance.PlaySfx(AudioManager.SFX.Hit);
        }
    }
}
