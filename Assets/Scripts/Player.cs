using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Vector2 inputVec;

    Rigidbody2D rigid;
    public float speed;

    public bool isMovable;
    public bool isAction;

    private GameObject treasure;


    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();

        isMovable = true;
        isAction = false;

        treasure = null;
    }

    // Update is called once per frame
    void Update()
    {
        inputVec.x = Input.GetAxisRaw("Horizontal");
        inputVec.y = Input.GetAxisRaw("Vertical");

        if (Input.GetKey(KeyCode.Z) && !isAction)
        {
            isMovable = false;
            isAction = true;
            AudioManager.instance.PlaySfx(AudioManager.SFX.Dig);
            Invoke("Dig", 1);
        }
    }

    void FixedUpdate()
    {
        Vector2 nextVec = inputVec.normalized * speed * Time.fixedDeltaTime;

        if(isMovable)
            rigid.MovePosition(rigid.position + nextVec);
    }

    void Dig()
    {
        if (treasure)
        {
            treasure.GetComponent<Treasure>().Find();
            AudioManager.instance.PlaySfx(AudioManager.SFX.Success);
        }
        else
        {
            AudioManager.instance.PlaySfx(AudioManager.SFX.Fail);
        }
        isAction = false;
        isMovable = true;
    }
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Treasure"))
        {
            treasure = col.gameObject;
        }
    }
    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Treasure"))
        {
            treasure = null;
        }
    }

}
