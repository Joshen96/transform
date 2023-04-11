using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Product : MonoBehaviour //프로덕트 추상클래스로 선언   정의 불가
{
    [SerializeField]
    private VeningMachine.EVMProduct productType;


    public VeningMachine.EVMProduct GetProductType()
    {
        return productType;
    }
    public static void UseWithType(VeningMachine.EVMProduct _productType, Player player)
    {
        switch (_productType)
        {
            case VeningMachine.EVMProduct.Coke:
                Debug.Log("Drink - Coke");
                break;
            case VeningMachine.EVMProduct.Tissue:
                Debug.Log("Use - Tissue");
                break;
            case VeningMachine.EVMProduct.Bread:
                Debug.Log("Eat - Bread");
                break;
            case VeningMachine.EVMProduct.Letsbe:
                Debug.Log("Drink - Letsbe");
                break;
            case VeningMachine.EVMProduct.Vita500:
                Debug.Log("Drink - Vita500");
                break;
            case VeningMachine.EVMProduct.OronmainC:
                Debug.Log("Drink - OronmainC");
                break;
            case VeningMachine.EVMProduct.Sprite:
                Debug.Log("Drink - Sprite");
                break;
            case VeningMachine.EVMProduct.SSOrange:
                Debug.Log("Drink - SSOrange");
                break;
        }
    }
    public abstract void Use();
        
}
