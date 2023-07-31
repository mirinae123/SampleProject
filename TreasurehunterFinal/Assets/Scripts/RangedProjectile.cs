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
}
