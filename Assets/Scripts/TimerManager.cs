using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections;

public class GameManager : MonoBehaviour
{
    public TextMeshProUGUI timerText;
    public Image colorIndicator;
    public PlatformGenerator platformGen;
    public TextMeshProUGUI countdownText;

    public float roundTime = 5f;
    private float currentTime;
    private Material targetMaterial;

    private bool isGameActive = false;
    private int currentRound = 1;

    public TextMeshProUGUI roundsDisplay; // Перетащишь сюда текст из UI

    public TextMeshProUGUI sandwichCounterText; // Перетащишь сюда текст из иерархии
    private int sandwichesEaten = 0;

    // Метод, который мы будем вызывать при поглощении
    public void AddSandwich()
    {
        sandwichesEaten++;
        sandwichCounterText.text = sandwichesEaten.ToString();
    }

    void Start()
    {
        timerText.gameObject.SetActive(false);
        colorIndicator.gameObject.SetActive(false);

        StartCoroutine(StartCountdown());
    }

    IEnumerator StartCountdown()
    {
        countdownText.gameObject.SetActive(true);

        for (int i = 5; i > 0; i--)
        {
            countdownText.text = i.ToString() + "...";
            yield return new WaitForSeconds(1f);
        }

        countdownText.text = "START!";
        yield return new WaitForSeconds(1f);

        StartCoroutine(ShowRoundTransition());
    }

    IEnumerator ShowRoundTransition()
    {
        isGameActive = false;

        timerText.gameObject.SetActive(false);
        colorIndicator.gameObject.SetActive(false);

        countdownText.gameObject.SetActive(true);
        countdownText.text = "Round " + currentRound;

        if (roundsDisplay != null)
        {
            roundsDisplay.text = "Score: " + currentRound;
        }

        yield return new WaitForSeconds(1.5f);

        countdownText.gameObject.SetActive(false);
        timerText.gameObject.SetActive(true);
        colorIndicator.gameObject.SetActive(true);

        isGameActive = true;
        StartNewRound();
    }

    void StartNewRound()
    {
        currentTime = roundTime;

        // Восстанавливаем плитки, если они были спрятаны
        platformGen.ShowAllTiles();
        platformGen.RepaintTiles();

        Material[] materials = platformGen.tileMaterials;
        targetMaterial = materials[Random.Range(0, materials.Length)];
        colorIndicator.color = targetMaterial.color;
    }

    void Update()
    {
        if (!isGameActive) return;

        if (currentTime > 0)
        {
            currentTime -= Time.deltaTime;
            timerText.text = Mathf.Ceil(currentTime).ToString();
        }
        else
        {
            timerText.text = "0";
            isGameActive = false; // Ставим игру на паузу, чтобы таймер не шел в минус

            // Запускаем процесс окончания раунда (падение)
            StartCoroutine(HandleRoundEnd());
        }
    }

    // НОВЫЙ БЛОК: Падение и ожидание 3 секунды
    IEnumerator HandleRoundEnd()
    {
        // 1. Убираем неправильные цвета
        platformGen.HideWrongTiles(targetMaterial);

        // 2. Ждем 3 секунды. Если игрок стоял не там - он падает в лаву, и сцена перезагружается сама
        yield return new WaitForSeconds(3f);

        // 3. Если игрок выжил: возвращаем пол
        platformGen.ShowAllTiles();

        // 4. Запускаем переход к следующему раунду
        currentRound++;
        StartCoroutine(ShowRoundTransition());
    }
}