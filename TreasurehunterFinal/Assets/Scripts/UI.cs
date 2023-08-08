using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using System.Linq.Expressions;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

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
    public TMP_Text result;                 
    public TMP_Text best;                   
    public GameObject weapon;               // ���� �������� ���� ������Ʈ�� ���� ���۷���
    public Sprite[] weaponIcons;            // ���� ������ ��������Ʈ�� ���� ���۷���

    private GameObject minimap;             // �̴ϸ� â ������Ʈ
    private GameObject status;              // ���� â ������Ʈ
    private GameObject gameOver;            // ���ӿ��� â ������Ʈ

    void Awake()
    {
        instance = this;                    // public static ���� �ʱ�ȭ

        treasurePoints = new List<TreasurePoint>(); // ���� ��ġ ����Ʈ �ʱ�ȭ
        score = 0;                          // �÷��̾� ���� �ʱ�ȭ

        maxHealth = 100;                    // �÷��̾� ü�� �ʱ�ȭ
        currentHealth = maxHealth;

        minimap = GameObject.Find("Minimap");       // UI ������Ʈ �ʱ�ȭ
        status = GameObject.Find("Status");
        gameOver = GameObject.Find("Game Over");
        gameOver.SetActive(false);                  // ���ӿ��� â�� ���� ���� �� ��Ȱ��ȭ ����
    }

    void Update()
    {
        currentHealth -= Time.deltaTime;    // �ڵ� ü�°���

        // ���� ü�°� �ִ� ü���� ������ ���� ��, 1�� ������ ü�¹��� x�� 0, 0�� ������ ü�¹��� x�� -60���� �д�
        float healthPos = Mathf.Lerp(-60f, 0f, currentHealth / maxHealth);
        healthBarPos.localPosition = new Vector3(healthPos, healthBarPos.localPosition.y, healthBarPos.localPosition.z);

        weapon.GetComponent<Image>().sprite = weaponIcons[(int)AttackSystem.currentWeapon];
        weapon.GetComponent<Slider>().value = AttackSystem.currentCooldown / AttackSystem.maxCooldown;

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

    // ���ӿ��� �� ȣ���ϴ� �Լ�
    public void GameOver()
    {
        
        gameOver.SetActive(true);   // ���ӿ��� â Ȱ��ȭ
        minimap.SetActive(false);   // ������ UI ��Ȱ��ȭ
        status.SetActive(false);
        Save();
        result.text = score.ToString();
        best.text = PlayerPrefs.GetFloat("BestScore").ToString();
    }

    public void Hit()
    {
        currentHealth -= 10f;
    }

    // ���ӿ��� â���� ��ư Ŭ�� �� �����
    public void Restart()
    {
        SceneManager.LoadScene("Title Scene");
    }

    public void Save()
    {
        if(score < PlayerPrefs.GetFloat("BestScore"))
        {
            if(score < PlayerPrefs.GetFloat("SecondScore"))
            {
                if (score < PlayerPrefs.GetFloat("ThirdScore"))
                    return;
                PlayerPrefs.SetFloat("ThirdScore", score);
                return;
            }
            PlayerPrefs.SetFloat("ThirdScore", PlayerPrefs.GetFloat("SecondScore"));
            PlayerPrefs.SetFloat("SecondScore", score);
            return;
        }
        if (score == PlayerPrefs.GetFloat("BestScore")) return;
        PlayerPrefs.SetFloat("ThirdScore", PlayerPrefs.GetFloat("SecondScore"));
        PlayerPrefs.SetFloat("SecondScore", PlayerPrefs.GetFloat("BestScore"));
        PlayerPrefs.SetFloat("BestScore", score);
    }
}
