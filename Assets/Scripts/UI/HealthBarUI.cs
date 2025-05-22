using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 플레이어의 체력을 Slider UI로 표시하는 클래스
/// - healthSlider에 연결된 Slider의 값을 실시간 갱신
/// </summary>
public class HealthBarUI : MonoBehaviour
{
    // Slider 컴포넌트를 참조합니다.
    public Slider healthSlider;

    /// <summary>
    /// 현재 체력과 최대 체력을 받아와 Slider에 반영합니다.
    /// </summary>
    /// <param name="current">현재 체력</param>
    /// <param name="max">최대 체력</param>
    public void SetHealth(float current, float max)
    {
        healthSlider.maxValue = max;     // 슬라이더 최대값 설정 (ex: 100)
        healthSlider.value = current;    // 현재 체력 값을 설정 (ex: 72)
    }
}
