using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProductSpawnManager
{
    private static List<GameObject> productPrefabList = null; //프리펩 리스트

    private static void LoadPrefabs()
    {
        if (productPrefabList != null) return;

        
       GameObject[] prefabs =
            Resources.LoadAll<GameObject>("Prefabs\\Products"); //LoadAll 폴더에있는 파일 전부


        productPrefabList = new List<GameObject>(prefabs); //자동으로 배열의 내용을 리스트로 넣어준다


    }


     public static GameObject GetPrefab(VeningMachine.EVMProduct _productType
         ) //스폰타입과 스폰위치
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
