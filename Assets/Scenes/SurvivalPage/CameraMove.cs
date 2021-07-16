using UnityEngine;

public class CameraMove : MonoBehaviour
{
    public Transform target;

    GameObject cameramove;

    // 해상도 x 1280에 y 720에 10배로 17.777..:10의 비율로 게임 화면에 주사되고 있다. 고로 1280/720*10/2
    // 임시로 설정한 x 150의 floor를 기준으로 카메라의 이동을 제한해 보았다.
    private float mapMax = Mathf.Round((150 - 640 / 72f));

    private void FixedUpdate()
    {

        if (target != null)
        {
            float playerX = target.position.x;
            float playerY = target.position.y;
            float playerZ = target.position.z;
            if (playerX < -5.5f && playerX > -57.5f)
            {
                if (playerX < mapMax && playerX > (mapMax * (-1f)))
                    // 캐릭터를 화면의 중심에서 아래 1/3 위치하게 놓았다.
                    transform.position = new Vector3(playerX, playerY + 1.6f, playerZ - 50);
                else if (playerX > 0)
                    transform.position = new Vector3(mapMax, playerY + 1.6f, playerZ - 50);
                else if (playerX < 0)
                    transform.position = new Vector3(mapMax * (-1f), playerY + 1.6f, playerZ - 50);
            }
            else if (playerX >= -5.5f)
                transform.position = new Vector3(-5.5f, playerY + 1.6f, playerZ - 50);
            else if (playerX <= -57.5f)
                transform.position = new Vector3(-57.5f, playerY + 1.6f, playerZ - 50);
        }
    }
}
