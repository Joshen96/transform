using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Dependencies.Sqlite;
using UnityEngine;
using System.Text;
using System.Runtime.InteropServices.WindowsRuntime;
//�ű�� : ��Ʈ������ ����ϱ�����

public class Player : MonoBehaviour
{
    private PlayerController playerctrl = null;

    private Electronics electronics = null; //��ȣ�ۿ��� ������ǰ 
  

    [SerializeField]
    //private Queue <Product> inventory = new Queue<Product> (); 
    //��ǰ��ũ��Ʈ�� �ִ� ���
    private Queue <VeningMachine.EVMProduct> inventory = 
         new Queue <VeningMachine.EVMProduct>();
    [SerializeField]
    private int money = 10000;
    [SerializeField]
    private UIMoney uiMoney = null;

    public int Money { get { return money;  }    
       
    }
    private void OnTriggerEnter(Collider _other)
    {
        //if (_other.gameObject.tag == "Electronics") { }

        if (_other.gameObject.CompareTag("Electronics"))
        {
            electronics = _other.GetComponent<Electronics>();
            Debug.Log("Get Electronics!");
        }

        if(_other.gameObject.CompareTag("Product"))
        {
           // GetProduct(_other.GetComponent<Product>()); //��ǰ�ֱ�
           Product product = _other.GetComponent<Product>();
            GetProduct(product.GetProductType());
            Destroy(product.gameObject);
        }


    }

    private void OnTriggerExit(Collider _other)
    {
        if (_other.gameObject.CompareTag("Electronics"))
        {
            electronics = null;

        }
    }
    private void Awake()
    {
        playerctrl = GetComponent<PlayerController>();
        playerctrl.SetMLBDelegate(OnMLBDown);
        playerctrl.SetMRBDelegate(OnMRBDown);
        playerctrl.SetUseDelegate(UseProduct);


    }
    private void Start()
    {
        uiMoney.UpdatePosition(transform.position);
        uiMoney.UpdateMoney(money);
    }

    private void Update() //���߿� �ٲ����
    {
        //uiMoney.UpdatePosition(transform.position);
        //uiMoney.UpdateMoney(money);
    }
    public void OnMLBDown() //���� �ѱ�
    {

        //Power On Off

        if (electronics)
        {
            if (electronics.GETIsPowerOn())
            {
                electronics.PowerOff();
                Debug.Log("Poweroff");
            }
            else
            {
                electronics.PowerOn();
                Debug.Log("Poweron");
            }
        }


       


        
    }

    public void OnMRBDown() 
    {

        if (electronics != null)
        {
            electronics.Use();
            Debug.Log("Use");
        }
        
    }

    public void UseProduct()
    {
        if (inventory.Count == 0) return;
        VeningMachine.EVMProduct product = inventory.Dequeue();
        
        Product.UseWithType(product,this);
        //Product.UseWithType(inventory.Dequeue(),this);


        //��ü�������Ǵ°� 

        //Product product =inventory.Dequeue(); //ť �θ������ �κ��丮���� �ϳ� ��ť�Ѱ��� ��ǰ���� ������
        //product.Use();  //������ ��ǰ�� ���


    }


    public void GetProduct(
        //Product _product
        VeningMachine.EVMProduct _product)
    {
        
        inventory.Enqueue(_product);
        StringBuilder sb = new StringBuilder(); //���ڿ� ������ ���ο� ���ڿ���ü�� �ȸ��� ����
                                                //foreach (Product product in inventory) {
                                                //sb.Append(product.name + " - ");//��������
                                                //sb.Append(product.name); //������
                                                //sb.Append(" - ");
        foreach (VeningMachine.EVMProduct product in inventory)
        {
            //sb.Append(product.name + " - ");//��������
            sb.Append(product.ToString()); //������
            sb.Append(" - ");
        }

        //sb.Append("(" + inventory.Count + ")");  //��Ʈ�� ��ü 3�� �ε� ��ġ�� 6���λ���ؼ� ���Ͻ����� ��������
        sb.Append("(");
        sb.Append(inventory.Count);
        sb.Append(")");  //������

        
        Debug.Log(sb.ToString());

        //���������� :���ڿ��� ���̴� 
        //��Ʈ��Ŭ���� 
    }
    public void Buy(int _price)
    {
        if (_price > money) return;

        money -= _price;

        if (uiMoney) uiMoney.UpdateMoney(money);
    }
}
