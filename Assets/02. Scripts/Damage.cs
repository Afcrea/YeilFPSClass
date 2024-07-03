using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damage : MonoBehaviour
{
    const string bulletTag = "BULLET";
    float initHp = 100f;
    public float currHp;

    private void Start()
    {
        currHp = initHp;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag(bulletTag))
        {
            Destroy(other.gameObject);

            currHp -= 5f;

            if(currHp < 0)
            {
                //�÷��̾� ���� �޼ҵ� ȣ��
                PlayerDie();
            }    
        }
    }

    void PlayerDie()
    {
        print("�÷��̾� ���");
    }
}
