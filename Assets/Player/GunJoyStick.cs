using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;


public class GunJoyStick : MonoBehaviour, IDragHandler, IPointerDownHandler, IPointerUpHandler
{
    public Gun gun;
    public RectTransform touchArea;
    public Image Inner;
    public bool FireBullet = false;
    float angle;

    // 터치하고 있는 동안의 움직임
    public void OnDrag(PointerEventData eventData)
    {
        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(touchArea,
        eventData.position, eventData.pressEventCamera, out Vector2 localVector))
        {
            if (localVector.magnitude < 115)  // outer 안에 있으면 반지름 150까지 원을 그리며 이동 가능
                Inner.transform.localPosition = localVector;
            else  // outer 밖에 있다면 위치 고정
                Inner.transform.localPosition = localVector.normalized * 115;
            
            // JoyStick과 같은 방향으로 gun의 포구를 향하게 하기 위한 코드
            angle = Mathf.Atan2(localVector.y - gun.transform.position.y, localVector.x - gun.transform.position.x) * Mathf.Rad2Deg;
            gun.transform.rotation = Quaternion.AngleAxis(angle + 180, Vector3.forward);
        }
    }

    // 터치하고 있는 동안
    public void OnPointerDown(PointerEventData eventData)
    {
        OnDrag(eventData);
        FireBullet = true;
    }

    // 터치를 그만 뒀을 때
    public void OnPointerUp(PointerEventData eventData)
    {
        Inner.transform.localPosition = Vector2.zero;  // inner의 위치 복귀
        FireBullet = false;
    }
}
