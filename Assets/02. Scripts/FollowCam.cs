using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCam : MonoBehaviour
{
    public Transform target;
    public float moveDamping = 15f;  //이동 속도 계수
    public float rotateDamping = 10f;  //회전속도 계수
    public float distance = 5f;  //추적 대상과의 거리
    public float height = 4f;  //추적 대상과의 높이
    public float targetOffset = 2f;  //추적 좌표의 오프셋

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
        // var 변수는 가변 자료형이라고 볼 수 있지만, 처음 값이 설정될 때 지정된 타입으로 굳혀진다.
        // 이후 타입의 변경은 불가하다
        // var는 Vector3 값이지만 가변자료형을 사용하는 이유는 유연성을 위해서 사용한다.
        //object와 var의 차이 
        //var은 스택에 싸이는 값형식 object는 힙에 메모리 참조형으로 사용됨.

        //이동할 때 속도 계수 적용
        tr.position = Vector3.Lerp(tr.position, camPos, Time.deltaTime * moveDamping);

        //회전할 때 속도 계수 적용
        tr.rotation = Quaternion.Slerp(tr.rotation, target.rotation, Time.deltaTime * rotateDamping);

        //카메라가 위치 및 회전 이동 후에 타겟을 바라보도록
        tr.LookAt(target.position + (target.up * targetOffset));
        
               
    }

    //기즈모라는 씬뷰에서 보이는 가상의 라인들을 확인하기 위한 메소드
    private void OnDrawGizmos()
    {
        //기즈모의 색상 지정
        Gizmos.color = Color.green;
        //DrawWireSphere(위치, 반경)
        //기즈모에는 몇가지 지정된 모양이 있음
        //DrawWireSphere는 선으로 이루어진 구형 모양
        Gizmos.DrawWireSphere(target.position + (target.up * targetOffset), 0.1f);
        //메인카메라와 대상간의 선을 표시
        //DrawLine(A위치, B위치) = A와 B 사이 선 긋기
        Gizmos.DrawLine(target.position + (target.up * targetOffset), transform.position);
    }
}
