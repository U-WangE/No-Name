using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterMove : MonoBehaviour
{
    Rigidbody2D rigid;
    public int nextMove; // 다음 행동 지표를 결정할 변수

    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        Invoke("Think", 2f); // 초기화 함수 안에 넣어서 실행될 때 마다(최초 1회) nextMove 변수가 초기화 되도록 함
    }

    private void FixedUpdate()
    {
        rigid.velocity = new Vector2(nextMove * 4f, rigid.velocity.y); // nextMove에 0 : 멈춤 -1 : 왼쪽 1: 오른쪽 이동

        // 바닥의 지형을 탐색
        Vector2 downVec = new Vector2(rigid.position.x + nextMove * 0.4f, rigid.position.y); // 진행 방향의 바닥 지형을 탐색
        Debug.DrawRay(downVec, Vector3.down, new Color(0, 1, 0)); // 진행 방향의 바닥으로 ray를 쏜다
        RaycastHit2D raycastDown = Physics2D.Raycast(downVec, Vector2.down, 1, LayerMask.GetMask("Floor")); // ray를 쏴서 맞은 오브젝트 탐지

        // 진행 방향 탐색
        float rayDirection = 1f; // 좌우 이동시 ray의 방향 조정

        if (nextMove > 0)  // right
            rayDirection = 1f;
        else  // left
            rayDirection = -1f;

        Vector2 frontVec = new Vector2(rigid.position.x + nextMove * 0.7f, rigid.position.y); // 진행 방향의 전방 Zone을 탐색
        Debug.DrawRay(frontVec, Vector3.right * rayDirection * 0.1f, new Color(0, 1, 0)); // 진행 방향의 전방으로 ray를 쏜다
        RaycastHit2D raycastFront = Physics2D.Raycast(frontVec, Vector2.right * rayDirection, 0.1f, LayerMask.GetMask("MonsterZone")); // ray를 쏴서 맞은 오브젝트 탐지

        // 탐지된 오브젝트가 null
        if (raycastDown.collider == null || raycastFront.collider == null)
        {
            nextMove = nextMove * (-1); // 방향을 바꾸어줌

            CancelInvoke(); // think를 잠시 멈춘 후 재실행
            Invoke("Think", 1f);
        }
    }

    void Think()
    {
        float time = Random.Range(0.5f, 2f); // 생각하는 시간 랜덤 부여

        // 몬스터가 스스로 생각해서 판단 (-1 : 왼쪽 이동, 1 : 오른쪽 이동, 0 : 멈춤)
        // Random.Range : 최소 <= 난수 <최대 / 범위의 랜덤 수를 생성
        nextMove = Random.Range(-1, 2);

        Invoke("Think", time); // 매개변수로 받은 함수를 5초의 딜레이를 부여하여 재실행
    }

}
