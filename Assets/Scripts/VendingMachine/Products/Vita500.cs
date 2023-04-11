using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Vita500 : Product
{
    float count= 0f;
    public override void Use()
    {
        Debug.Log("Drink - Vita 500");
    }

    

    void Update()
    {
        transform.Rotate(Vector3.up * 100 * Time.deltaTime);


        updown();
        //Debug.Log(count);
    }
    void updown()
    {
        count += Time.deltaTime;

        if (count < 1f)
        {

            transform.position += Vector3.up * 1.5f * Time.deltaTime;

        }
        else if (count > 1f && count < 2f)
        {

            transform.position += Vector3.down * 1.5f * Time.deltaTime;

        }
        else
        {
            count = 0;
        }
    }
}
