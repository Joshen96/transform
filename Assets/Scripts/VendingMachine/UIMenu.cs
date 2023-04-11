using System.Collections;
using System.Collections.Generic;
using TMPro.Examples;
using UnityEngine;
//using UnityEngine.UIElements;
using UnityEngine.UI;

public class UIMenu : MonoBehaviour
{

    ///��������Ʈ �����
    public delegate void OnClickMenuDelegate(int _btnNum, UI_MenuButton _menubtn);

    /// </summary>
    //��ư ��ġ 
    [SerializeField]
    private GameObject menuBtnPrefab = null;

    private List<UI_MenuButton> menuBtnList = null;

    //
    public void BuildButtons(
        List<VeningMachine.SProductInfo> _productInfoList,
        OnClickMenuDelegate _onClickCallback ,int _Pmoney) //����Ʈ�� �����ι޾���  //��ư�߰�  //��������Ʈ
    {
        
        if (_productInfoList == null || _productInfoList.Count == 0) return;

        if (menuBtnList != null && menuBtnList.Count > 0) ClearMenuButtonList();  //�Ҵ��� �����ֱ�  ����߰� �Ǵ°� ����
        menuBtnList = new List<UI_MenuButton>();

        for(int i=0; i<_productInfoList.Count; i++) 
        {
            GameObject go = Instantiate(menuBtnPrefab); //��ư �Ѱ��� ����
            //go.transform.SetParent(transform);
            //go.transform.localPosition = Vector3.zero;
            RectTransform rectTf = go.GetComponent<RectTransform>(); // ����ư�� rect����
            rectTf.SetParent(GetComponent<RectTransform>());//
            rectTf.localPosition = CalcLocalPositionWithIndex(i,_productInfoList.Count);//�θ��� ��ġ
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
            //��������Ʈ
            Button button = go.GetComponent<Button>(); //��ư �޾ƿ��� 

           
            button.onClick.AddListener(
                //���ٽ� == ���⿡ �ٷ��Լ������ ������ : ���°���� �ٷ� ��ȣ �����Ϸ���
                () =>
                {
                    Debug.Log(i);
                    _onClickCallback?.Invoke(i);
                }   
            );//���帮����(�׼�) ���° ��ư����
           */

            menuBtnList.Add(btn);

        }
       
    }
    private void ClearMenuButtonList() //�����
    {
        foreach(UI_MenuButton btn in menuBtnList)
        {
            Destroy(btn.gameObject);
        }
    }
    /// <summary>
    /// ��ư ��ġ ���ϴ� �Լ�
    /// </summary>
    /// <param name="_idx">���� �ε���</param>
    /// <param name="_totalCnt">��ư ��ü ����</param>
    /// <returns></returns>
    /// ����
    /// ���°������, ��ü���� 
    /// ���� ���ϱ� 
    /// ���׶��� ���� ��� 
    /// 1���̻��ϰ�� 
    /// ���������� ��� 2���ϰ�� ��׶��带 3����ϱ� == ��׶��尡 ����Ǿ 3��еǱ⿡ ���� 
    /// 3���ϰ��  4����ϰ� 
    /// ������ġ offsetw 
    /// �ϳ��� �Լ��� ¦���϶� Ȧ���϶�  Ȧ���� offset ��ŭ ���ϰ� ���Ⱑ�� ¦���� �Ұ��� 

    private Vector3 CalcLocalPositionWithIndex(int _idx, int _totalCnt) //������¹�ư //��ü ����
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
        

        //�۾���
        /*
        const int COL_MAX = 3; // �� 3���̻� �������� ���3

        Vector3 pos = Vector3.zero;
        //��׶��� ���̾˾ƿ;���

        Transform bgTr = transform.GetChild(0);//ù��° �ڽ� == �� ��׶���

        RectTransform bgRtTr = bgTr.GetComponent<RectTransform>();//�� ������Ʈ�� ��Ʈ Ʈ������ ������

        Vector2 bgSize = bgRtTr.sizeDelta;// ��׶����� ��ƮƮ���������ִ� ������ ��Ÿ�� bgSize�� ����2������ �־���
        //��׶����� x���� y���� ���ϱ�
        float bgWidth = bgSize.x;
        float bgHeight = bgSize.y;

        //������ ���̱��ϱ� ��׶��� ���� / ����+1 
        Vector2 offset = Vector2.zero;
        offset.x= bgWidth / (_totalCnt) + 1;


        //�������� ���ϱ�
        Vector2 startpos = -offset / (_totalCnt % 2 == 0 ? 2f : 1f);

        if (_totalCnt> 1) pos.x = startpos.x + ((_idx % COL_MAX) * offset.x);

        return pos;
        */


    }
    /*
     public void UpdateButtonInfo(
         int _btnNum,
         VeningMachine.SProductInfo _productInfo)  //����ӽ��� ����
    {
        menuBtnList[_btnNum].UpdateInfo( _productInfo );
    }
    */
}
