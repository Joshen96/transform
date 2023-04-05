using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.TextCore.Text;
using UnityEngine.UI;
using UnityEditor;

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
        InitInfos("콜라", 600, 3);
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
    public void InitInfos(VeningMachine.SProductInfo _productinfo,
        int _num,
        UIMenu.OnClickMenuDelegate _onClickCallback) //오버로딩
    {
        InitInfos(
            VeningMachine.VMProductToName(_productinfo.Product),
            _productinfo.price,
            _productinfo.stock,
            _num,
            _onClickCallback
            );
        btn.onClick.AddListener(() =>
        {
            Debug.Log("여기요"+_num);
            _onClickCallback?.Invoke(_num);
            InitInfos(
            VeningMachine.VMProductToName(_productinfo.Product),
            _productinfo.price,
            _productinfo.stock,
            _num,
            _onClickCallback);


        });

    }



}
