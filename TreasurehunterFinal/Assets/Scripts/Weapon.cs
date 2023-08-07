using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public int id;
    public int prefabId;
    public float damage;
    public int count;
    public float speed;

    // Update is called once per frame
    void Start()
    {
        Init();
    }
    void Update()
    {
        switch(id){
            case 0:
            transform.Rotate(Vector3.forward * speed * Time.deltaTime);
            break;
            default:
            break;
        }
    }

    public void Init()
    {
        switch(id){
            case 0:
            speed = -150;
            Batch();
            Debug.Log("LOOOOL");
            break;
            default:
            break;
        }
    }
    void Batch()
    {
        for(int index =0; index <count; index++){
            Transform bullet = GameManager.instance.pool.Get(prefabId).transform;
            bullet.parent = transform;
            bullet.GetComponent<Bullet>().Init(damage, -1);
        }
    }
}
