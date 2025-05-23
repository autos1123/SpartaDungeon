using UnityEngine;

public class PlayerStamina : MonoBehaviour
{
    public float maxStamina = 100f;
    public float currentStamina;
    public float recoveryRate = 10f; // 초당 회복량

    public bool IsRecovering => !isConsuming;
    private bool isConsuming;

    private void Awake()
    {
        currentStamina = maxStamina;
    }

    private void Update()
    {
        if (!isConsuming && currentStamina < maxStamina)
        {
            currentStamina += recoveryRate * Time.deltaTime;
            currentStamina = Mathf.Min(currentStamina, maxStamina);
        }

        isConsuming = false; // 매 프레임 초기화  consumeStamina에서 true로 변경
    }

    public bool ConsumeStamina(float amount)
    {
        if (currentStamina >= amount)
        {
            currentStamina -= amount;
            isConsuming = true;
            Debug.Log($"[Stamina] 소모됨: {amount} / 남은 스태미나: {currentStamina}");
            return true;
        }
        Debug.Log("[Stamina] 부족하여 소모 실패");
        return false;
    }

    public float GetNormalized() => currentStamina / maxStamina;
}
