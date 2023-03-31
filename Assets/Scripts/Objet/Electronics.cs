using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public abstract class Electronics : Objet  //�߻�Ŭ������ �ۼ� 
{
    [SerializeField]
    private int price = 0;
    //�÷��װ� ���� ����
    private bool isPluged = true;
    private bool isPowerOn = false;


    public int Price { get { return price; } }
    public bool GETIsPowerOn(){return isPowerOn;}  //�ζ��� �Լ�  C++ ����� ����� ����

    public virtual void Awake()
    {
        type = ETYPE.Pluged;

    }

    public void PowerOn()//�θ��ִ°� �����ϱ� 1.������Ƽ�� ����¹��(�Լ��θ���) 2. �θ� ���������� ������Ƽ��� �ٲٴ¹� 
    {
        if (isPluged)
        {
            isPowerOn = true;
            Debug.Log(productName + " Power On");
        }
    }
    public void PowerOff() 
    {
        if (isPowerOn)
        {
            isPowerOn = false;
            Debug.Log(productName + " Power Off");
        }

    }

    public abstract void Use();  //������ǰ�� �ν���Ʈȭ�ϱ� ���� �߻��Լ� ���
    

}
