using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;
using UnityEngine.Playables;

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

     //�ui�� �Ѹ���
    [SerializeField] 
    private UIMenu uiMenu = null; //������Ʈ �ޱ����� �巡�׷� 


 
    //Template
    //����ü ����Ʈ
    [SerializeField]
    private List<SProductInfo> ProductInfoList = new List<SProductInfo>(); //����ӽſ� �ִ� ��ǰ��������Ʈ

    private Player player = null; //�÷��̾� ���� �����ϱ����ؼ���

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
                player = _other.GetComponent<Player>();//�÷��̾� ������ Ž���� �÷��̾�������Ʈ �ޱ�
                                                       //��ȣ�ۿ��� �÷��̾�
                uiMenu.gameObject.SetActive(true);
                uiMenu.BuildButtons(ProductInfoList,OnClickMenu,player.Money); //��������Ʈ
                //uiMenu.gameObject.SetActive(true); //ui Ȱ��ȭ
                
               
                
                



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

    
    public void OnClickMenu(int _btnNum
        ,
        UI_MenuButton _menubtn) //��ư��ȣ �ޱ� 
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

        /*
        Debug.Log("�ȱ���"+
           ProductInfoList[_btnNum].Product.ToString()+"("+
           ProductInfoList[_btnNum].price+")"+
           ProductInfoList[_btnNum].stock
            );
        */

        if (player == null) return;
        if (!ProductInfoList[_btnNum].CheckStock()) return; //����ִ��� �˻�

        if (ProductInfoList[_btnNum].price>player.Money)
        {
            Debug.Log("�ܾ׺���");
            return;
        }



        //�ٲ����
        //ProductInfoList[_btnNum].Sell(); //�ȱ�  
        //������� ����ȵ�
        
        //�ٲ��� 
        SProductInfo changeInfo=
            ProductInfoList[_btnNum]; //����Ȱ��ϳ������
        changeInfo.Sell(); //���ҽ�Ų��

        ProductInfoList[_btnNum] = changeInfo; //�װ��� �־���  Ʈ�������̶� ���
        player.Buy(changeInfo.price);


        
        //����Ʈ�� 
        
        
        
        Debug.Log("�ȸ���" +
           ProductInfoList[_btnNum].Product.ToString() + "(" +
           ProductInfoList[_btnNum].price + ")" +
           ProductInfoList[_btnNum].stock
            );

        //��ǰ �����
        GameObject prefab = ProductSpawnManager.GetPrefab(ProductInfoList[_btnNum].Product);

        Vector3 pos = this.gameObject.transform.position;

        // transform.rotation = Quaternion.Euler(0f, Random.Range(0,360), 0f);
        
        
        // ���� + �Ÿ� ���ϱ�
        float deg = Random.Range(0, 360);
        float range = Random.Range(5, 7);
        float rad  =Mathf.Deg2Rad * deg;
        float x = range * Mathf.Sin(rad);
        float z = range * Mathf.Cos(rad);




        //Instantiate(prefab, pos + new Vector3(x,0f,z), Quaternion.identity);

        Instantiate(prefab, GetValidSpawnPosition(), Quaternion.identity);


        //������ġ 

        //����
        //��ư ���� ���̽�������Ʈ���� ����Ʈ �����ߴ������..


        //�� ����



        //��ư ���� ����


        /*
        uiMenu.UpdateButtonInfo(_btnNum,
            changeInfo);
        */

        _menubtn.UpdateInfo(changeInfo,player.Money);
        





    }
    private Vector3 GetValidSpawnPosition()
    {

        //�����˻� �ϱ����� ������ ������ Ʈ���Ű˻��ϴ� ���� 
        //�����ϱ����� ���� �ϱ�����
        //Physics.BoxCastAll() //�ٷΰ˻� ���ݸ��� ���� ��������  all�� �ɸ��ֵ� �迭���ѹ��� ���� ��all�� �ϳ���



        const float SPAWN_DIST = 3f;            // ���� �Ÿ�
        const float PI2 = Mathf.PI * 2f;        // 360���� ��ġ�ϱ� ����
        const float POS_Y = 0.5f;               // �ٴڿ��� �˻��ϸ� ��Ȯ���� ��������� ���� ���, ���߿� 0���� ����

        Vector3 startPos = transform.position;  // ���̽�� ������ ��ġ
        startPos.y = POS_Y;
        bool isValidPos = false;                // ��ȿ�� ��ġ���� ������üũ
        float angle = 0f;                       // ���� ���� ��������
        RaycastHit hitInfo;                     // ���� ��Ʈ ���� 

        Vector3 spawnPos = Vector3.zero;        // ������ ��ġ
        while (!isValidPos)
        {
            // ���� ���� ���
            angle = Random.Range(0f, PI2);
            spawnPos = transform.position +
                new Vector3(
                    Mathf.Cos(angle) * SPAWN_DIST,
                    POS_Y,
                    Mathf.Sin(angle) * SPAWN_DIST
                );

            Vector3 dir = (spawnPos - startPos).normalized; //���1 ���Ҷ� ����ȭ
            //dir.Normalize();                              //���2 ���ϰ� ����ȭ

            // Physics.Raycast (�������� �߻��� ��ġ, �߻� ����, �浹 ���, �ִ� �Ÿ�)
            // ������ ��ġ �ĺ��� ���� ���� �浹�˻�
            if (Physics.Raycast(startPos, dir, out hitInfo, SPAWN_DIST))//(�������� �߻��� ��ġ, �߻� ����, �浹 ���, �ִ� �Ÿ�)
            {
                // �浹�� ������Ʈ�� �ִٸ� �ٸ� ��ġ�� ã�ƾ� ��
                Debug.Log("Raycast Hit: " + hitInfo.transform.name);
                continue;
            }

            isValidPos = true;
        }

        // ���̸� �ٽ� 0����
        spawnPos.y = 0f;
        return spawnPos;
    }

}
