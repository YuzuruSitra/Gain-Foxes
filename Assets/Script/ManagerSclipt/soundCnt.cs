using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//サウンド管理
public class SoundCnt : MonoBehaviour
{
    [SerializeField]
    private AudioSource bgmAudioSource;
    [SerializeField]
    private AudioSource seAudioSource;
    [SerializeField]
    private AudioSource seReadAudioSource;
    [SerializeField]
    private AudioSource seEnvironmentalSource;


    //設定画面による音量変更
    public void SetnewValueBGM(float newValueBGM)    //bgm音変更
    {
        bgmAudioSource.volume = Mathf.Clamp01(newValueBGM);
    }

    public void SetnewValueSE(float newValueSE)    //bgm音変更
    {
        seAudioSource.volume = Mathf.Clamp01(newValueSE);
        seReadAudioSource.volume = Mathf.Clamp01(newValueSE);
        seEnvironmentalSource = GameObject.Find("EnvironmentalSource").GetComponent<AudioSource> ();
        seEnvironmentalSource.volume = Mathf.Clamp01(newValueSE);
    }

    void Start()
    {
        GameObject soundManager = CheckOtherSoundManager();
        bool checkResult = soundManager != null && soundManager != gameObject;

        if (checkResult)
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
    }

    GameObject CheckOtherSoundManager()
    {
        return GameObject.FindGameObjectWithTag("SoundManager");
    }

    public void PlayBgm(AudioClip clip)
    {
        bgmAudioSource.clip = clip;
        if(clip == null)
        {
            return;
        }
        bgmAudioSource.Play();
    }

    public void PlaySe(AudioClip clip)
    {
        if(clip == null)
        {
            return;
        }
        seAudioSource.PlayOneShot(clip);
    }

    //文字表示SE再生
    public void PlayReadSe(AudioClip clip)
    {
        if(clip == null)
        {
            return;
        }
        seReadAudioSource.PlayOneShot(clip);
    }

    //再生しているかチェック
    public bool CheckReadSE()
    {
        return seReadAudioSource.isPlaying;
    }

    //文字表示SE再生停止
    public void StopSE()
    {
        seReadAudioSource.Stop();
    }
}
