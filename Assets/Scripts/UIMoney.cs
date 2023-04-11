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

    
    public void UpdatePosition(Vector3 _pos) //위치 받아와서  갱신
    {
        
        Vector3 w2c =Camera.main.WorldToScreenPoint(WorldOBJ.transform.position); //월드 포지션을 스크린포지션으로 
        rectTr.position = w2c + new Vector3(0f, 30f, 0f);
    }

    public void UpdateMoney(int _pMoney)
    {
         textMoney.text = _pMoney.ToString();
    }

    
}
