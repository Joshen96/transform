using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]  //오디오 컴포넌트 추가

public class Radio : Electronics
{
    private AudioSource audioSource = null;

    public override void Awake()
    {
        base.Awake(); //부모부분까지 호출해줘야함 재정의할경우 
        audioSource = GetComponent<AudioSource>();
    }

    public override void Use()
    {
        // if (!GETIsPowerOn()) return;
        Debug.Log("라디오 사용");
        if (!audioSource.isPlaying) {
            audioSource.Play();
            Debug.Log(productName + "Play");
                }
        else {
            audioSource.Stop();
            Debug.Log(productName + "Stop");
        }
    }
}
