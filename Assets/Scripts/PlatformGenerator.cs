using UnityEngine;
using System.Collections.Generic;

public class PlatformGenerator : MonoBehaviour
{
    public GameObject tilePrefab;
    public int width = 25;
    public int length = 25;
    public float tileSize = 2f;

    public Material[] tileMaterials;

    // Теперь мы храним не просто MeshRenderer, а целиком объекты GameObject
    private List<GameObject> allTiles = new List<GameObject>();

    void Start()
    {
        GeneratePlatform();
    }

    void GeneratePlatform()
    {
        float startX = -(width * tileSize) / 2f + (tileSize / 2f);
        float startZ = -(length * tileSize) / 2f + (tileSize / 2f);

        for (int x = 0; x < width; x++)
        {
            for (int z = 0; z < length; z++)
            {
                float posX = startX + (x * tileSize);
                float posZ = startZ + (z * tileSize);

                Vector3 pos = new Vector3(posX, 0, posZ);

                GameObject newTile = Instantiate(tilePrefab, pos, Quaternion.identity, transform);

                if (tileMaterials != null && tileMaterials.Length > 0)
                {
                    int randomIndex = Random.Range(0, tileMaterials.Length);
                    // Используем sharedMaterial для точного сравнения
                    newTile.GetComponent<MeshRenderer>().sharedMaterial = tileMaterials[randomIndex];
                }

                allTiles.Add(newTile);
            }
        }
    }

    public void RepaintTiles()
    {
        if (tileMaterials == null || tileMaterials.Length == 0) return;

        foreach (GameObject tile in allTiles)
        {
            int randomIndex = Random.Range(0, tileMaterials.Length);
            tile.GetComponent<MeshRenderer>().sharedMaterial = tileMaterials[randomIndex];
        }
    }

    // НОВЫЙ МЕТОД: Прячем все плитки, кроме правильных
    public void HideWrongTiles(Material correctMaterial)
    {
        foreach (GameObject tile in allTiles)
        {
            MeshRenderer renderer = tile.GetComponent<MeshRenderer>();

            // Если материал плитки НЕ совпадает с загаданным
            if (renderer.sharedMaterial != correctMaterial)
            {
                renderer.enabled = false; // Отключаем видимость
                tile.GetComponent<Collider>().enabled = false; // Отключаем столкновение (проваливаемся)
            }
        }
    }

    // НОВЫЙ МЕТОД: Возвращаем все плитки обратно
    public void ShowAllTiles()
    {
        foreach (GameObject tile in allTiles)
        {
            tile.GetComponent<MeshRenderer>().enabled = true;
            tile.GetComponent<Collider>().enabled = true;
        }
    }
}