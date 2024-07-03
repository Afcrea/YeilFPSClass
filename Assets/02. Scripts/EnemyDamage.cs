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
        // Resources.Load<GameObject>("���ϰ��")  Images/Blood
        bloodEffect = Resources.Load<GameObject>("Blood");
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag(bulletTag))
        {
            //����ȿ�� ���� �Լ� ȣ��
            ShowBloodEffect(collision);

            Destroy(collision.gameObject);
            //BulletCtrl�� �ۼ��� damage ������ ���� �����ͼ� ü�¿��� - ����
            hp -= collision.gameObject.GetComponent<BulletCtrl>().damage;
            if (hp <= 0f)
            {
                GetComponent<EnemyAI>().state = EnemyAI.State.DIE;
            }
        }
    }

    void ShowBloodEffect(Collision coll)
    {
        //�浹���� ��ġ ��������
        Vector3 pos = coll.contacts[0].point;

        //�浹 ������ �������� ���ϱ�
        Vector3 _normal = coll.contacts[0].normal;

        //�Ѿ��� ź�ο� ���ֺ��� ���Ⱚ ���ϱ�
        Quaternion rot = Quaternion.FromToRotation(-Vector3.forward, _normal);

        GameObject blood = Instantiate<GameObject>(bloodEffect, pos, rot);
        Destroy(blood, 1f);

    }
}
