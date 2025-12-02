using System;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;

public class ThirdPersonCameraController : MonoBehaviour
{
    public float zoomSpeed = 2f;
    public float zoomLerpSpeed = 10f;
    public float minDistance = 3f;
    public float maxDistance = 15f;

    private PlayerControls controls;

    private CinemachineCamera cam;
    private CinemachineOrbitalFollow orbital;
    private Vector2 scrollDelta;

    private float targetZoom;

    [Header("Debug")]
    [SerializeField] private float currentZoom;



    private void Awake()
    {
        cam = GetComponent<CinemachineCamera>();
        orbital = cam.GetComponent<CinemachineOrbitalFollow>();
    }
    private void Start()
    {
        controls = new PlayerControls();
        controls.Enable();
        controls.Player.MouseZoom.performed += HandleMouseScroll;
        
        Cursor.lockState = CursorLockMode.Locked;

        targetZoom = currentZoom = orbital.Radius;
        Time.timeScale = 1f;
    }

    private void HandleMouseScroll(InputAction.CallbackContext context)
    {
        scrollDelta = context.ReadValue<Vector2>();
    }

    private void Update()
    {
        if (scrollDelta.y != 0)
        {
            if (orbital != null)
            {
                targetZoom = Mathf.Clamp(orbital.Radius - scrollDelta.y * zoomSpeed, minDistance, maxDistance);
                scrollDelta = Vector2.zero;
            }
        }

        if (targetZoom < minDistance || targetZoom > maxDistance)
        {
            targetZoom = Mathf.Clamp(currentZoom, minDistance, maxDistance);
        }

        maxDistance = minDistance + 15f;

        currentZoom = Mathf.Lerp(currentZoom, targetZoom, Time.deltaTime * zoomLerpSpeed);
        orbital.Radius = currentZoom;
    }
}
