using System.Collections;
using System.Collections.Generic;
using TMPro.Examples;
using UnityEngine;
//using UnityEngine.UIElements;
using UnityEngine.UI;

public class UIMenu : MonoBehaviour
{

    ///델리게이트 만들기
    public delegate void OnClickMenuDelegate(int _btnNum, UI_MenuButton _menubtn);

    /// </summary>
    //버튼 배치 
    [SerializeField]
    private GameObject menuBtnPrefab = null;

    private List<UI_MenuButton> menuBtnList = null;

    //
    public void BuildButtons(
        List<VeningMachine.SProductInfo> _productInfoList,
        OnClickMenuDelegate _onClickCallback ,int _Pmoney) //리스트는 참조로받아짐  //버튼추가  //델리게이트
    {
        
        if (_productInfoList == null || _productInfoList.Count == 0) return;

        if (menuBtnList != null && menuBtnList.Count > 0) ClearMenuButtonList();  //할당전 지워주기  계속추가 되는것 방지
        menuBtnList = new List<UI_MenuButton>();

        for(int i=0; i<_productInfoList.Count; i++) 
        {
            GameObject go = Instantiate(menuBtnPrefab); //버튼 한개씩 생성
            //go.transform.SetParent(transform);
            //go.transform.localPosition = Vector3.zero;
            RectTransform rectTf = go.GetComponent<RectTransform>(); // 각버튼의 rect정보
            rectTf.SetParent(GetComponent<RectTransform>());//
            rectTf.localPosition = CalcLocalPositionWithIndex(i,_productInfoList.Count);//부모의 위치
            /*
            if (_productInfoList.Count == 3) 
            {
                if (i == 0)
                {
                    rectTf.localPosition = Vector3.left * 163;
                }
                if (i == 1)
                {
                    rectTf.localPosition = Vector3.zero;
                }
                if (i == 2)
                {
                    rectTf.localPosition = Vector3.right * 163;
                }

            }
            if (_productInfoList.Count == 2)
            {
                if (i == 0)
                {
                    rectTf.localPosition = Vector3.left*80;
                }
                if (i == 1)
                {
                    rectTf.localPosition = Vector3.right*80;
                }
               

            }
            */





            UI_MenuButton btn = go.GetComponent<UI_MenuButton>();
            /*
             btn.InitInfos(
                 _productInfoList[i].Product.ToString(), 
                 _productInfoList[i].price,
                 _productInfoList[i].stock
                 );
            */
            btn.InitInfos(_productInfoList[i],
                i,
                _onClickCallback,_Pmoney);


            /*
            //델리게이트
            Button button = go.GetComponent<Button>(); //버튼 받아오기 

           
            button.onClick.AddListener(
                //람다식 == 여기에 바로함수만들기 쓴이유 : 몇번째인지 바로 번호 지정하려고
                () =>
                {
                    Debug.Log(i);
                    _onClickCallback?.Invoke(i);
                }   
            );//에드리스너(액션) 몇번째 버튼인지
           */

            menuBtnList.Add(btn);

        }
       
    }
    private void ClearMenuButtonList() //지우기
    {
        foreach(UI_MenuButton btn in menuBtnList)
        {
            Destroy(btn.gameObject);
        }
    }
    /// <summary>
    /// 버튼 위치 구하는 함수
    /// </summary>
    /// <param name="_idx">현재 인덱스</param>
    /// <param name="_totalCnt">버튼 전체 갯수</param>
    /// <returns></returns>
    /// 설명
    /// 몇번째인지와, 전체갯수 
    /// 간격 구하기 
    /// 빽그라운드 길이 재기 
    /// 1개이상일경우 
    /// 비율에맞춰 계산 2개일경우 백그라운드를 3등분하기 == 백그라운드가 변경되어도 3등분되기에 ㄱㅊ 
    /// 3개일경우  4등분하고 
    /// 시작위치 offsetw 
    /// 하나의 함수로 짝수일때 홀수일때  홀수면 offset 만큼 더하고 빼기가능 짝수면 불가능 

    private Vector3 CalcLocalPositionWithIndex(int _idx, int _totalCnt) //현재들어온버튼 //전체 갯수
    {
        
        if (_idx < 0 || _totalCnt < 1) return Vector3.zero;

        const int COL_MAX = 3;

        Vector2 bgSize = transform.GetChild(0).GetComponent<RectTransform>().sizeDelta;
        Vector2 btnSize = menuBtnPrefab.GetComponent<RectTransform>().sizeDelta;

        int colCnt = Mathf.Clamp(_totalCnt, 1, COL_MAX);
        float btnTotalW = colCnt * btnSize.x;
        float totalOffsetW = bgSize.x - btnTotalW;

        int rowCnt = (int)Mathf.Ceil((float)_totalCnt / colCnt);
        float btnTotalH = rowCnt * btnSize.y;
        float totalOffsetH = bgSize.y - btnTotalH;

        Vector2 offset = Vector2.zero;
        offset.x = totalOffsetW / (float)(colCnt + 1);
        offset.y = totalOffsetH / (float)(rowCnt + 1);

        Vector2 btnDist = offset + btnSize;
        Vector2 startPos = new Vector2(
            -btnDist.x / (colCnt % 2 == 0 ? 2f : 1f),
            btnDist.y / (rowCnt % 2 == 0 ? 2f : 1f)
            );

        Vector3 pos = Vector3.zero;
        if (colCnt > 1) pos.x = startPos.x + ((_idx % COL_MAX) * btnDist.x);
        if (rowCnt > 1) pos.y = startPos.y - ((_idx / COL_MAX) * btnDist.y);

        return pos;
        

        //작업중
        /*
        const int COL_MAX = 3; // 열 3개이상 못들어오게 상수3

        Vector3 pos = Vector3.zero;
        //백그라운드 길이알아와야함

        Transform bgTr = transform.GetChild(0);//첫번째 자식 == 즉 백그라운드

        RectTransform bgRtTr = bgTr.GetComponent<RectTransform>();//갯 컴포넌트로 랙트 트랜스폼 가져옴

        Vector2 bgSize = bgRtTr.sizeDelta;// 백그라운드의 랙트트랜스폼에있는 사이즈 델타를 bgSize의 벡터2값으로 넣어줌
        //백그라운드의 x길이 y길이 구하기
        float bgWidth = bgSize.x;
        float bgHeight = bgSize.y;

        //오프셋 길이구하기 백그라운드 길이 / 갯수+1 
        Vector2 offset = Vector2.zero;
        offset.x= bgWidth / (_totalCnt) + 1;


        //시작지점 구하기
        Vector2 startpos = -offset / (_totalCnt % 2 == 0 ? 2f : 1f);

        if (_totalCnt> 1) pos.x = startpos.x + ((_idx % COL_MAX) * offset.x);

        return pos;
        */


    }
    /*
     public void UpdateButtonInfo(
         int _btnNum,
         VeningMachine.SProductInfo _productInfo)  //밴딩머신의 정보
    {
        menuBtnList[_btnNum].UpdateInfo( _productInfo );
    }
    */
}
