using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIMoney : MonoBehaviour
{
    private RectTransform rectTr = null;
    private TextMeshProUGUI textMoney = null;


    public GameObject WorldOBJ;
    private void Awake()
    {
        rectTr = GetComponent<RectTransform>();
        textMoney = GetComponentInChildren<TextMeshProUGUI>();

    }

    
    public void UpdatePosition(Vector3 _pos) //��ġ �޾ƿͼ�  ����
    {
        
        Vector3 w2c =Camera.main.WorldToScreenPoint(WorldOBJ.transform.position); //���� �������� ��ũ������������ 
        rectTr.position = w2c + new Vector3(0f, 30f, 0f);
    }

    public void UpdateMoney(int _pMoney)
    {
         textMoney.text = _pMoney.ToString();
    }

    
}
