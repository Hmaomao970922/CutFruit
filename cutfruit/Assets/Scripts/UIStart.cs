using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class UIStart : MonoBehaviour
{

    private Button btnPlay;//开始按钮
    private Button btnSounds;//声音按钮
    private AudioSource audioSourceBG;
    private Image imgSound;
    public Sprite[] soundSprites;//声音按钮图片

    private void getComponents()
    {
        btnPlay = transform.Find("btnPlay").GetComponent<Button>();
        btnSounds = transform.Find("btnSounds").GetComponent<Button>();
        audioSourceBG = transform.Find("btnSounds").GetComponent<AudioSource>();
        imgSound = transform.Find("btnSounds").GetComponent<Image>();
    }

    void Start()
    {
        getComponents();

        btnPlay.onClick.AddListener(onPlayClick);//事件监听
        btnSounds.onClick.AddListener(onSoundsClick);
    }
    
    void onDestroy()//移除监听
    {
        btnPlay.onClick.RemoveListener(onPlayClick);
        btnSounds.onClick.RemoveListener(onSoundsClick);
    }
    void onPlayClick()
    {
        SceneManager.LoadScene("play", LoadSceneMode.Single);
    }

    void onSoundsClick()
    {
        if (audioSourceBG.isPlaying)//如果在放
        {
            audioSourceBG.Stop();
            imgSound.sprite = soundSprites[1];
        }
        else//反之
        {
            audioSourceBG.Play();
            imgSound.sprite = soundSprites[0];
        }
    }
}
