using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CT_Sphere : MonoBehaviour
{
    // Callback Function
    private void OnCollisionEnter(Collision _collision)
    {
        Debug.Log("CT_Sphere Collision");
    }
}
