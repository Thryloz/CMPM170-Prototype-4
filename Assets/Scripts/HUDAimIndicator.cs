using UnityEngine;
using UnityEngine.InputSystem;

/* TODO:
DONE - make indicator rotate 360

raycast from crosshair to world
use that to get player's forward rotation
make the indicator incorporate forward rotation

*/

public class HUDAimIndicator : MonoBehaviour
{
    Transform orbitTargetTransform; // the invisible thing rotating around player
    RectTransform selfTransform; // pivot point for the aim indicator (the attached obj)
    TempestController _player;
    [SerializeField] private Transform cam; // attach in inspector
    public float angle;

    private void Awake()
    {
        cam = Camera.main.transform;
    }

    void Start()
    {
        GameManager.Instance.aimIndicator = this;
        orbitTargetTransform = GameManager.Instance.player.transform.Find("OrbitTarget");
        _player = GameManager.Instance.player.GetComponent<TempestController>();
        selfTransform = GetComponent<RectTransform>();
    }

    void Update()
    {
        // angle of orbiting debris relative to player
        Vector3 orbitingDirection = orbitTargetTransform.localPosition;
        float orbitingAngle = Mathf.Atan2(orbitingDirection.z, orbitingDirection.x) * Mathf.Rad2Deg;

        // angle of player camera orientation relative to player
        Vector3 aimDirection = (cam.position - _player.transform.position).normalized;
        float aimAngle = Mathf.Atan2(aimDirection.z, aimDirection.x) * Mathf.Rad2Deg;

        // angle of debris relative to cam orientation
        angle = orbitingAngle - aimAngle + 90;
        selfTransform.rotation = Quaternion.Euler(0,0,angle);

        
    }
}
