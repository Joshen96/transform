using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

//Z-Fighting ����
//Billboard = ����
//Pivot = ȸ��
//Screen = ������� 
//���� �����ҿ���
public class Billboard : MonoBehaviour
{
    [SerializeField]
    private Transform targetTr = null; //Ÿ�� �޾ƾ���

    
    private AnchorPivot apj = null;
    private Screen screen = null;

    private void Awake()
    {
        // GetComponentInChildren
        //GetComponentsInChildren
        //transform.GetChild

        //GameObject.Find  == ã�� ����� ��� 
        apj = GetComponentInChildren<AnchorPivot>(); //�̷������� ã�� ������ȿ� �ڽ����� �ִ� ��ũ��Ʈ
        screen = GetComponentInChildren<Screen>();

        //ȸ���� ��Ŀ�ǹ����ϰ� �󸶳� ���ƾ��ϴ��� ����� �����尡 �Ѵ�.

    }

    private void Update()
    {
        //�����忡�� ����ϰ� �ǹ��� �������� �����ִ� ����

        float angleRad = CalcAngleToTarget(); //Atan2 �� 0���γ����� ���� �����ϴ°��� ���س���
        apj.Yaw((90f+180f)-(angleRad * Mathf.Rad2Deg)); //Rad2Deg == 180 �� ���� �̸�����ص� ��� 
        //���⼭ +180 �������� ����Ƽ �𵨸��� �ݴ�εǾ��־ z��-���� �������� ������
        //���ȿ� ���� ���̳����� 2  ��׸��� �����Ҷ�� +90
       
        
        
    }

    private void Start()
    {
        VideoClip clip = Resources.Load<VideoClip>("Videos\\han_angly"); //���ø� ���
       
        
        //clip = (VideoClip)Resources.Load("Videos\\�Ѽ��� ����"); //���� ����ȯ

        //clip = Resources.Load("Videos\\�Ѽ��� ����") as VideoClip; //���������� ����ȯ 

        screen.SetVideoClip(clip);
    }
    private float CalcAngleToTarget()
    {
        Vector3 dirToTarget = targetTr.position - transform.position; //���� ��ġ - ��������ġ = Ÿ�ٹ���(dirToTarget)
        //����ȭ 
        dirToTarget.Normalize();
        //�������ϱ� ��ũź��Ʈ

       return Mathf.Atan2(dirToTarget.z, dirToTarget.x); //Atan2 �� 0���γ����� ���� �����ϴ°��� ���س���
    }
}
