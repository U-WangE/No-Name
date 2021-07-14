using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStat : MonoBehaviour
{
    public struct PS
    {
        public int Lv;
        public float Hp;
        public int Stamina; // 공복, 이동
        public int STR; // 공격력
        public int DEF; // 방여력
        public int EXP; // 경험치

        public float Weapon_mastery; // 무기
        public float Tool_mastery; // 도구
        public float Create_mastery; // 제작
        public float Gather_mastery; // 채집
    }

    public PS ps;

    public void StatDown()
    {
        var hpmp = GameObject.Find("player_State").GetComponent<StatsManager>();
        hpmp.StatManager(-1,-1);
    }

}