using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.TextCore.Text;
using UnityEngine.UI;
using UnityEditor;
using static VeningMachine;
using Unity.VisualScripting;

public class UI_MenuButton : MonoBehaviour
{

    public enum EMBInfo { Name, Price, Stock }

    private TextMeshProUGUI[] texts = null;


    private Button btn = null;


    private void Awake()
    {
        texts =
            GetComponentsInChildren<TextMeshProUGUI>();
        //foreach (TextMeshProUGUI text in texts)
        //    Debug.Log(text.name);
        btn = GetComponent<Button>();

    }
    /*
    private void Start()
    {
        InitInfos("�ݶ�", 600, 3);
    }
    */
    // Initialization
    public void InitInfos(string _name, int _price, int _stock,
        int _num,
        UIMenu.OnClickMenuDelegate _onClickCallback)
    {
        texts[(int)EMBInfo.Name].text = _name;
        texts[(int)EMBInfo.Price].text =
            _price.ToString();
        texts[(int)EMBInfo.Stock].text =
            _stock.ToString();
        /*
        btn.onClick.AddListener(() =>
        {
            _onClickCallback?.Invoke(_num);
        });
           */ 
    }
    public void InitInfos(VeningMachine.SProductInfo _productInfo,
        int _num,
        UIMenu.OnClickMenuDelegate _onClickCallback , int _Pmoney) //�����ε�
    {
        InitInfos(
            VeningMachine.VMProductToName(_productInfo.Product),
            _productInfo.price,
            _productInfo.stock,
            _num,
            _onClickCallback
            );
        btn.onClick.AddListener(() =>
        {
            Debug.Log("�����"+_num);
            _onClickCallback?.Invoke(_num,this); //������ ���� ��ư������ this
  
        });
        //btn.interactable = _productInfo.stock > 0; //���ְ�
        btn.interactable = _productInfo.stock > 0 && _productInfo.price < _Pmoney;


    }
    /*
    public void UpdateInfo(
        VeningMachine.SProductInfo _productInfo)
    {

        texts[(int)EMBInfo.Name].text = VeningMachine.VMProductToName(_productInfo.Product);
        texts[(int)EMBInfo.Price].text = _productInfo.price.ToString();
        texts[(int)EMBInfo.Stock].text = _productInfo.stock.ToString();

        btn.interactable = _productInfo.stock > 0; //������Ʈ��  ��� 0�̸� Ȱ��ȭ x
 
    }
    */

    public int GetPlayerMoney(int _Pmoney)
    {
        return _Pmoney;
    }
    public void UpdateInfo(
        VeningMachine.SProductInfo _productInfo,int _Pmoney)
    {
        
        texts[(int)EMBInfo.Name].text = VeningMachine.VMProductToName( _productInfo.Product);
        texts[(int)EMBInfo.Price].text = _productInfo.price.ToString();
        texts[(int)EMBInfo.Stock].text = _productInfo.stock.ToString();

        btn.interactable = _productInfo.stock > 0 && _productInfo.price < _Pmoney; //������Ʈ��  ��� 0�̸� Ȱ��ȭ x
        //btn.interactable = _productInfo.price < _Pmoney;

        Debug.Log(_Pmoney);
        


    }



}
