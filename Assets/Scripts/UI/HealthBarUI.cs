using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// �÷��̾��� ü���� Slider UI�� ǥ���ϴ� Ŭ����
/// - healthSlider�� ����� Slider�� ���� �ǽð� ����
/// </summary>
public class HealthBarUI : MonoBehaviour
{
    // Slider ������Ʈ�� �����մϴ�.
    public Slider healthSlider;

    /// <summary>
    /// ���� ü�°� �ִ� ü���� �޾ƿ� Slider�� �ݿ��մϴ�.
    /// </summary>
    /// <param name="current">���� ü��</param>
    /// <param name="max">�ִ� ü��</param>
    public void SetHealth(float current, float max)
    {
        healthSlider.maxValue = max;     // �����̴� �ִ밪 ���� (ex: 100)
        healthSlider.value = current;    // ���� ü�� ���� ���� (ex: 72)
    }
}
