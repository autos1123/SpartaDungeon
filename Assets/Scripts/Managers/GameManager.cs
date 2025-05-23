using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/// <summary>
/// ���� ������ ���� ���� (���� ����, Ŭ����, ���ӿ���, �Ͻ����� ��)�� ����ϴ� �Ŵ����Դϴ�.
/// ����ȭ�� �ǳ� ���� �� Ű �Է¿� ���� ���� �帧�� ó���մϴ�.
/// </summary>
public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [Header("UI Panels")]
    public GameObject startUI;
    public GameObject gameOverUI;
    public GameObject gameClearUI;
    public TextMeshProUGUI timerText;
    public TextMeshProUGUI clearTimeText;

    private float elapsedTime = 0f;
    private bool isGameOver = false;
    private bool isGameClear = false;
    private bool gameStarted = false;

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

    private void Start()
    {
        Time.timeScale = 0f; // ����ȭ�鿡�� �Ͻ�����
        if (startUI != null)
            startUI.SetActive(true);
    }

    private void Update()
    {
        if (!gameStarted) return;
        if (isGameOver || isGameClear) return;

        elapsedTime += Time.deltaTime;
        if (timerText != null)
            timerText.text = $"�ð�: {elapsedTime:F1}";

        // �����
        if (Input.GetKeyDown(KeyCode.R))
        {
            Retry();
        }

        // ����
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            QuitGame();
        }
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
        if (clearTimeText != null)
            clearTimeText.text = $"���: {elapsedTime:F1}";
    }

    public void StartGame()
    {
        gameStarted = true;
        Time.timeScale = 1f;
        if (startUI != null)
        {
            startUI.SetActive(false);
            if (timerText != null)
                timerText.gameObject.SetActive(true);
        }
            
    }

    public void Retry()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void QuitGame()
    {
#if UNITY_EDITOR
        EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
    }

    public float GetElapsedTime() => elapsedTime;
}
