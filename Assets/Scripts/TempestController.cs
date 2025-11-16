using UnityEngine;
using UnityEngine.InputSystem;

public class TempestController : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Camera cam;
    [SerializeField] private TempestMain tempestMain;
    [SerializeField] private GameObject projectilePrefab;

    [Header("Suck & Throw Fields")]
    public bool isSucking = false;
    public float suckSpeed = 5;
    public GameObject projectile;

    [Header("Debug")]
    [SerializeField] private Vector3 velocity;
    [SerializeField] private float velocityMagnitude;

    private PlayerControls controls;
    private InputAction move;
    private InputAction suck;

    private Rigidbody rb;

    private Vector3 forceDirection = Vector3.zero;
    private Vector3 forward = Vector3.zero;
    private Vector3 right = Vector3.zero;
    private Vector3 horizontalVelocity = Vector3.zero;


    private void Awake()
    {
        GameManager.Instance.player = gameObject;

        controls = new PlayerControls();
        rb = GetComponent<Rigidbody>();
        if (tempestMain == null)
        {
            tempestMain = GetComponent<TempestMain>();
        }
    }


    private void OnEnable()
    {
        move = controls.Player.Move;
        suck = controls.Player.Suck;

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
        isSucking = suck.ReadValue<float>() != 0;

        forward = cam.transform.forward;
        forward.y = 0f;

        right = cam.transform.right;
        right.y = 0f;

        forceDirection = forward.normalized * move.ReadValue<Vector2>().y + right.normalized * move.ReadValue<Vector2>().x;

        rb.AddForce(forceDirection * tempestMain.acceleration, ForceMode.VelocityChange);
        forceDirection = Vector3.zero;

        horizontalVelocity = rb.linearVelocity;
        if (horizontalVelocity.sqrMagnitude > tempestMain.maxSpeed * tempestMain.maxSpeed)
        {
            rb.linearVelocity = horizontalVelocity.normalized * tempestMain.maxSpeed; 
        }

        velocity = rb.linearVelocity;
        velocityMagnitude = rb.linearVelocity.magnitude;
    }

    public void CreateProjectile()
    {
        projectile = Instantiate(projectilePrefab);
        projectile.transform.parent = transform.Find("OrbitTarget");
        projectile.transform.localPosition = Vector3.zero;
        projectile.transform.localScale = Vector3.one;
    }
}
