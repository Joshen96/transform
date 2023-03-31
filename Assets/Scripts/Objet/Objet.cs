using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Objet : MonoBehaviour
{

    //전기 타입 논타입
    public enum ETYPE { Pluged, Unpluged }
    [SerializeField]
    protected string productName = "Unknown";
    [SerializeField]
    protected ETYPE type = ETYPE.Unpluged;


}
