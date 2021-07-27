using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerMove : MonoBehaviour
{
    Rigidbody2D rigid;
    public float maxSpeed = 5f;  // 이후 private로 변경해 주자...
    public float jumpPower = 10f;  // 이후 private로 변경해 주자...

    private bool IsJumping; // 1단 점프로 제한

    // 터치 인식
    public bool InputJump;
    public bool InputRight;
    public bool InputLeft;
    public bool keyup; // 화면에서 손이 떨어졌을 때 인식

    private void Start()
    {
        rigid = GetComponent<Rigidbody2D>();

        IsJumping = false;
        InputJump = false;
        InputLeft = false;
        keyup = false;
    }

    private void FixedUpdate()
    {
        Move();
        Jump();
    }


    // 이동 관련
    void Move()
    {
        // Keyboard로 이동
        // button up 일때 멈추는 스피드
        if (Input.GetButtonUp("Horizontal"))
        {
            rigid.velocity = new Vector2(rigid.velocity.normalized.x * 0.5f, rigid.velocity.y);
        }

        // Move speed
        float h = Input.GetAxisRaw("Horizontal");
        rigid.AddForce(Vector2.right * h * Time.fixedDeltaTime * 10f, ForceMode2D.Impulse);

        // Max Speed
        if (rigid.velocity.x > maxSpeed) // Right Max Speed
            rigid.velocity = new Vector2(maxSpeed, rigid.velocity.y);
        else if (rigid.velocity.x < maxSpeed * (-1f)) // Left Max Speed
            rigid.velocity = new Vector2(maxSpeed * (-1f), rigid.velocity.y);

        // Touch로 이동
        // touch up 일때 멈추는 스피드
        if (keyup)
        {
            rigid.velocity = new Vector2(rigid.velocity.normalized.x * 0.5f, rigid.velocity.y);
            keyup = false;
        }

        // left 이동 관련
        if (InputLeft)
        {
            // move speed
            rigid.AddForce(Vector2.left * Time.fixedDeltaTime * 10f, ForceMode2D.Impulse);

            // max speed
            if (rigid.velocity.x < maxSpeed * (-1f))
                rigid.velocity = new Vector2(maxSpeed * (-1f), rigid.velocity.y);
        }

        // move 이동 관련
        if (InputRight)
        {
            // move speed
            rigid.AddForce(Vector2.right * Time.fixedDeltaTime * 10f, ForceMode2D.Impulse);

            // max speed
            if (rigid.velocity.x > maxSpeed)
                rigid.velocity = new Vector2(maxSpeed, rigid.velocity.y);
        }
    }


    // 오른쪽 화살표 터치
    public void RightDown()
    {
        InputRight = true;
    }
    public void RightUp()
    {
        InputRight = false;
        keyup = true;
    }

    // 왼쪽 화살표 터치
    public void LeftDown()
    {
        InputLeft = true;
    }
    public void LeftUp()
    {
        InputLeft = false;
        keyup = true;
    }

    // Jump 관련
    void Jump()
    {
        // 스페이스 키 or 점프 버튼 터치시 점프
        if (Input.GetButtonDown("Jump"))
            InputJump = true;
        if (InputJump)
        {
            if (!IsJumping)
            {
                // 바닥에 닿아야만 다시 점프 가능
                rigid.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
                IsJumping = true;
            }
            InputJump = false;
        }
    }

    // Jump 버튼 터치
    public void JumpClick()
    {
        InputJump = true;
    }


    private void OnCollisionEnter2D(Collision2D other)
    {
        // 바닥에 닿았는지 인식 -> 바닥에 닿아야 점프 할 수 있다.
        if (other.gameObject.name == "Floor")
            IsJumping = false;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // 몬스터와 부딪혔을 때
        if (other.name == "Monster_A")
        {
            KnockBack(other.transform.position);
        }
    }

    void KnockBack(Vector2 pos)
    {
        // 반작용. 플레이어의 포지션 x값에서  충돌체의 포지션 x값을 뺀 값이  0보다 크다면 1 작다면 -1
        // 즉 부딪힌 목표물보다 왼쪽에서 맞았으면 왼쪽에서 튕겨나감. 반대는 오른쪽
        int reaction = transform.position.x - pos.x > 0 ? 1 : -1;
        rigid.AddForce(new Vector2(reaction, 1) * 8f, ForceMode2D.Impulse);
    }
}
