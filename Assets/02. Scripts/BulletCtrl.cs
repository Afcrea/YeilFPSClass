using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletCtrl : MonoBehaviour
{
    public float damage = 20f; //총알 공격력
    public float speed = 2000f; //총알 이동 속도




    void Start()
    {

        GetComponent<Rigidbody>().AddForce(transform.forward * speed);
                
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
