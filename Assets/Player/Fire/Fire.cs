using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire : MonoBehaviour
{
    public GameObject Bullet;
    public Transform GunPos;
    public GunJoyStick gunJoyStick;
    Quaternion quaternion;
    Vector3 vec;

    bool charging = false; // 딜레이 중인지 아닌지 판단
    float time = 0.5f; // 발사 딜레이
    float RPM = 0.5f; // 분당 발사 속도

    private void Update()
    {
        // 공격 JoyStick을 클릭(터치) 했고, 딜레이 중이 아닌 경우
        if (gunJoyStick.FireBullet && !charging)
        {
            time += Time.deltaTime;
            if (time > RPM) // 발사 딜레이
            {
                vec = transform.position;
                Instantiate(Bullet, vec, GunPos.transform.rotation); // 현재 radian을 향해 bullet clone 생성
                time = 0f;
            }
        }
        else if (time < 0.5f) // 연속 클릭(터치)로 발사 속도와 관계없이 연사되는 것을 방지하기 위함
        {
            charging = true;
            time += Time.deltaTime;
        }
        else // 대기 상태 : 발사 딜레이가 끝났지만, 공격 JoyStick을 클릭하지 않은 경우
        {
            charging = false;
            time = 0.5f;
            RPM = 0.5f;
        }
    }
}