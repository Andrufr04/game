using UnityEngine;

public class TestTrigger : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        Debug.Log("ВНИМАНИЕ: Триггер сработал! Кто коснулся: " + other.name);
    }
}