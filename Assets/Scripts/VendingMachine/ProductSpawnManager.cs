using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProductSpawnManager
{
    private static List<GameObject> productPrefabList = null; //������ ����Ʈ

    private static void LoadPrefabs()
    {
        if (productPrefabList != null) return;

        
       GameObject[] prefabs =
            Resources.LoadAll<GameObject>("Prefabs\\Products"); //LoadAll �������ִ� ���� ����


        productPrefabList = new List<GameObject>(prefabs); //�ڵ����� �迭�� ������ ����Ʈ�� �־��ش�


    }


     public static GameObject GetPrefab(VeningMachine.EVMProduct _productType
         ) //����Ÿ�԰� ������ġ
    {
        if (productPrefabList == null)
            LoadPrefabs();

        //switch (_productType)
        // {
        //    case VeningMachine.EVMProduct.Coke:
        //       productPrefabList[(int)Coke]
        //       break;
        //}

       
        foreach(GameObject prefab in productPrefabList)
        {
            Product product = prefab.GetComponent<Product>();
            if(product.GetProductType()==_productType)
            {
                return prefab;
                
            }
        }
        return null;
        
        

    }



}
