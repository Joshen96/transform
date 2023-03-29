using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnchorPivot : MonoBehaviour
{
    //회전 기능
    private Transform tr = null;

    private void Awake()
    {
        tr = transform; //바로 접근가능 한데 이런식으로 하는 이유 : 내 transform정보만 알고 관리하면됨 
        
    }
    //yaw회전 
    public void Yaw(float _angleDeg) //각도가 들어오는타입대로적어주면좋음 디그리
    {
        tr.rotation = Quaternion.Euler(0f, _angleDeg, 0f); // 오일러 앵글 사용
    }
}
