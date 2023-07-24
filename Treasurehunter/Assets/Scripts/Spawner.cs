using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{

    //���� ������Ʈ�� ������ ��ġ���� �����ϱ� ���� ��ũ��Ʈ
    //instantiate �Լ��� ���� ������ �����ϰ��� ��
    [SerializeField]
    private int SpawnCount = 10;    //ù ���۽� ������ �����ϵ��� ����
    [SerializeField]
    public GameObject[] prefabArray;
    public GameObject treasure;

    public static List<GameObject> list = new List<GameObject>();
    // Start is called before the first frame update
    public void Awake()    //�ʱ⿡ ������ ������ ��ġ���� ����
    {

        for (int i = 0; i < SpawnCount; i++)
        {
            int index = Random.Range(0, prefabArray.Length);// ���� ������ ���� ������Ʈ�� ����
            float x = Random.Range(-40f, 40f);              // �ʱ� x ��ġ(���� ���� ����)
            float y = Random.Range(-40f, 40f);              // �ʱ� y ��ġ(���� ���� ����)
            Vector3 position = new Vector3(x, y, 0);        // ������ ��ġ���� ��� ����

            Instantiate(prefabArray[index], position, Quaternion.identity);  //���� ������ ���� �Լ�
            
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
