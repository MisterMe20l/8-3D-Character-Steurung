using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraLook : MonoBehaviour
{
    GameInput gameInput;

    [Range(1f, 10f)] public float mouseSensitivity = 1f;
    [Range(5f, 15f)] public float lerpIntencity = 1f;
    InputAction look;
    InputAction cursor;

    Vector3 eulerAngle;
    Vector3 lerpedEulerAngle;

    void Awake()
    {
        gameInput = new GameInput();
    }

    void OnEnable()
    {
        look = gameInput.Player.Look;
        cursor = gameInput.Player.Cursor;

        look.Enable();
        cursor.Enable();

        cursor.performed += OnCursorInput;
    }

    private void OnCursorInput(InputAction.CallbackContext context)
    {
        Cursor.lockState = (Cursor.lockState != CursorLockMode.Locked) ? CursorLockMode.Locked : CursorLockMode.None;
    }

    private void OnDisable()
    {
        look.Disable();
        cursor.Disable();
    }

    void Update()
    {
        if (Cursor.lockState == CursorLockMode.Locked)
        {
            eulerAngle.x += look.ReadValue<Vector2>().y * mouseSensitivity * 0.01f;
            eulerAngle.y += look.ReadValue<Vector2>().x * mouseSensitivity * 0.01f;

            eulerAngle.x = Mathf.Clamp(eulerAngle.x, -90f, 90f);
        }

        lerpedEulerAngle.x = Mathf.Lerp(lerpedEulerAngle.x, eulerAngle.x, lerpIntencity * Time.deltaTime);
        lerpedEulerAngle.y = Mathf.Lerp(lerpedEulerAngle.y, eulerAngle.y, lerpIntencity * Time.deltaTime);

        transform.eulerAngles = lerpedEulerAngle;
    }
}
