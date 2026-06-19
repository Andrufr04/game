using UnityEngine;
using UnityEngine.UI;

public class EnergyManager : MonoBehaviour
{
    public Image energyBar;
    public float energy = 1f;
    public float decayRate = 0.05f;

    [Header("Настройки цвета")]
    public Color fullColor = Color.green; // Цвет при полной шкале
    public Color emptyColor = Color.red;  // Цвет при пустой шкале

    public PlayerMovement playerMovement;
    public float minSpeed = 2f;
    public float maxSpeed = 10f;

    void Update()
    {
        if (energyBar == null) return;

        energy -= decayRate * Time.deltaTime;
        energy = Mathf.Clamp01(energy);

        energyBar.fillAmount = energy;

        // МАГИЯ ЦВЕТА:
        // Color.Lerp смешивает цвета: 0 - красный, 1 - зеленый
        energyBar.color = Color.Lerp(emptyColor, fullColor, energy);

        playerMovement.speed = Mathf.Lerp(minSpeed, maxSpeed, energy);
    
    }

    // Метод для пополнения (вызывай его при сборе сэндвича)
    public void AddEnergy(float amount)
    {
        energy += amount;
        energy = Mathf.Clamp01(energy);
    }
}