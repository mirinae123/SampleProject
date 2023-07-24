using System.Collections;
using System.Collections.Generic;
//using Unity.VisualScripting;
using UnityEngine;

public class Box : MonoBehaviour
{ 
    
    Rigidbody2D rb;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Player")
            InvokeRepeating("PlayBridge", 0f, 64f);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Player")
        {
            CancelInvoke("PlayBridge");
            AudioManager.instance.StopSfx(AudioManager.SFX.Bridge);
        }
    }

    private void PlayBridge()
    {
        AudioManager.instance.PlaySfx(AudioManager.SFX.Bridge);
    }
}
