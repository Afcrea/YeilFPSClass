using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCam : MonoBehaviour
{
    public Transform target;
    public float moveDamping = 15f;  //�̵� �ӵ� ���
    public float rotateDamping = 10f;  //ȸ���ӵ� ���
    public float distance = 5f;  //���� ������ �Ÿ�
    public float height = 4f;  //���� ������ ����
    public float targetOffset = 2f;  //���� ��ǥ�� ������

    Transform tr;

    // Start is called before the first frame update
    void Start()
    {
        tr = GetComponent<Transform>();
        
    }

    // Update is called once per frame
    void LateUpdate()
    {
        var camPos = target.position - (target.forward * distance) + (target.up * height);
        // var ������ ���� �ڷ����̶�� �� �� ������, ó�� ���� ������ �� ������ Ÿ������ ��������.
        // ���� Ÿ���� ������ �Ұ��ϴ�
        // var�� Vector3 �������� �����ڷ����� ����ϴ� ������ �������� ���ؼ� ����Ѵ�.
        //object�� var�� ���� 
        //var�� ���ÿ� ���̴� ������ object�� ���� �޸� ���������� ����.

        //�̵��� �� �ӵ� ��� ����
        tr.position = Vector3.Lerp(tr.position, camPos, Time.deltaTime * moveDamping);

        //ȸ���� �� �ӵ� ��� ����
        tr.rotation = Quaternion.Slerp(tr.rotation, target.rotation, Time.deltaTime * rotateDamping);

        //ī�޶� ��ġ �� ȸ�� �̵� �Ŀ� Ÿ���� �ٶ󺸵���
        tr.LookAt(target.position + (target.up * targetOffset));
        
               
    }

    //������� ���信�� ���̴� ������ ���ε��� Ȯ���ϱ� ���� �޼ҵ�
    private void OnDrawGizmos()
    {
        //������� ���� ����
        Gizmos.color = Color.green;
        //DrawWireSphere(��ġ, �ݰ�)
        //����𿡴� ��� ������ ����� ����
        //DrawWireSphere�� ������ �̷���� ���� ���
        Gizmos.DrawWireSphere(target.position + (target.up * targetOffset), 0.1f);
        //����ī�޶�� ����� ���� ǥ��
        //DrawLine(A��ġ, B��ġ) = A�� B ���� �� �߱�
        Gizmos.DrawLine(target.position + (target.up * targetOffset), transform.position);
    }
}
