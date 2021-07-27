using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MonsterStatsManager : MonsterStat
{
    [SerializeField] Image imageHp;

    float Triggertime;

    public override void InitStat()
    {
        curHp = maxHp = 5f;
        STR = 1f;
    }

    // 해당 씬이 실행 되자 마자 hp mp가 적용 되도록 하기 위해 사용
    private void Start()
    {
        InitStat();
    }


    // Update is called once per frame
    void Update()
    {
        HpImage();
    }

    void HpImage()
    {
        // 현재 체력에 맞게 hp bar의 width를 조절
        imageHp.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, curHp / maxHp);
    }

    // Hp가 Zero가 되었을 때 Setting 메뉴를 연다.
    public override void HpZero()
    {
        GameObject.Find("RespawnObj").GetComponent<RespawnObj>().DeadNumber = true;
        GameObject.Find("Player_State").GetComponent<PlayerStatsManager>().KillCountDown();
        Destroy(gameObject);
    }


    void AttackToPlayer()
    {
        // 몬스터가 플레이어에게 데미지를 준다
        GameObject.Find("Player_State").GetComponent<PlayerStatsManager>().StatManager(STR*(-1f), 0);
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        // 총에 맞았을 때
        if (other.tag == "Bullet")
        {
            SetEnemyAttack(1f);
        }
        // 플레이어와 부딪쳤을 때
        else if (other.tag == "Player") // 다시 공격에 Delay를 줘서 collider가 중복으로 triggerEnter되는 것을 막아보자
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
            if (Triggertime > 2f)
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
