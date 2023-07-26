using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using System.Linq.Expressions;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

// ������ �߰��ϸ� �� ��ġ ������ AddTreasurePoint() �Լ��� ���� �߰��Ѵ�.
// AddTreasurePoint() �Լ��� �ش� ��ġ ������ ���� ���� �������� �̴ϸʿ� �߰��Ѵ�.
// Update()������ ���� �߰ߵ� �������� �̴ϸ� ��� ǥ������ �ǽð����� �����Ѵ�.

// ü�¹ٴ� ���� ü�°� �ִ� ü���� �� ���� Lerp() �Լ��� �̿��� x���� �����Ѵ�.

// ���� �������̳� ü�¹ٰ� ������ ����� �������� ȿ���� Mask ������Ʈ�� �̿��Ѵ�.

public class UI : MonoBehaviour
{
    public static UI instance;              // �ٸ� Ŭ�������� ���� ������ �� �ֵ��� public static ���� ����

    public float maxHealth;                 // �÷��̾� �ִ� ü��
    public float currentHealth;             // �÷��̾� ���� ü��

    public GameObject treasurePointPrefab;  // �̴ϸʿ� ����� ���� ������

    public float score;                     // �÷��̾��� ���� ����

    struct TreasurePoint                    // �߰��� ���� ������ �����ϱ� ���� ����ü
    {
        public GameObject minimapRef;       // �̴ϸʿ� ǥ�õǴ� ���� �����ܿ� ���� ���۷���
        public Vector3 position;            // ������ �߰ߵ� ��ġ ����
    }

    private List<TreasurePoint> treasurePoints; // �߰��� ��� ������ ������ �����ϱ� ���� ����Ʈ

    public RectTransform healthBarPos;      // ü�¹��� RectTransform ������Ʈ�� ���� ���۷���
    public Transform playerPos;             // �÷��̾��� Transfrom ������Ʈ�� ���� ���۷���
    public TMP_Text text;                   // ������ TextMeshPro ������Ʈ�� ���� ���۷��� 

    void Awake()
    {
        instance = this;                    // public static ���� �ʱ�ȭ

        treasurePoints = new List<TreasurePoint>(); // ���� ��ġ ����Ʈ �ʱ�ȭ
        score = 0;                          // �÷��̾� ���� �ʱ�ȭ

        maxHealth = 100;                    // �÷��̾� ü�� �ʱ�ȭ
        currentHealth = maxHealth;
    }

    void Update()
    {
        currentHealth -= Time.deltaTime;    // �ڵ� ü�°���
        // ���� ü�°� �ִ� ü���� ������ ���� ��, 1�� ������ ü�¹��� x�� 0, 0�� ������ ü�¹��� x�� -60���� �д�
        float healthPos = Mathf.Lerp(-60f, 0f, currentHealth / maxHealth);
        healthBarPos.localPosition = new Vector3(healthPos, healthBarPos.localPosition.y, healthBarPos.localPosition.z);

        text.text = score.ToString();           // ���� ǥ�� ������Ʈ

        if (treasurePoints.Count == 0) return;  // ���� ��ġ ������ ������ �� �̻� �������� �ʴ´�

        // ���� ��ġ ������ ������ ��� ��ġ ������ ����...
        foreach (var point in treasurePoints)
        {
            float minimapPosX = (point.position.x - playerPos.position.x) * 3;  // �÷��̾�� ������ ��ġ�� ���� �� �������� �����ش�
            float minimapPosY = (point.position.y - playerPos.position.y) * 3;  // �������� �������� ������ �� ���� ������ ǥ���� �� �ִ�

            point.minimapRef.GetComponent<RectTransform>().localPosition = new Vector3(minimapPosX, minimapPosY, 0f);   // ������ ���� ��ġ�� ���� �������� RectTransform ������Ʈ�� ����
        }
    }

    // �ٸ� Ŭ�������� ���� ��ġ ������ �߰��ϰ��� �� �� ����ϴ� �Լ�
    public void AddTreasurePoint(Vector3 position)
    {
        float minimapPosX = (position.x - playerPos.position.x) * 3;    // �÷��̾�� ������ ��ġ�� ���� �� �������� �����ش�
        float minimapPosY = (position.y - playerPos.position.y) * 3;

        GameObject temp = Instantiate(treasurePointPrefab);                 // Prefab�� �ҷ��� ���� �������� ��Ÿ���� ���� ������Ʈ ����
        temp.transform.parent = GameObject.Find("Minimap Mask").transform;  // ������ ������Ʈ�� Minimap Mask ������Ʈ�� �ڽ����� �д�
        temp.GetComponent<RectTransform>().localPosition = new Vector3(minimapPosX, minimapPosY, 0f);   // ������ ������Ʈ�� RectTransfrom ����

        TreasurePoint point = new TreasurePoint();  // ���� ��ġ ������ ��� ���� ����ü ����
        point.minimapRef = temp;                    // ����ü�� ������ ������Ʈ ���۷��� ����
        point.position = position;                  // ����ü�� ���� ���� ��ġ ����

        treasurePoints.Add(point);  // ����Ʈ�� ���� ��ġ ���� �߰�
    }
}
