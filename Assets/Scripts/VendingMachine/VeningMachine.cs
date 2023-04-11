using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;
using UnityEngine.Playables;

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
    public struct SProductInfo
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

    private Player player = null; //플레이어 정보 저장하기위해선언

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
                player = _other.GetComponent<Player>();//플레이어 변수에 탐지된 플레이어컴포넌트 받기
                                                       //상호작용할 플레이어
                uiMenu.gameObject.SetActive(true);
                uiMenu.BuildButtons(ProductInfoList,OnClickMenu,player.Money); //델리게이트
                //uiMenu.gameObject.SetActive(true); //ui 활성화
                
               
                
                



            }
        }
    }
    private void OnTriggerExit(Collider _other)
    {
        if(_other.CompareTag("Player"))
            if(uiMenu)
            {
                uiMenu.gameObject.SetActive(false);
                player = null;
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

    
    public void OnClickMenu(int _btnNum
        ,
        UI_MenuButton _menubtn) //버튼번호 받기 
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

        /*
        Debug.Log("팔기전"+
           ProductInfoList[_btnNum].Product.ToString()+"("+
           ProductInfoList[_btnNum].price+")"+
           ProductInfoList[_btnNum].stock
            );
        */

        if (player == null) return;
        if (!ProductInfoList[_btnNum].CheckStock()) return; //재고있는지 검사

        if (ProductInfoList[_btnNum].price>player.Money)
        {
            Debug.Log("잔액부족");
            return;
        }



        //바뀌기전
        //ProductInfoList[_btnNum].Sell(); //팔기  
        //값복사라 적용안됨
        
        //바뀐후 
        SProductInfo changeInfo=
            ProductInfoList[_btnNum]; //복사된거하나만들고
        changeInfo.Sell(); //감소시킨후

        ProductInfoList[_btnNum] = changeInfo; //그곳에 넣어줌  트랜스폼이랑 비슷
        player.Buy(changeInfo.price);


        
        //리스트는 
        
        
        
        Debug.Log("팔린후" +
           ProductInfoList[_btnNum].Product.ToString() + "(" +
           ProductInfoList[_btnNum].price + ")" +
           ProductInfoList[_btnNum].stock
            );

        //상품 만들기
        GameObject prefab = ProductSpawnManager.GetPrefab(ProductInfoList[_btnNum].Product);

        Vector3 pos = this.gameObject.transform.position;

        // transform.rotation = Quaternion.Euler(0f, Random.Range(0,360), 0f);
        
        
        // 각도 + 거리 구하기
        float deg = Random.Range(0, 360);
        float range = Random.Range(5, 7);
        float rad  =Mathf.Deg2Rad * deg;
        float x = range * Mathf.Sin(rad);
        float z = range * Mathf.Cos(rad);




        //Instantiate(prefab, pos + new Vector3(x,0f,z), Quaternion.identity);

        Instantiate(prefab, GetValidSpawnPosition(), Quaternion.identity);


        //스폰위치 

        //보유
        //버튼 갱신 파이썬프로젝트때도 리스트 갱신했던기억이..


        //돈 차감



        //버튼 정보 갱신


        /*
        uiMenu.UpdateButtonInfo(_btnNum,
            changeInfo);
        */

        _menubtn.UpdateInfo(changeInfo,player.Money);
        





    }
    private Vector3 GetValidSpawnPosition()
    {

        //물리검사 하기위함 원래는 생성후 트리거검사하는 문제 
        //생성하기전에 감지 하기위해
        //Physics.BoxCastAll() //바로검사 온콜리더 보다 빠르게함  all은 걸린애들 배열로한번에 받음 논all은 하나만



        const float SPAWN_DIST = 3f;            // 생성 거리
        const float PI2 = Mathf.PI * 2f;        // 360도로 배치하기 위해
        const float POS_Y = 0.5f;               // 바닥에서 검사하면 정확도가 떨어질까봐 위로 띄움, 나중에 0으로 복구

        Vector3 startPos = transform.position;  // 레이쏘기 시작할 위치
        startPos.y = POS_Y;
        bool isValidPos = false;                // 유효한 위치인지 벽감지체크
        float angle = 0f;                       // 랜덤 각도 임의저장
        RaycastHit hitInfo;                     // 레이 히트 정보 

        Vector3 spawnPos = Vector3.zero;        // 생성할 위치
        while (!isValidPos)
        {
            // 랜덤 각도 얻기
            angle = Random.Range(0f, PI2);
            spawnPos = transform.position +
                new Vector3(
                    Mathf.Cos(angle) * SPAWN_DIST,
                    POS_Y,
                    Mathf.Sin(angle) * SPAWN_DIST
                );

            Vector3 dir = (spawnPos - startPos).normalized; //방법1 구할때 정규화
            //dir.Normalize();                              //방법2 구하고 정규화

            // Physics.Raycast (레이저를 발사할 위치, 발사 방향, 충돌 결과, 최대 거리)
            // 생성할 위치 후보를 향해 레이 충돌검사
            if (Physics.Raycast(startPos, dir, out hitInfo, SPAWN_DIST))//(레이저를 발사할 위치, 발사 방향, 충돌 결과, 최대 거리)
            {
                // 충돌된 오브젝트가 있다면 다른 위치를 찾아야 됨
                Debug.Log("Raycast Hit: " + hitInfo.transform.name);
                continue;
            }

            isValidPos = true;
        }

        // 높이를 다시 0으로
        spawnPos.y = 0f;
        return spawnPos;
    }

}
