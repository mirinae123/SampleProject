using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{

    //보물 오브젝트가 임의의 위치에서 생성하기 위한 스크립트
    //instantiate 함수로 랜덤 생성을 유도하고자 함
    [SerializeField]
    private int SpawnCount = 10;    //첫 시작시 보물이 존재하도록 설정
    [SerializeField]
    public GameObject[] prefabArray;
    public GameObject treasure;

    public static List<GameObject> list = new List<GameObject>();
    // Start is called before the first frame update
    public void Awake()    //초기에 보물이 임의의 위치에서 생성
    {

        for (int i = 0; i < SpawnCount; i++)
        {
            int index = Random.Range(0, prefabArray.Length);// 랜덤 생성될 보물 오브젝트의 순서
            float x = Random.Range(-40f, 40f);              // 초기 x 위치(보물 생성 범위)
            float y = Random.Range(-40f, 40f);              // 초기 y 위치(보물 생성 범위)
            Vector3 position = new Vector3(x, y, 0);        // 정의한 위치값의 기능 설정

            Instantiate(prefabArray[index], position, Quaternion.identity);  //랜덤 생성을 위한 함수
            
        }
    }
    public void Check()
    {
        list.Add(treasure.gameObject);
    }            

    public void Miss()
    {
        list.Remove(treasure.gameObject);
    }

}
