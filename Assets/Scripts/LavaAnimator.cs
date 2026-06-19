using UnityEngine;

public class LavaAnimator : MonoBehaviour
{
    public float speed = 1f;       // Скорость течения лавы
    public float amount = 0.05f;   // Насколько сильно сдвигается текстура

    private Material lavaMat;

    void Start()
    {
        // Берем материал с объекта лавы
        lavaMat = GetComponent<MeshRenderer>().material;
    }

    void Update()
    {
        // Математическая функция Sin плавно меняет числа туда-сюда со временем
        float offset = Mathf.Sin(Time.time * speed) * amount;

        // Применяем сдвиг к текстуре и к свечению
        lavaMat.mainTextureOffset = new Vector2(offset, offset);
    }
}