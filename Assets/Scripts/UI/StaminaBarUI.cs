using UnityEngine;
using UnityEngine.UI;

public class StaminaBarUI : MonoBehaviour
{
    public PlayerStamina stamina;
    public Slider slider;

    void Start()
    {
        if (stamina != null && slider != null)
        {
            // 게임 시작 시 스태미나 바를 현재 값으로 초기화
            slider.value = stamina.GetNormalized();
        }
    }

    void Update()
    {
        if (stamina != null && slider != null)
        {
            // 매 프레임 실시간 반영
            slider.value = stamina.GetNormalized();
        }
    }
}
