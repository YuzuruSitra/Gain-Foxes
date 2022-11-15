using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class soundCnt : MonoBehaviour
{
    [SerializeField]
    AudioSource bgmAudioSource;
    [SerializeField]
    AudioSource seAudioSource;

    //設定画面による音量変更
    public void setnewValueBGM(float newValueBGM)    //bgm音変更
    {
        bgmAudioSource.volume = Mathf.Clamp01(newValueBGM);
    }

    public void setnewValueSE(float newValueSE)    //bgm音変更
    {
        seAudioSource.volume = Mathf.Clamp01(newValueSE);
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
}
