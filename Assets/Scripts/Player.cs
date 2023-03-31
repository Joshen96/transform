using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Dependencies.Sqlite;
using UnityEngine;

public class Player : MonoBehaviour
{
    private PlayerController playerctrl = null;

    private Electronics electronics = null; //상호작용할 전자제품 

    private void OnTriggerEnter(Collider _other)
    {
        //if (_other.gameObject.tag == "Electronics") { }

        if (_other.gameObject.CompareTag("Electronics"))
        {
            electronics = _other.GetComponent<Electronics>();
            Debug.Log("Get Electronics!");
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
}
