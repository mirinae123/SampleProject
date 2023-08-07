using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemAcquired : MonoBehaviour
{
    private float lifetime = 0;

    void Update()
    {
        lifetime += Time.deltaTime;

        if (lifetime < .2f)
        {
            transform.position += new Vector3(0, Time.deltaTime * 4, 0);
        }
        else if (lifetime > 1 && lifetime < 1.5f)
        {
            GetComponent<SpriteRenderer>().color -= new Color(0, 0, 0, Time.deltaTime);
        }
        else if (lifetime > 1.5f) 
        {
            Destroy(gameObject);
        }
    }
}
