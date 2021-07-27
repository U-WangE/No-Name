using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class RespawnObj : MonoBehaviour
{
    List<Transform> spawnPos = new List<Transform>();
    GameObject[] monsters;

    public GameObject monPrefab;
    public bool DeadNumber = false;
    public int spawnNumber = 1;
    public float respawnDelay = 3f;

    System.Random random = new System.Random();

    void Start()
    {
        MakeSpawnPos();
    }

    void MakeSpawnPos()
    {
        foreach (Transform pos in transform)
        {
            if (pos.tag == "Respawn")
            {
                spawnPos.Add(pos); // 수정 합시다. 이거는 위치가 너무 고정이다.
            }
        }

        monsters = new GameObject[spawnNumber];

        MakeMonsters();
    }

    // 프리팹으로 부터 몬스터를 만들어 관리하는 함수
    void MakeMonsters()
    {
        for (int i = 0; i < spawnNumber; i++)
        {
            int a = random.Next(spawnPos.Count - 1);
            GameObject mon = Instantiate(monPrefab, spawnPos[a].position, Quaternion.identity) as GameObject;

            mon.SetActive(false);

            monsters[i] = mon;
        }
    }

    void RespawnDelay()
    {
        for (int i = 0; i < spawnNumber; i++)
        {
            if(monsters[i] == null) {
                GameObject mon = Instantiate(monPrefab, spawnPos[random.Next(spawnPos.Count - 1)].position, Quaternion.identity) as GameObject;
                monsters[i] = mon;
                monsters[i].GetComponent<MonsterStatsManager>().InitStat();
                monsters[i].SetActive(true);
                return;
            }
        }
    }

    void SpawnMonster()
    {
        for (int i = 0; i < monsters.Length; i++)
        {
            monsters[i].GetComponent<MonsterStatsManager>().InitStat();
            monsters[i].SetActive(true);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            SpawnMonster();
        }
    }

    private void Update() {
        if (DeadNumber) {
            Invoke("RespawnDelay", respawnDelay);
            DeadNumber = false;
        }
    }
}
