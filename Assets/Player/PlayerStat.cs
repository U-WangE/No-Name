using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStat : MonoBehaviour
{
    public struct Stats
    {
        public int Lv;
        public float Hp;
        public float Stamina; // 공복, 이동 -> shield point
        public int STR; // 공격력
        public int DEF; // 방여력
        public int EXP; // 경험치

    }

    public Stats stats;

}