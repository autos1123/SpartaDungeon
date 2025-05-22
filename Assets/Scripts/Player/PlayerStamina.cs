using UnityEngine;

public class PlayerStamina : MonoBehaviour
{
    public float maxStamina = 100f;
    public float currentStamina;
    public float recoveryRate = 10f; // �ʴ� ȸ����

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

        isConsuming = false; // �� ������ �ʱ�ȭ  consumeStamina���� true�� ����
    }

    public bool ConsumeStamina(float amount)
    {
        if (currentStamina >= amount)
        {
            currentStamina -= amount;
            isConsuming = true;
            Debug.Log($"[Stamina] �Ҹ��: {amount} / ���� ���¹̳�: {currentStamina}");
            return true;
        }
        Debug.Log("[Stamina] �����Ͽ� �Ҹ� ����");
        return false;
    }

    public float GetNormalized() => currentStamina / maxStamina;
}
