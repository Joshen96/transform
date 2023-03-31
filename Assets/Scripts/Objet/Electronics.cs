using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public abstract class Electronics : Objet  //추상클래스로 작성 
{
    [SerializeField]
    private int price = 0;
    //플러그가 꼽힌 여부
    private bool isPluged = true;
    private bool isPowerOn = false;


    public int Price { get { return price; } }
    public bool GETIsPowerOn(){return isPowerOn;}  //인라인 함수  C++ 배울적 기억이 난다

    public virtual void Awake()
    {
        type = ETYPE.Pluged;

    }

    public void PowerOn()//부모에있는것 접근하기 1.프로퍼티를 만드는방법(함수로만들어서) 2. 부모 접근지정자 프로텍티드로 바꾸는법 
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

    public abstract void Use();  //전자제품은 인스턴트화하기 힘들어서 추상함수 사용
    

}
