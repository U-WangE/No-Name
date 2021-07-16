using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    float Xpos;
    float Ypos;
    public float Xspeed = 0;
    public float Yspeed = 0;
    Quaternion quaternion;

    void Start()
    {
        // bullet의 진행 방향 단위 벡터
        Xpos = Mathf.Cos(transform.rotation.eulerAngles.z * Mathf.PI / 180);
        Ypos = -Mathf.Sin(transform.rotation.eulerAngles.z * Mathf.PI / 180);
        
        // bullet 생성 유지 기간
        Destroy(gameObject, 0.75f);
    }

    void FixedUpdate()
    {
        // 이동
        transform.position += new Vector3(-Xpos * Xspeed * Time.deltaTime, Ypos * Yspeed * Time.deltaTime, 0);
    }

    // Monster와 만났을 시
    private void OnTriggerEnter2D(Collider2D other) {
        if (other.tag == "Monster" || other.tag == "Obstacle")
        {
            Destroy(gameObject);
        }
    }
}