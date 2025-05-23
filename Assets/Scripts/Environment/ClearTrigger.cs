using UnityEngine;

public class ClearTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GameManager.Instance.TriggerClear();
        }
    }
}
