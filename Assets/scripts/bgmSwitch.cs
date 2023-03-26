using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class bgmSwitch : MonoBehaviour
{
    public Button bgmbutton;
    public Text buttonText;
    public AudioSource bgm;
    public Sprite bgmOn;
    public Sprite bgmOff;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            bgmswitch();
        }
    }
    public void bgmswitch()
    {
        if(buttonText.text == "BGM ON")
        {

            bgmOFF();
        }
        else
        {
            bgmON();
        }
    }
    void bgmON()
    {
        bgmbutton.image.sprite = bgmOn;
        buttonText.text = "BGM ON";
        bgm.Play();
    }
    void bgmOFF()
    {
        bgmbutton.image.sprite = bgmOff;
        buttonText.text = "BGM OFF";
        bgm.Pause();
    }
}
