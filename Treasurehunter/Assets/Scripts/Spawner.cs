using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public class Spawner : MonoBehaviour
{

    //보물 오브젝트가 임의의 위치에서 생성하기 위한 스크립트
    //초기에 보물 임의 생성/보물을 캘 경우, 플레이어 근방에서 보물 임의 생성 구현하고자 함

    [SerializeField]
    private int SpawnCount = 5;    //첫 시작시 생성될 보물의 수
    [SerializeField]
    public GameObject[] prefabArray;    //보물 임의 생성을 위한 배열

    public Rigidbody2D point;    //플레이어의 위치값

    public static List<Vector3> list = new List<Vector3>();   //미니맵에 구현할 보물 오브젝트 리스트
                                                              // Start is called before the first frame update

    public void Awake()    //초기에 보물이 임의의 위치에서 생성
    {

        for (int i = 0; i < SpawnCount; i++)
        {
            int index = Random.Range(0, prefabArray.Length);// 랜덤 생성될 보물 오브젝트의 순서
            float x = Random.Range(-10f, 10f);              // 초기 x 위치(보물 생성 범위)
            float y = Random.Range(-10f, 10f);              // 초기 y 위치(보물 생성 범위)
            Vector3 position = new Vector3(x, y, 0);        // 정의한 위치값의 기능 설정

            Instantiate(prefabArray[index], position, Quaternion.identity);  //랜덤 생성을 위한 함수
        }
    }
    public void Spawn()     //플레이어가 Dig할 경우에 호출할 생성 함수
    {
        Vector3 spawnpoint = point.position;
        int ran = Random.Range(1, 3);

        for (int i = 0; i < ran; i++)
        {
            int index = Random.Range(0, prefabArray.Length);
            float x = Random.Range(spawnpoint.x - 15f, spawnpoint.x + 15f);
            float y = Random.Range(spawnpoint.y - 15f, spawnpoint.y - 15f);
            Vector3 treasurepoint = new Vector3(x, y, 0);

            Instantiate(prefabArray[index], treasurepoint, Quaternion.identity);
        }
    }

}
