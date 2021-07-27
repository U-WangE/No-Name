using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStatsManager : MonoBehaviour
{
    [SerializeField] Text textHp;
    [SerializeField] Text textSp;
    [SerializeField] Image imageHp;
    [SerializeField] Image imageSp;

    [SerializeField] Text textKill;

    PlayerStat Player;

    // 최대 Hp Sp  
    static float MaxHP = 5f;
    static float MaxSP = 5f;

    // 현재 Hp Sp
    float currentHp = 5f;
    float currentSp = 5f;

    // Kill Count
    int countKill = 0;

    // player stat 초기화
    void PlayerInit() { // 매개변수 넣어서 사용하면 Load 시에도 쓸 수 있을 듯
        // 게임 초기 or Load 시 사용할 듯
        currentHp = MaxHP = Player.stats.Hp = 5f;
        currentSp = MaxSP = Player.stats.Stamina = 5f;
        
        // Hp Sp Text 초기화
        textHp.text = currentHp.ToString() + " / " + MaxHP.ToString();
        textSp.text = currentSp.ToString() + " / " + MaxSP.ToString();

        // Kill 초기화
        textKill.text = countKill.ToString();
    }

    // 해당 씬이 실행 되자 마자 hp mp가 적용 되도록 하기 위해 사용
    private void Start()
    {
        Player = GameManager.Instance.GetComponent<PlayerStat>();
        PlayerInit();
    }

    private void Update()
    {
        HpSpText();
        KillText();
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
            
            // 현재 체력 == 0 player 사망
            if (currentHp == 0) {
                HpZero();
            }
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

    // Hp가 Zero가 되었을 때 Setting 메뉴를 연다.
    void HpZero()
    {
        Destroy(GameObject.Find("Player"));
        GameObject.Find("UICanvas").transform.Find("Setting").gameObject.SetActive(true);
    }

    // kill한 몬스터 수 계산
    public void KillCountDown() {
        countKill += 1;
        KillText();
    }
    
    // kill한 몬스터 수에 따라 text 갱신
    void KillText() {
        textKill.text = countKill.ToString();
    }
}