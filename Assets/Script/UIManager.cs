using UnityEngine;
using TMPro; // Namespace untuk TextMeshPro
using UnityEngine.SceneManagement; // Untuk restart scene

public class UIManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI coinText;
    [SerializeField] private GameObject gameOverPanel;
    [SerializeField] private GameObject gamePausePanel;
    [SerializeField] private GameObject controll;

    private int coinCount = 0;

    void Start()
    {
        Time.timeScale = 1f;
        controll.SetActive(true);
        gamePausePanel.SetActive(false);
        gameOverPanel.SetActive(false); // Pastikan panel mati saat mulai
        UpdateCoinUI();
    }

    public void AddCoin()
    {
        coinCount++;
        UpdateCoinUI();
    }

    private void UpdateCoinUI()
    {
        coinText.text = "Coins: " + coinCount;
    }

    public void ShowGameOver()
    {
        controll.SetActive(false);
        gameOverPanel.SetActive(true);
    }

    // Method ini akan dipanggil oleh tombol Restart
    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void BacktoMenu()
    {
        SceneManager.LoadScene("Menu");
    }

    public void Pause()
    {
        controll.SetActive(false);
        gamePausePanel.SetActive(true);
        Time.timeScale = 0f;
    }

    public void continueGame()
    {
        controll.SetActive(true);
        gamePausePanel.SetActive(false);
        Time.timeScale = 1f;
    }
}