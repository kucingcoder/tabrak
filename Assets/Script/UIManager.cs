using UnityEngine;
using TMPro; // Namespace untuk TextMeshPro
using UnityEngine.SceneManagement; // Untuk restart scene

public class UIManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI coinText;
    [SerializeField] private GameObject gameOverPanel;

    private int coinCount = 0;

    void Start()
    {
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
        gameOverPanel.SetActive(true);
    }

    // Method ini akan dipanggil oleh tombol Restart
    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}