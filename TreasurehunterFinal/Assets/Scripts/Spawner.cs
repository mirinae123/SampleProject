using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public class Spawner : MonoBehaviour
{

    //���� ������Ʈ�� ������ ��ġ���� �����ϱ� ���� ��ũ��Ʈ
    //�ʱ⿡ ���� ���� ����/������ Ķ ���, �÷��̾� �ٹ濡�� ���� ���� ���� �����ϰ��� ��

    [SerializeField]
    private int SpawnCount = 5;    //ù ���۽� ������ ������ ��
    [SerializeField]
    public GameObject[] prefabArray;    //���� ���� ������ ���� �迭

    public Rigidbody2D point;    //�÷��̾��� ��ġ��

    public static List<Vector3> list = new List<Vector3>();   //�̴ϸʿ� ������ ���� ������Ʈ ����Ʈ
                                                              // Start is called before the first frame update

    public void Awake()    //�ʱ⿡ ������ ������ ��ġ���� ����
    {

        for (int i = 0; i < SpawnCount; i++)
        {
            int index = Random.Range(0, prefabArray.Length);// ���� ������ ���� ������Ʈ�� ����
            float x = Random.Range(-10f, 10f);              // �ʱ� x ��ġ(���� ���� ����)
            float y = Random.Range(-10f, 10f);              // �ʱ� y ��ġ(���� ���� ����)
            Vector3 position = new Vector3(x, y, 0);        // ������ ��ġ���� ��� ����

            Instantiate(prefabArray[index], position, Quaternion.identity);  //���� ������ ���� �Լ�
        }
    }
    public void Spawn()     //�÷��̾ Dig�� ��쿡 ȣ���� ���� �Լ�
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
