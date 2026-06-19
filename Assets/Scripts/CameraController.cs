using UnityEngine;
using UnityEngine.InputSystem;

public class CameraController : MonoBehaviour
{
    public Transform target;
    public float distance = 6f;
    public float sensitivity = 0.3f;

    private float currentX = 0f;
    private float currentY = 20f;

    void LateUpdate()
    {
        if (target == null) return;

        // Проверяем, зажата ли ПРАВАЯ или ЛЕВАЯ кнопка мыши
        if (Mouse.current.rightButton.isPressed || Mouse.current.leftButton.isPressed)
        {
            // Считываем движение мыши
            Vector2 mouseDelta = Mouse.current.delta.ReadValue();
            currentX += mouseDelta.x * sensitivity;
            currentY -= mouseDelta.y * sensitivity;

            // Ограничиваем наклон камеры (от 5 до 80 градусов)
            currentY = Mathf.Clamp(currentY, 5f, 80f);

            // Поворачиваем самого персонажа ТОЛЬКО если зажата ПРАВАЯ кнопка
            if (Mouse.current.rightButton.isPressed)
            {
                target.rotation = Quaternion.Euler(0, currentX, 0);
            }
        }

        // Высчитываем новую позицию камеры вокруг игрока (выполняется всегда)
        Vector3 dir = new Vector3(0, 0, -distance);
        Quaternion rotation = Quaternion.Euler(currentY, currentX, 0);

        Vector3 lookPosition = target.position + new Vector3(0, 1f, 0);

        transform.position = lookPosition + rotation * dir;
        transform.LookAt(lookPosition);
    }
}