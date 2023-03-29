using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnchorPivot : MonoBehaviour
{
    //ȸ�� ���
    private Transform tr = null;

    private void Awake()
    {
        tr = transform; //�ٷ� ���ٰ��� �ѵ� �̷������� �ϴ� ���� : �� transform������ �˰� �����ϸ�� 
        
    }
    //yawȸ�� 
    public void Yaw(float _angleDeg) //������ ������Ÿ�Դ�������ָ����� ��׸�
    {
        tr.rotation = Quaternion.Euler(0f, _angleDeg, 0f); // ���Ϸ� �ޱ� ���
    }
}
