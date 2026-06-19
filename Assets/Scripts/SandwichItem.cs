using UnityEngine;

public class SandwichItem : MonoBehaviour
{
    [Header("Настройки анимации")]
    public float rotationSpeed = 100f; // Скорость вращения

    // Позволяет выбрать ось вращения в Инспекторе (по умолчанию Y)
    public Vector3 rotationAxis = new Vector3(0, 1, 0);

    [Header("Границы поля для появления")]
    public float minX = -20f;
    public float maxX = 20f;
    public float minZ = -20f;
    public float maxZ = 20f;
    public float spawnY = 1f;

    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        Respawn();
    }

    void Update()
    {
        // ВАЖНО: Добавили Space.Self, чтобы вращение шло вокруг собственной оси модели
        transform.Rotate(rotationAxis * rotationSpeed * Time.deltaTime, Space.Self);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (audioSource != null)
            {
                audioSource.Play();
            }
            // Ищем скрипт EnergyManager на персонаже и пополняем энергию
            EnergyManager energyManager = other.GetComponent<EnergyManager>();
            if (energyManager != null)
            {
                energyManager.AddEnergy(0.5f); // Добавляем 50% энергии
            }

            FindObjectOfType<GameManager>().AddSandwich();

            Respawn(); // Телепортируем сэндвич
        }
    }

    void Respawn()
    {
        float randomX = Random.Range(minX, maxX);
        float randomZ = Random.Range(minZ, maxZ);
        transform.position = new Vector3(randomX, spawnY, randomZ);
    }
}