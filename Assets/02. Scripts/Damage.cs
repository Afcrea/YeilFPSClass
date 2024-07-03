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
                //플레이어 죽음 메소드 호출
                PlayerDie();
            }    
        }
    }

    void PlayerDie()
    {
        print("플레이어 사망");
    }
}
