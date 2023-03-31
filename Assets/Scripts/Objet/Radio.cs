using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]  //����� ������Ʈ �߰�

public class Radio : Electronics
{
    private AudioSource audioSource = null;

    public override void Awake()
    {
        base.Awake(); //�θ�κб��� ȣ��������� �������Ұ�� 
        audioSource = GetComponent<AudioSource>();
    }

    public override void Use()
    {
        // if (!GETIsPowerOn()) return;
        Debug.Log("���� ���");
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
