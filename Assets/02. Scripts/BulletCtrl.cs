using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletCtrl : MonoBehaviour
{
    public float damage = 20f; //�Ѿ� ���ݷ�
    public float speed = 2000f; //�Ѿ� �̵� �ӵ�




    void Start()
    {

        GetComponent<Rigidbody>().AddForce(transform.forward * speed);
                
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
