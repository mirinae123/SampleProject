using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    // public static 변수인 instance에 현재 생성된 AudioManager의 객체를 대입할 예정
    // 이렇게 하면 AudioManager 객체를 어느 클래스에서든 접근할 수 있음
    public static AudioManager instance;

    [Header("BGM Player")]                  // BGM 플레이어 변수 선언
    public AudioClip bgmClip;               // BGM으로 사용할 클립 파일
    public float bgmVolume = 1;             // BGM 볼륨
    AudioSource bgmPlayer;                  // BGM을 재생할 오디오 컴포넌트

    [Header("SFX Player")]                  // SFX 플레이어 변수 선언
    public AudioClip[] sfxClips;            // SFX로 사용할 클립 파일들의 배열
    public float sfxVolume = 1;             // SFX 볼륨
    AudioSource[] sfxPlayers;               // SFX를 재생할 오디오 컴포넌트들의 배열
    public int channels = 8;                // 채널의 개수

    int channelIndex;                       // 채널 사용 알고리즘에 필요한 변수. 필요시 사용

    public enum SFX { };                    // 각 SFX 클립에 대응되는 enum. 필요시 추가

    // 오브젝트 최초 생성시 실행
    private void Awake()
    {
        instance = this;                    // instance 변수에 현재 객체 대입
        Init();                             // 초기화 작업
    }

    // 오디오 시스템 초기화
    void Init()
    {
        GameObject bgmObject = new GameObject("bgmPlayer");     // BGM 플레이어 오브젝트 생성
        bgmObject.transform.parent = transform;                 // BGM 플레이어를 오디오 시스템의 자식 오브젝트로 둠
        bgmPlayer = bgmObject.AddComponent<AudioSource>();      // BGM 플레이어에 오디오 컴포넌트 추가
        bgmPlayer.loop = true;                                  // 반복 재생 활성화
        bgmPlayer.volume = bgmVolume;                           // 볼륨 적용
        bgmPlayer.clip = bgmClip;                               // BGM 클립 파일 적용

        GameObject sfxObject = new GameObject("sfxPlayer");     // SFX 플레이어 오브젝트 생성
        sfxObject.transform.parent = transform;                 // SFX 플레이어를 오디오 시스템의 자식 오브젝트로 둠
        sfxPlayers = new AudioSource[channels];                 // SFX 플레이어에 추가할 오디오 컴포넌트 배열 생성

        for (int i = 0; i < channels; i++)
        {
            sfxPlayers[i] = sfxObject.AddComponent<AudioSource>();  // 채널 개수만큼 오디오 컴포넌트 추가한 후 배열에 저장
            sfxPlayers[i].playOnAwake = false;                      // 시작시 실행 비활성화
            sfxPlayers[i].volume = sfxVolume;                       // 볼륨 적용
        }
    }

    // 클립 파일은 bgmClip, sfxClips[]에 저장됨
    // 클립을 재생할 오디오 컴포넌트는 bgmPlayer, sfxPlayers[]에 저장됨
}
