using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

// 상속(Inheritance)
public class PlayerController : MonoBehaviour
{
    //델리게이트 : 대리자 대신해주는

    public delegate void MLBDelegate(); //마우스 왼쪽 버튼  자료형임
    public delegate void MRBDelegate(); //오른쪽 

    private MLBDelegate mlbCallback = null; //변수생성
    private MRBDelegate mrbCallback = null; //변수생성

    public delegate void VoidVoidDelegate();
    public delegate bool VoidBoolDelegate(int _val);
    private VoidVoidDelegate useCallBack = null;



    // Class Member Variables
    private Transform tr = null;
    private Rigidbody rb = null;

    // Attribute
    [SerializeField][Range(10f, 50f)]
    private float moveSpeed = 10f;
    [SerializeField, Range(50f, 100f)]
    private float rotSpeed = 50f;
    [SerializeField]
    private UIMoney uiMoney = null;
    private void Awake()
    {
        tr = GetComponent<Transform>();
        //tr = transform;
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.W))
        {
            //tr.position.z = 10f;
            //Vector3 newPos = transform.position;
            //newPos.z += moveSpeed * Time.deltaTime;
            //transform.position = newPos;
            rb.velocity = tr.forward * moveSpeed;
            uiMoney.UpdatePosition(transform.position);
        }

        if (Input.GetKeyUp(KeyCode.W))
        {
            rb.velocity = Vector3.zero;
            uiMoney.UpdatePosition(transform.position);
        }

            if (Input.GetKey(KeyCode.S))
            {
                Vector3 newPos =
                    transform.position +
                    (-tr.forward * moveSpeed * Time.deltaTime);

                transform.position = newPos;
            uiMoney.UpdatePosition(transform.position);
        }

        if (Input.GetKey(KeyCode.A))
        {
            tr.Translate(
                Vector3.left * moveSpeed * Time.deltaTime);
            uiMoney.UpdatePosition(transform.position);
        }

        if (Input.GetKey(KeyCode.D))
        {
            tr.Translate(
                Vector3.right * moveSpeed * Time.deltaTime);
            uiMoney.UpdatePosition(transform.position);
        }

        if (Input.GetKey(KeyCode.Q))
        {
            Vector3 newRot = tr.rotation.eulerAngles;
            newRot.y -= rotSpeed * Time.deltaTime;
            tr.rotation = Quaternion.Euler(newRot);
            //tr.rotation.eulerAngles = newRot;

            //r.localRotation
        }

        if (Input.GetKey(KeyCode.E))
        {
            // Function Overloading
            tr.Rotate(Vector3.up,
                      rotSpeed * Time.deltaTime);
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            tr.localScale = Vector3.one * 2f;
        }

        if (Input.GetKeyDown(KeyCode.F))
        {
            tr.localScale = Vector3.one;
        }

        // 마우스 입력
        if(Input.GetMouseButtonDown(0))//예전방식
        {
            if(mlbCallback != null)  
                mlbCallback();
        }

        if(Input.GetMouseButtonDown(1))//지금방식
        {
            mrbCallback?.Invoke();  //?는 null 검사 하는것
        }

        if (Input.GetKeyDown(KeyCode.U))
        {
            useCallBack?.Invoke();
        }


    }


    //마우스 입력

  

    //델리게이트 설정 함수
    public void SetMLBDelegate(MLBDelegate _callback)
    {
        mlbCallback = _callback;   
    }

    public void SetMRBDelegate(MRBDelegate _callback)
    {
        mrbCallback = _callback;
    }

    public void SetUseDelegate(VoidVoidDelegate _callback)
    {
        useCallBack = _callback;
    }
    /*
    private void OnCollisionEnter(Collision _collision)
    {
        Debug.Log(_collision.gameObject.name);
    }
    */

}
