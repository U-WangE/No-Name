using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 몸통 박치기
public class BodyButting : MonoBehaviour
{
    float Triggertime;

    void AttackToPlayer()
    {
        // 몬스터가 플레이어에게 데미지를 준다
        GameObject.Find("player_State").GetComponent<PlayerStatsManager>().StatManager(-1, 0);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // 플레이어와 겹칠 때
        if (other.tag == "Player")
        {
            AttackToPlayer();
        }
    }
    
    
    private void OnTriggerStay2D(Collider2D other)
    {
        // 특정 시간 동안 계속 몬스터와 겹쳐 있다면 플레이어에게 데미지를 준다
        if (other.tag == "Player")
        {
            Triggertime += Time.deltaTime;
            if (Triggertime > 1f)  // 임의로 2초
            {
                //Action
                AttackToPlayer();
                Triggertime = 0f;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        // 몬스터와 플레이어가 겹쳐 있지 않다면 Triggertime 초기화
        if (other.tag == "Player")
        {
            Triggertime = 0f;
        }
    }
}
