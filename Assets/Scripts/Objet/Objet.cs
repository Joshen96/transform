using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Objet : MonoBehaviour
{

    //���� Ÿ�� ��Ÿ��
    public enum ETYPE { Pluged, Unpluged }
    [SerializeField]
    protected string productName = "Unknown";
    [SerializeField]
    protected ETYPE type = ETYPE.Unpluged;


}
