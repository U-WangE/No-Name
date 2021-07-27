using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterStat : MonoBehaviour
{
    public float maxHp { get; set; }
    public float curHp { get; set; }
    public float STR { get; set; }

    void Start()
    {
        InitStat();
    }

    public virtual void InitStat()
    {

    }

    public void SetEnemyAttack(float enemyAttackPower)
    {
        curHp -= enemyAttackPower;
        if (curHp <= 0)
            HpZero();
    }

    public virtual void HpZero()
    {

    }
}
