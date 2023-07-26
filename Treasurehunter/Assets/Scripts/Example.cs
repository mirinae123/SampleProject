using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Example : MonoBehaviour
{
    public Vector2 inputVec;
    SpriteRenderer spriter;
    // Start is called before the first frame update
    void Start()
    {
        spriter = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        inputVec.x = Input.GetAxisRaw("Horizontal");
    }

    void LateUpdate()
    {
        if (inputVec.x != 0)
        {
            spriter.flipX = inputVec.x < 0;
        }
    }
}
