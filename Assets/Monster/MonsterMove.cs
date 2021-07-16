using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterMove : MonoBehaviour
{
    Rigidbody2D rigid;

    private void Awake() {
        rigid = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate() {
        rigid.velocity = new Vector2(-1, rigid.velocity.y); // 왼쪽이동.
    }
    
    private void OnTriggerEnter2D(Collider2D other) {
        if (other.tag == "Bullet")
        {
            GameObject.Find("player_State").GetComponent<StatsManager>().KillCountDown();
            Destroy(gameObject);
        }
    }
}
