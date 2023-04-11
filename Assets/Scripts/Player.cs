using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Dependencies.Sqlite;
using UnityEngine;
using System.Text;
using System.Runtime.InteropServices.WindowsRuntime;
//신기술 : 스트링빌더 사용하기위함

public class Player : MonoBehaviour
{
    private PlayerController playerctrl = null;

    private Electronics electronics = null; //상호작용할 전자제품 
  

    [SerializeField]
    //private Queue <Product> inventory = new Queue<Product> (); 
    //상품스크립트를 넣는 방식
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
           // GetProduct(_other.GetComponent<Product>()); //상품넣기
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

    private void Update() //나중에 바꿔야함
    {
        //uiMoney.UpdatePosition(transform.position);
        //uiMoney.UpdateMoney(money);
    }
    public void OnMLBDown() //전원 켜기
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


        //객체없어도실행되는것 

        //Product product =inventory.Dequeue(); //큐 로만들어진 인벤토리에서 하나 디큐한것을 상품으로 꺼내고
        //product.Use();  //꺼내진 상품을 사용


    }


    public void GetProduct(
        //Product _product
        VeningMachine.EVMProduct _product)
    {
        
        inventory.Enqueue(_product);
        StringBuilder sb = new StringBuilder(); //문자열 빌더는 새로운 문자열객체를 안만들어서 좋음
                                                //foreach (Product product in inventory) {
                                                //sb.Append(product.name + " - ");//안좋은예
                                                //sb.Append(product.name); //좋은예
                                                //sb.Append(" - ");
        foreach (VeningMachine.EVMProduct product in inventory)
        {
            //sb.Append(product.name + " - ");//안좋은예
            sb.Append(product.ToString()); //좋은예
            sb.Append(" - ");
        }

        //sb.Append("(" + inventory.Count + ")");  //스트링 객체 3개 인데 합치면 6개로사용해서 부하심해짐 안좋은예
        sb.Append("(");
        sb.Append(inventory.Count);
        sb.Append(")");  //좋은예

        
        Debug.Log(sb.ToString());

        //안좋은이유 :문자열이 무겁다 
        //스트링클래스 
    }
    public void Buy(int _price)
    {
        if (_price > money) return;

        money -= _price;

        if (uiMoney) uiMoney.UpdateMoney(money);
    }
}
