using System.Collections;
using System.Collections.Generic;
using System.Net.WebSockets;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public enum State
    {
        PATROL,
        TRACE,
        ATTACK,
        DIE
    }

    public State state = State.PATROL;

    Transform playerTr; //플레이어 위치 저장 변수
    Transform enemyTr; //적의 위치 저장 변수
    Animator animator; 

    public float attackDist = 5f; //공격 사거리
    public float traceDist = 10f; //추적 사거리
    public bool isDie = false; //사망 여부 판단 변수

    //코루틴에서 사용할 시간 지연 변수
    WaitForSeconds ws;

    //Enemy의 움직임을 제어하는 MoveAgent 스크립트 가져오기
    MoveAgent moveAgent;
    EnemyFire enemyFire;
    

    //애니메이션 제어 파라미터
    readonly int hashMove = Animator.StringToHash("IsMove");
    readonly int hashSpeed = Animator.StringToHash("Speed");
    readonly int hashDie = Animator.StringToHash("Die");
    readonly int hashDieIdx = Animator.StringToHash("DieIdx");

    // Start is called before the first frame update
    void Awake()
    {
        var player = GameObject.FindGameObjectWithTag("PLAYER");
        if (player != null)
        {
            playerTr = player.GetComponent<Transform>();
        }

        enemyTr = GetComponent<Transform>();
        moveAgent = GetComponent<MoveAgent>();
        enemyFire = GetComponent<EnemyFire>();
        animator = GetComponent<Animator>();

        ws = new WaitForSeconds(0.3f);



    }

    private void OnEnable()
    {
        //코루틴 함수는 반드시 StartCoroutine을 통해서 실행
        StartCoroutine(CheckState());
        StartCoroutine(Action());
    }

    IEnumerator CheckState()
    {
        //생존해 있는 동안 무한 루프
        while(!isDie)
        {
            if (state == State.DIE)
                yield break;

            //Distance(A, B) = A와 B 사이의 거리를 계산
            float dist = Vector3.Distance(playerTr.position, enemyTr.position);
            if(dist <= attackDist)
            {
                state = State.ATTACK;
                
            }
            else if(dist <= traceDist)
            {
                state = State.TRACE;
            }
            else
            {
                state = State.PATROL;
            }
            yield return ws;
        }
    }

    //변화된 State에 따라서 실제 행동을 처리하는 코루틴 함수
    IEnumerator Action()
    {
        while(!isDie)
        {
            yield return ws;
            switch(state)
            {
                case State.PATROL:
                    enemyFire.isFire = false;
                    moveAgent.patrolling = true;
                    animator.SetBool(hashMove, true);
                    break;
                case State.TRACE:
                    enemyFire.isFire = false;
                    moveAgent.traceTarget = playerTr.position;
                    animator.SetBool(hashMove, true);
                    break;
                case State.ATTACK:
                    moveAgent.Stop();
                    animator.SetBool(hashMove, false);
                    if (!enemyFire.isFire)
                        enemyFire.isFire = true;
                    break;
                case State.DIE:
                    isDie = true;
                    enemyFire.isFire = false;

                    moveAgent.Stop();

                    animator.SetInteger(hashDieIdx, Random.Range(0, 3));
                    animator.SetTrigger(hashDie);

                    //사망이후 콜라이더 비활성화 해줘야 촐알 안맞음
                    GetComponent<CapsuleCollider>().enabled = false;
                    break;

            }
        }
    }

    private void Update()
    {
        //MoveAgent가 가지고 있는 speed 프로퍼티 값을 애니메이터의 speed 파라미터에 전달하여 속도변경
        animator.SetFloat(hashSpeed, moveAgent.speed);
    }

}
