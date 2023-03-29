using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;



//[RequireComponent(typeof(VideoPlayer))]// ��ũ��Ʈ�� ������ �߰��� ���ִ±�� ��ȹ������ ������� (��ɺ���)

public class Screen : MonoBehaviour
{
   //�����÷��̾� ������Ʈ �߰� ����

    private VideoPlayer vp = null;


    private void Awake()
    {
        vp = GetComponent<VideoPlayer>();
        
        if(vp == null)
        {
            //�ν����� â�� �����÷��̾ ���»���

            vp = gameObject.AddComponent<VideoPlayer>(); //������Ʈ�� �߰��ϱ� �߰��Ҷ��� gameObject�� ����
            //�������Ʈ�� ������ �����ϴ°��̱� ����
            //���� �����÷��̾ �̹��߰��Ǿ��ִٸ� null�� ����
            //��Ÿ�Ӷ� ����� ������� Ư¡!

        }
        vp.playOnAwake = true;
        vp.isLooping = true;

        

       
        
    }
    private void Start()
    {
        
    }

    public void SetVideoClip(VideoClip _clip)
    {
        vp.clip = _clip;

    }
}