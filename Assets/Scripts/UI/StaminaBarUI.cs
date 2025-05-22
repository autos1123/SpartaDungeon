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
            // ���� ���� �� ���¹̳� �ٸ� ���� ������ �ʱ�ȭ
            slider.value = stamina.GetNormalized();
        }
    }

    void Update()
    {
        if (stamina != null && slider != null)
        {
            // �� ������ �ǽð� �ݿ�
            slider.value = stamina.GetNormalized();
        }
    }
}
