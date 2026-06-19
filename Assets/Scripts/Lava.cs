using UnityEngine;
using UnityEngine.SceneManagement;

public class Lava : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Игрок упал в лаву!");

            // 1. Находим скрипт меню на сцене
            PauseManager menu = FindObjectOfType<PauseManager>();

            // 2. Вызываем наш новый метод показа проигрыша
            if (menu != null)
            {
                menu.ShowLoseScreen();
            }
            else
            {
                // Если меню почему-то не найдено, просто перезагружаем по-старому
                RestartLevel();
            }
        }
    }

    private void RestartLevel()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.name);
    }
}