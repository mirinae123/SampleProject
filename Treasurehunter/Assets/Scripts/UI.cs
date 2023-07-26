using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using System.Linq.Expressions;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

// 보물을 발견하면 그 위치 정보를 AddTreasurePoint() 함수를 통해 추가한다.
// AddTreasurePoint() 함수는 해당 위치 정보를 가진 보물 아이콘을 미니맵에 추가한다.
// Update()에서는 현재 발견된 보물들을 미니맵 어디에 표시할지 실시간으로 조정한다.

// 체력바는 현재 체력과 최대 체력의 비에 따라 Lerp() 함수를 이용해 x값을 조정한다.

// 보물 아이콘이나 체력바가 범위를 벗어나면 가려지는 효과는 Mask 컴포넌트를 이용한다.

public class UI : MonoBehaviour
{
    public static UI instance;              // 다른 클래스에서 쉽게 접근할 수 있도록 public static 변수 선언

    public float maxHealth;                 // 플레이어 최대 체력
    public float currentHealth;             // 플레이어 현재 체력

    public GameObject treasurePointPrefab;  // 미니맵에 사용할 보물 아이콘

    public float score;                     // 플레이어의 현재 점수

    struct TreasurePoint                    // 발견한 보물 정보를 저장하기 위한 구조체
    {
        public GameObject minimapRef;       // 미니맵에 표시되는 보물 아이콘에 대한 레퍼런스
        public Vector3 position;            // 보물이 발견된 위치 정보
    }

    private List<TreasurePoint> treasurePoints; // 발견한 모든 보물의 정보를 저장하기 위한 리스트

    public RectTransform healthBarPos;      // 체력바의 RectTransform 컴포넌트에 대한 레퍼런스
    public Transform playerPos;             // 플레이어의 Transfrom 컴포넌트에 대한 레퍼런스
    public TMP_Text text;                   // 점수의 TextMeshPro 컴포넌트에 대한 레퍼런스 

    void Awake()
    {
        instance = this;                    // public static 변수 초기화

        treasurePoints = new List<TreasurePoint>(); // 보물 위치 리스트 초기화
        score = 0;                          // 플레이어 점수 초기화

        maxHealth = 100;                    // 플레이어 체력 초기화
        currentHealth = maxHealth;
    }

    void Update()
    {
        currentHealth -= Time.deltaTime;    // 자동 체력감소
        // 현재 체력과 최대 체력의 비율을 비교한 뒤, 1에 가까우면 체력바의 x를 0, 0에 가까우면 체력바의 x를 -60으로 둔다
        float healthPos = Mathf.Lerp(-60f, 0f, currentHealth / maxHealth);
        healthBarPos.localPosition = new Vector3(healthPos, healthBarPos.localPosition.y, healthBarPos.localPosition.z);

        text.text = score.ToString();           // 점수 표기 업데이트

        if (treasurePoints.Count == 0) return;  // 보물 위치 정보가 없으면 더 이상 진행하지 않는다

        // 보물 위치 정보가 있으면 모든 위치 정보에 대해...
        foreach (var point in treasurePoints)
        {
            float minimapPosX = (point.position.x - playerPos.position.x) * 3;  // 플레이어와 보물의 위치를 비교한 뒤 조정값을 곱해준다
            float minimapPosY = (point.position.y - playerPos.position.y) * 3;  // 조정값이 작을수록 지도가 더 넓은 범위를 표시할 수 있다

            point.minimapRef.GetComponent<RectTransform>().localPosition = new Vector3(minimapPosX, minimapPosY, 0f);   // 위에서 구한 위치를 보물 아이콘의 RectTransform 컴포넌트에 적용
        }
    }

    // 다른 클래스에서 보물 위치 정보를 추가하고자 할 때 사용하는 함수
    public void AddTreasurePoint(Vector3 position)
    {
        float minimapPosX = (position.x - playerPos.position.x) * 3;    // 플레이어와 보물의 위치를 비교한 뒤 조정값을 곱해준다
        float minimapPosY = (position.y - playerPos.position.y) * 3;

        GameObject temp = Instantiate(treasurePointPrefab);                 // Prefab을 불러와 보물 아이콘을 나타내는 게임 오브젝트 생성
        temp.transform.parent = GameObject.Find("Minimap Mask").transform;  // 생성된 오브젝트를 Minimap Mask 오브젝트의 자식으로 둔다
        temp.GetComponent<RectTransform>().localPosition = new Vector3(minimapPosX, minimapPosY, 0f);   // 생성된 오브젝트의 RectTransfrom 조정

        TreasurePoint point = new TreasurePoint();  // 보물 위치 정보를 담기 위한 구조체 생성
        point.minimapRef = temp;                    // 구조체에 아이콘 오브젝트 레퍼런스 대입
        point.position = position;                  // 구조체에 실제 보물 위치 대입

        treasurePoints.Add(point);  // 리스트에 보물 위치 정보 추가
    }
}
