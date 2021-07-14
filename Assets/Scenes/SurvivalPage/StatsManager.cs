using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatsManager : MonoBehaviour
{
    [SerializeField] Text textHp;
    [SerializeField] Text textSp;
    [SerializeField] Image imageHp;
    [SerializeField] Image imageSp;

    PlayerStat Stat;

    // 최대 Hp Sp  
    static float MaxHP = 5;
    static float MaxSP = 5;

    float currentHp = 5;
    float currentSp = 5;

    // player stat 초기화
    void PlayerInit() {
        Stat.ps.Hp = 5;
        Stat.ps.Stamina = 5;
        currentHp = MaxHP = Stat.ps.Hp;
        currentSp = MaxSP = Stat.ps.Stamina;
        
        textHp.text = currentHp.ToString() + " / " + MaxHP.ToString();
        textSp.text = currentSp.ToString() + " / " + MaxSP.ToString();
    }

    // 해당 씬이 실행 되자 마자 hp mp가 적용 되도록 하기 위해 사용
    private void Start()
    {
        Stat = GameManager.Instance.GetComponent<PlayerStat>();
        PlayerInit();
    }

    // 체력의 회복과 감소로 사용할 수 있는 함수
    public void StatManager(float hp, float sp)
    {
        if (currentHp <= MaxHP)
        {
            currentHp += hp;
            // 현재 체력 + 회복한 체력 > 최대 체력인 경우
            if (currentHp > MaxHP)
                currentHp = MaxHP;
        }

        if (currentSp <= MaxSP)
        {
            currentSp += sp;
            // 현재 체력 + 회복한 체력 > 최대 체력인 경우
            if (currentSp > MaxSP)
                currentSp = MaxSP;
        }
        HpSpText();  // 일종의 업데이트 형식으로 HP와 MP를 갱신 시킨다
    }


    // HpSp에 변화가 있을시 text 갱신하는 함수
    void HpSpText()
    {
        // 현재 체력에 맞게 hp bar의 width를 조절
        imageHp.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, 300 * currentHp / MaxHP);
        textHp.text = currentHp.ToString() + " / " + MaxHP.ToString();

        // 현재 체력에 맞게 hp bar의 width를 조절
        imageSp.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, 300 * currentSp / MaxSP);
        textSp.text = currentSp.ToString() + " / " + MaxSP.ToString();
    }
}