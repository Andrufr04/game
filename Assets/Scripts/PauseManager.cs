using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using TMPro;

public class PauseManager : MonoBehaviour
{
    public GameObject pauseMenuUI;
    private bool isPaused = false;
    private bool isGameOver = false; // Отслеживает проигрыш

    public TextMeshProUGUI statusText;
    public AudioSource audioSource;
    public AudioClip loseSound;
    public GameObject pausePanel;

    // Сюда перетяни ТЕКСТ, который находится внутри кнопки Continue
    public TextMeshProUGUI continueButtonText;
    public AudioSource bgMusic;

    void Start()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        isGameOver = false;
    }

    void Update()
    {
        if (Keyboard.current != null && Keyboard.current.escapeKey.wasPressedThisFrame)
        {
            // Если проиграли, блокируем снятие с паузы через Escape
            if (isGameOver) return;

            if (isPaused) Resume();
            else Pause();
        }
    }

    // ЭТОТ МЕТОД НУЖНО ПОВЕСИТЬ НА КНОПКУ CONTINUE
    public void OnContinueButtonPressed()
    {
        if (isGameOver)
        {
            RestartGame(); // Если мертвы -> рестарт
        }
        else
        {
            Resume(); // Если живы -> продолжаем
        }
    }

    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
    }

    void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;
    }

    public void LoadMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }

    public void ShowLoseScreen()
    {
        isGameOver = true; // Отмечаем проигрыш

        pausePanel.SetActive(true);
        statusText.text = "LOOSE!";

        // Меняем текст на кнопке
        if (continueButtonText != null)
        {
            continueButtonText.text = "Restart";
        }

        if (bgMusic != null) bgMusic.Stop();

        if (audioSource != null && loseSound != null)
        {
            audioSource.clip = loseSound;
            audioSource.Play();
        }

        Time.timeScale = 0f;
    }

    public void RestartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}