using UnityEngine;
using UnityEngine.InputSystem;

public class TempestController : MonoBehaviour
{
    [SerializeField] private float maxSpeed = 5f;
    [SerializeField] private float acceleration = 2f;
    [SerializeField] private Camera cam;

    [Header("Debug")]
    [SerializeField] private Vector3 velocity;
    [SerializeField] private float velocityMagnitude;

    private PlayerControls controls;
    private InputAction move;


    private Rigidbody rb;


    private Vector3 forceDirection = Vector3.zero;
    private Vector3 forward = Vector3.zero;
    private Vector3 right = Vector3.zero;
    private Vector3 horizontalVelocity = Vector3.zero;

    private void Awake()
    {
        controls = new PlayerControls();
        rb = GetComponent<Rigidbody>();

    }

    private void OnEnable()
    {
        move = controls.Player.Move;

        controls.Player.Enable();
    }

    private void OnDisable()
    {
        controls.Player.Disable();
    }


    private void Update()
    {
    }

    private void FixedUpdate()
    {
        forward = cam.transform.forward;
        forward.y = 0f;

        right = cam.transform.right;
        right.y = 0f;

        forceDirection = forward.normalized * move.ReadValue<Vector2>().y + right.normalized * move.ReadValue<Vector2>().x;

        rb.AddForce(forceDirection * acceleration, ForceMode.VelocityChange);
        forceDirection = Vector3.zero;

        horizontalVelocity = rb.linearVelocity;
        if (horizontalVelocity.sqrMagnitude > maxSpeed * maxSpeed)
        {
            rb.linearVelocity = horizontalVelocity.normalized * maxSpeed; 
        }

        velocity = rb.linearVelocity;
        velocityMagnitude = rb.linearVelocity.magnitude;
    }

}
