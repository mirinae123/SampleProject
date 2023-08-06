using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSpawn : MonoBehaviour
{
    public Transform[] Point;
    public SpawnStat[] Stat;

    int level;
    float timer;

    public void Awake()
    {
        Point = GetComponentsInChildren<Transform>();
        level = 0;
    }

    public void Update()
    {
        timer += Time.deltaTime;
        level = Mathf.Min(Mathf.FloorToInt(GameManager.instance.GameTime / 10f), Stat.Length - 1);

        if (timer > Stat[level].SpawnTime)
        {
            timer = 0;
            Spawn();
        }
    }

    public void Spawn()
    {
        GameObject mobs = GameManager.instance.pool.Get(0);
        mobs.transform.position = Point[Random.Range(1, Point.Length)].position;
        mobs.GetComponent<Monster>().Init(Stat[level]);
    }
}
[System.Serializable]
public class SpawnStat
{
    public int type;
    public float SpawnTime;
    public int Health;
    public float Speed;
}
