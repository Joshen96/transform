using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;


//Z-Fighting 현상
//Billboard = 관리
//Pivot = 회전
//Screen = 영상출력 
//으로 제작할예정
public class Billboard : MonoBehaviour
{
    [SerializeField]
    private Transform targetTr = null; //타겟 받아야함

    
    private AnchorPivot apj = null;
    private Screen screen = null;

    //거리에따라 플레이 기능
    //오리와 빌보드의 거리계산
    //Screen Play Distance
    
    private readonly float screnPlayDist = 20f; //10미터기준 

    private void Awake()
    {
        // GetComponentInChildren
        //GetComponentsInChildren
        //transform.GetChild

        //GameObject.Find  == 찾는 비용이 비쌈 
        apj = GetComponentInChildren<AnchorPivot>(); //이런식으로 찾음 빌보드안에 자식으로 있는 스크립트
        screen = GetComponentInChildren<Screen>();

        //회전은 앵커피벗이하고 얼마나 돌아야하는지 계산은 빌보드가 한다.

    }

    private void Update()
    {
        //빌보드에서 계산하고 피벗에 각도값만 보내주는 형식

        float angleRad = CalcAngleToTarget(); //Atan2 는 0으로나누는 것을 방지하는것을 위해나옴
        apj.Yaw((90f+180f)-(angleRad * Mathf.Rad2Deg)); //Rad2Deg == 180 ÷ 파이 미리계산해둔 상수 
                                                        //여기서 +180 해준이유 유니티 모델링이 반대로되어있어서 z축-쪽을 정면으로 세워둠
                                                        //라디안에 보정 파이나누기 2  디그리에 보정할라면 +90

        float dist = CalcDistanceWithTarget(); //거리 계산 

        if(dist<screnPlayDist)screen.Play();
        else screen.Pause();

        DebugDistance();
    }

    private void Start()
    {
        VideoClip clip = Resources.Load<VideoClip>("Videos\\han_angly"); //템플릿 방식 
       
        
        //clip = (VideoClip)Resources.Load("Videos\\한석원 폭주"); //강제 형변환

        //clip = Resources.Load("Videos\\한석원 폭주") as VideoClip; //안정적으로 형변환 
        //경로 Resources폴더에 Videos 폴더안에 han_angly 로접근 
        screen.SetVideoClip(clip);
    }
    private float CalcAngleToTarget()
    {
        Vector3 dirToTarget = targetTr.position - transform.position; //오리 위치 - 빌보드위치 = 타겟방향(dirToTarget)
        //정규화 
        dirToTarget.Normalize();
        //각도구하기 아크탄젠트

       return Mathf.Atan2(dirToTarget.z, dirToTarget.x); //Atan2 는 0으로나누는 것을 방지하는것을 위해나옴
    }
    private float CalcDistanceWithTarget()
    { 
        Vector3 dirToTarget = targetTr.position - transform.position;  //방법 1
        float dist =  dirToTarget.magnitude; // magnitude 거리

        dist = Vector3.Distance(targetTr.position, transform.position); //방법 2

        return dist;

    }

    private  void DebugDistance()
    {
        Vector3 dirToTarget = 
            targetTr.position - transform.position; 

        Color color = Color.white;
        if (screnPlayDist < dirToTarget.magnitude) //감지보다 멀다면
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
