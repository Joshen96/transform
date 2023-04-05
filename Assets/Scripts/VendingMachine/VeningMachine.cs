using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;

public class VeningMachine : MonoBehaviour
{
    //3���� �� 

    //1.�÷��̾�� ��ȣ�ۿ�
    //2.UI 
    //3.���� 

    //����ü�����
    public enum EVMProduct //������ ��ǰ ���� �������� �˾ƾ���
    {
        Coke,Tissue,Bread, Letsbe,Vita500, OronmainC , Sprite,SSOrange
    }


    [System.Serializable] //����ȭ �ϰڴ�.
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

     //�ui�� �Ѹ���
    [SerializeField] 
    private UIMenu uiMenu = null; //������Ʈ �ޱ����� �巡�׷� 


 
    //Template
    //����ü ����Ʈ
    [SerializeField]
    private List<SProductInfo> ProductInfoList = new List<SProductInfo>(); //����ӽſ� �ִ� ��ǰ��������Ʈ



    private void Awake()
    {
        if(uiMenu==null)
        {
            Debug.LogError("UIMenu is missing");
        }
    }

    //��ǰ�Ǹ� ����


    //ui ��ȣ�ۿ�
    //ui �ѱ�

    private void OnTriggerEnter(Collider _other)
    {
        if (_other.CompareTag("Player"))
        {
            if (uiMenu)
            {
                uiMenu.BuildButtons(ProductInfoList,OnClickMenu); //��������Ʈ
                uiMenu.gameObject.SetActive(true); //ui Ȱ��ȭ

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
            case EVMProduct.Coke: return "�ݶ�";
            case EVMProduct.Tissue:       return "����";
            case EVMProduct.Bread:        return "��";
            case EVMProduct.Letsbe:       return "������";
            case EVMProduct.Vita500:      return "��Ÿ500";
            case EVMProduct.OronmainC:    return "���γ���C";
            case EVMProduct.Sprite:       return "��������Ʈ";
            case EVMProduct.SSOrange:     return "����������";
            default: return "����";
;        }
    }

    
    public void OnClickMenu(int _btnNum) //��ư��ȣ �ޱ� 
    {
        /*
        SProductInfo selectedInfo;
        foreach(SProductInfo info in ProductInfoList) 
        {
            if (info.Product == _product)
            {
                selectedInfo = info; //�ϸ�ȵ� ������Ȱ� �����ϸ� �ǹ̾���
                break;
            }
        }
        if (selectedInfo.stock < 1) return;
            */

        Debug.Log("�ȱ���"+
           ProductInfoList[_btnNum].Product.ToString()+"("+
           ProductInfoList[_btnNum].price+")"+
           ProductInfoList[_btnNum].stock
            );

        if (!ProductInfoList[_btnNum].CheckStock()) return; //����ִ��� �˻�
        ProductInfoList[_btnNum].Sell(); //�ȱ�
        Debug.Log("�ȸ���" +
           ProductInfoList[_btnNum].Product.ToString() + "(" +
           ProductInfoList[_btnNum].price + ")" +
           ProductInfoList[_btnNum].stock
            );
        //��ǰ �����
        //����
        //��ư ���� ���̽�������Ʈ���� ����Ʈ �����ߴ������..
        //��ư ���� ����





    }

}
