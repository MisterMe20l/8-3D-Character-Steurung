using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInteraction : MonoBehaviour
{
    [SerializeField] GameInput gameInput;
    Transform mainCameraTransform;
    InputAction interact;
    public Renderer crosshairRenderer;

    void Awake()
    {
        gameInput = new GameInput();
    }

    void OnEnable()
    {
        interact = gameInput.Player.Interact;

        interact.Enable();
    }

    private void OnDisable()
    {
        interact.Disable();
    }
    void Start()
    {
        mainCameraTransform = Camera.main.transform;
    }

    void FixedUpdate()
    {
        RaycastHit hit;

        Physics.Raycast(mainCameraTransform.position, mainCameraTransform.forward, out hit, 1.5f);

        if (hit.transform && hit.transform.GetComponent<IInteractable>() != null)
        {
            crosshairRenderer.material.color = Color.red;

            if (interact.WasPressedThisFrame()) 
            {
                hit.transform.GetComponent<IInteractable>().Interaction();
            }
        }
        else
        {
            crosshairRenderer.material.color = Color.black;
        }
    }
}
