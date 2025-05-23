using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/// <summary>
/// ���� ������ ���� ���� (���� ���� �ð�, Ŭ����, ���ӿ��� ��)�� ����ϴ� �Ŵ����Դϴ�.
/// �ܺ� Ʈ���ſ��� TriggerClear()�� ȣ���� Ŭ���� ó���մϴ�.
/// </summary>
public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [Header("UI")]
    public Text timerText;
    public GameObject gameOverUI;
    public GameObject gameClearUI;

    private float elapsedTime = 0f;
    private bool isGameOver = false;
    private bool isGameClear = false;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    private void Update()
    {
        if (isGameOver || isGameClear) return;

        elapsedTime += Time.deltaTime;
        if (timerText != null)
            timerText.text = $"Time: {elapsedTime:F1}";
    }

    public void TriggerGameOver()
    {
        if (isGameOver || isGameClear) return;
        isGameOver = true;
        Time.timeScale = 0f;
        gameOverUI?.SetActive(true);
    }

    public void TriggerClear()
    {
        if (isGameOver || isGameClear) return;
        isGameClear = true;
        Time.timeScale = 0f;
        gameClearUI?.SetActive(true);
    }

    public void Retry()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public float GetElapsedTime() => elapsedTime;
}
