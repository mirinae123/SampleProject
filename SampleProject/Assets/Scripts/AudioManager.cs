using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    // public static ������ instance�� ���� ������ AudioManager�� ��ü�� ������ ����
    // �̷��� �ϸ� AudioManager ��ü�� ��� Ŭ���������� ������ �� ����
    public static AudioManager instance;

    [Header("BGM Player")]                  // BGM �÷��̾� ���� ����
    public AudioClip bgmClip;               // BGM���� ����� Ŭ�� ����
    public float bgmVolume = 1;             // BGM ����
    AudioSource bgmPlayer;                  // BGM�� ����� ����� ������Ʈ

    [Header("SFX Player")]                  // SFX �÷��̾� ���� ����
    public AudioClip[] sfxClips;            // SFX�� ����� Ŭ�� ���ϵ��� �迭
    public float sfxVolume = 1;             // SFX ����
    AudioSource[] sfxPlayers;               // SFX�� ����� ����� ������Ʈ���� �迭
    public int channels = 8;                // ä���� ����

    int channelIndex;                       // ä�� ��� �˰��� �ʿ��� ����. �ʿ�� ���

    public enum SFX { Dig, Fail, Success, Bridge };                  // �� SFX Ŭ���� �����Ǵ� enum. �ʿ�� �߰�

    // ������Ʈ ���� ������ ����
    private void Awake()
    {
        instance = this;                    // instance ������ ���� ��ü ����
        Init();                             // �ʱ�ȭ �۾�
    }

    // ����� �ý��� �ʱ�ȭ
    void Init()
    {
        GameObject bgmObject = new GameObject("bgmPlayer");     // BGM �÷��̾� ������Ʈ ����
        bgmObject.transform.parent = transform;                 // BGM �÷��̾ ����� �ý����� �ڽ� ������Ʈ�� ��
        bgmPlayer = bgmObject.AddComponent<AudioSource>();      // BGM �÷��̾ ����� ������Ʈ �߰�
        bgmPlayer.loop = true;                                  // �ݺ� ��� Ȱ��ȭ
        bgmPlayer.volume = bgmVolume;                           // ���� ����
        bgmPlayer.clip = bgmClip;                               // BGM Ŭ�� ���� ����

        GameObject sfxObject = new GameObject("sfxPlayer");     // SFX �÷��̾� ������Ʈ ����
        sfxObject.transform.parent = transform;                 // SFX �÷��̾ ����� �ý����� �ڽ� ������Ʈ�� ��
        sfxPlayers = new AudioSource[channels];                 // SFX �÷��̾ �߰��� ����� ������Ʈ �迭 ����

        for (int i = 0; i < channels; i++)
        {
            sfxPlayers[i] = sfxObject.AddComponent<AudioSource>();  // ä�� ������ŭ ����� ������Ʈ �߰��� �� �迭�� ����
            sfxPlayers[i].playOnAwake = false;                      // ���۽� ���� ��Ȱ��ȭ
            sfxPlayers[i].volume = sfxVolume;                       // ���� ����
        }
    }

    // Ŭ�� ������ bgmClip, sfxClips[]�� �����
    // Ŭ���� ����� ����� ������Ʈ�� bgmPlayer, sfxPlayers[]�� �����
    public void PlaySfx(SFX sfx)
    {
        for(int i = 0; i < channels; ++i)
        {
            int loopIndex = (i + channelIndex) % channels;

            if (sfxPlayers[loopIndex].isPlaying)  // �̹� ������̴� ȿ������ ����°��� ����
                continue;

            channelIndex = loopIndex;
            sfxPlayers[loopIndex].clip = sfxClips[(int)sfx];
            sfxPlayers[loopIndex].Play();
            break;
        }
        
    }
}
