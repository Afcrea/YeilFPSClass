using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shake : MonoBehaviour
{
    //����ũ ȿ�� �� ī�޶�
    public Transform shakeCamera;

    public bool shakeRotate = false;

    Vector3 originPos;
    Quaternion originRot;


    void Start()
    {
        //�������� ���� ��ġ���� ȸ���� �����صα�
        originPos = shakeCamera.localPosition;
        originRot = shakeCamera.localRotation;
        
    }

    public IEnumerator ShakeCamera(float duration = 0.05f, float magnitudePos = 0.03f, float magnitudeRot = 0.1f)
    {
        float passTIme = 0f;

        //duration Ÿ�� ���� ���� ���ؼ� while ���
        while ( passTIme < duration)
        {
            //�������� 1�� ������ ���� �ȿ��� ������ 3���� ��ǥ(x, y, z) ����
            Vector3 shakePos = Random.insideUnitSphere;

            //������ ���� ������ġ�� �Ű����� ���ؼ� ����
            shakeCamera.localPosition = shakePos * magnitudePos;
            
            //�ұ�Ģ�� ȸ�� ��� ����
            if(shakeRotate)
            {
                //������ �޸������� �Լ��ν� � �ұ�Ģ�� ������ ���������� ��
                //���� �� ����� ����
                float z = Mathf.PerlinNoise(Time.time * magnitudeRot, 0f);
                Vector3 shakeRot = new Vector3(0, 0, z);

                shakeCamera.localRotation = Quaternion.Euler(shakeRot);

            }
            passTIme += Time.deltaTime;
            yield return null;
        }

        shakeCamera.localPosition = originPos;
        shakeCamera.localRotation = originRot;
    }

}
