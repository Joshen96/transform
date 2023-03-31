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

    //�Ÿ������� �÷��� ���
    //������ �������� �Ÿ����
    //Screen Play Distance
    
    private readonly float screnPlayDist = 20f; //10���ͱ��� 

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

        float dist = CalcDistanceWithTarget(); //�Ÿ� ��� 

        if(dist<screnPlayDist)screen.Play();
        else screen.Pause();

        DebugDistance();
    }

    private void Start()
    {
        VideoClip clip = Resources.Load<VideoClip>("Videos\\han_angly"); //���ø� ��� 
       
        
        //clip = (VideoClip)Resources.Load("Videos\\�Ѽ��� ����"); //���� ����ȯ

        //clip = Resources.Load("Videos\\�Ѽ��� ����") as VideoClip; //���������� ����ȯ 
        //��� Resources������ Videos �����ȿ� han_angly ������ 
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
    private float CalcDistanceWithTarget()
    { 
        Vector3 dirToTarget = targetTr.position - transform.position;  //��� 1
        float dist =  dirToTarget.magnitude; // magnitude �Ÿ�

        dist = Vector3.Distance(targetTr.position, transform.position); //��� 2

        return dist;

    }

    private  void DebugDistance()
    {
        Vector3 dirToTarget = 
            targetTr.position - transform.position; 

        Color color = Color.white;
        if (screnPlayDist < dirToTarget.magnitude) //�������� �ִٸ�
        {
            color = Color.yellow;
        }
        else
        {
            color = Color.red;
        }
        Debug.DrawLine(
            transform.position,targetTr.position,color);
    }
}
