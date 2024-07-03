using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyDamage : MonoBehaviour
{
    const string bulletTag = "BULLET";

    float hp = 100f;
    GameObject bloodEffect;

    private void Start()
    {
        // Resources.Load<GameObject>("파일경로")  Images/Blood
        bloodEffect = Resources.Load<GameObject>("Blood");
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag(bulletTag))
        {
            //혈흔효과 생성 함수 호출
            ShowBloodEffect(collision);

            Destroy(collision.gameObject);
            //BulletCtrl에 작성한 damage 변수의 값을 가져와서 체력에서 - 해줌
            hp -= collision.gameObject.GetComponent<BulletCtrl>().damage;
            if (hp <= 0f)
            {
                GetComponent<EnemyAI>().state = EnemyAI.State.DIE;
            }
        }
    }

    void ShowBloodEffect(Collision coll)
    {
        //충돌지점 위치 가져오기
        Vector3 pos = coll.contacts[0].point;

        //충돌 지점의 법선벡터 구하기
        Vector3 _normal = coll.contacts[0].normal;

        //총알의 탄두와 마주보는 방향값 구하기
        Quaternion rot = Quaternion.FromToRotation(-Vector3.forward, _normal);

        GameObject blood = Instantiate<GameObject>(bloodEffect, pos, rot);
        Destroy(blood, 1f);

    }
}
