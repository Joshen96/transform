using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;

public class VeningMachine : MonoBehaviour
{
    //3가지 일 

    //1.플레이어와 상호작용
    //2.UI 
    //3.음료 

    //구조체만들기
    public enum EVMProduct //열거형 상품 으로 만든이유 알아야함
    {
        Coke,Tissue,Bread, Letsbe,Vita500, OronmainC , Sprite,SSOrange
    }


    [System.Serializable] //직렬화 하겠다.
    public class SProductInfo
    {
        public EVMProduct Product;
        public int price;
        public int stock;
     

        public bool CheckStock() { return stock > 0; }
        public void Sell() {
            if(CheckStock())--stock; 
        }

    }

     //어떤ui에 뿌릴지
    [SerializeField] 
    private UIMenu uiMenu = null; //컴포넌트 받기위해 드래그로 


 
    //Template
    //구조체 리스트
    [SerializeField]
    private List<SProductInfo> ProductInfoList = new List<SProductInfo>(); //밴딩머신에 있는 상품정보리스트



    private void Awake()
    {
        if(uiMenu==null)
        {
            Debug.LogError("UIMenu is missing");
        }
    }

    //상품판매 생성


    //ui 상호작용
    //ui 켜기

    private void OnTriggerEnter(Collider _other)
    {
        if (_other.CompareTag("Player"))
        {
            if (uiMenu)
            {
                uiMenu.BuildButtons(ProductInfoList,OnClickMenu); //델리게이트
                uiMenu.gameObject.SetActive(true); //ui 활성화

            }
        }
    }
    private void OnTriggerExit(Collider _other)
    {
        if(_other.CompareTag("Player"))
            if(uiMenu)
            {
                uiMenu.gameObject.SetActive(false);
            }

    }
   
    public static string VMProductToName(EVMProduct _Product)
    {
        
        switch (_Product)
        {
            case EVMProduct.Coke: return "콜라";
            case EVMProduct.Tissue:       return "휴지";
            case EVMProduct.Bread:        return "빵";
            case EVMProduct.Letsbe:       return "레쓰비";
            case EVMProduct.Vita500:      return "비타500";
            case EVMProduct.OronmainC:    return "오로나민C";
            case EVMProduct.Sprite:       return "스프라이트";
            case EVMProduct.SSOrange:     return "색색오랜지";
            default: return "몰라";
;        }
    }

    
    public void OnClickMenu(int _btnNum) //버튼번호 받기 
    {
        /*
        SProductInfo selectedInfo;
        foreach(SProductInfo info in ProductInfoList) 
        {
            if (info.Product == _product)
            {
                selectedInfo = info; //하면안됨 값복사된걸 수정하면 의미없음
                break;
            }
        }
        if (selectedInfo.stock < 1) return;
            */

        Debug.Log("팔기전"+
           ProductInfoList[_btnNum].Product.ToString()+"("+
           ProductInfoList[_btnNum].price+")"+
           ProductInfoList[_btnNum].stock
            );

        if (!ProductInfoList[_btnNum].CheckStock()) return; //재고있는지 검사
        ProductInfoList[_btnNum].Sell(); //팔기
        Debug.Log("팔린후" +
           ProductInfoList[_btnNum].Product.ToString() + "(" +
           ProductInfoList[_btnNum].price + ")" +
           ProductInfoList[_btnNum].stock
            );
        //상품 만들기
        //보유
        //버튼 갱신 파이썬프로젝트때도 리스트 갱신했던기억이..
        //버튼 정보 갱신





    }

}
