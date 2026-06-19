using UnityEngine;
using UnityEngine.SceneManagement; // Обязательно для переключения сцен

public class MenuManager : MonoBehaviour
{
    // Этот метод мы привяжем к нашей кнопке
    public void StartGame()
    {
        // ВАЖНО: Впиши сюда точное название твоей сцены с игрой!
        SceneManager.LoadScene("GameScene");
    }

    public void QuitGame()
    {
    #if UNITY_STANDALONE
            Application.Quit();
    #endif
    #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
    #endif
    }

}