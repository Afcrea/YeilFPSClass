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

    Transform playerTr; //�÷��̾� ��ġ ���� ����
    Transform enemyTr; //���� ��ġ ���� ����
    Animator animator; 

    public float attackDist = 5f; //���� ��Ÿ�
    public float traceDist = 10f; //���� ��Ÿ�
    public bool isDie = false; //��� ���� �Ǵ� ����

    //�ڷ�ƾ���� ����� �ð� ���� ����
    WaitForSeconds ws;

    //Enemy�� �������� �����ϴ� MoveAgent ��ũ��Ʈ ��������
    MoveAgent moveAgent;
    EnemyFire enemyFire;
    

    //�ִϸ��̼� ���� �Ķ����
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
        //�ڷ�ƾ �Լ��� �ݵ�� StartCoroutine�� ���ؼ� ����
        StartCoroutine(CheckState());
        StartCoroutine(Action());
    }

    IEnumerator CheckState()
    {
        //������ �ִ� ���� ���� ����
        while(!isDie)
        {
            if (state == State.DIE)
                yield break;

            //Distance(A, B) = A�� B ������ �Ÿ��� ���
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

    //��ȭ�� State�� ���� ���� �ൿ�� ó���ϴ� �ڷ�ƾ �Լ�
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

                    //������� �ݶ��̴� ��Ȱ��ȭ ����� �;� �ȸ���
                    GetComponent<CapsuleCollider>().enabled = false;
                    break;

            }
        }
    }

    private void Update()
    {
        //MoveAgent�� ������ �ִ� speed ������Ƽ ���� �ִϸ������� speed �Ķ���Ϳ� �����Ͽ� �ӵ�����
        animator.SetFloat(hashSpeed, moveAgent.speed);
    }

}
