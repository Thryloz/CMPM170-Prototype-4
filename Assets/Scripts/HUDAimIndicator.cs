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

    void Start()
    {
        orbitTargetTransform = GameManager.Instance.player.transform.Find("OrbitTarget");
        _player = GameManager.Instance.player.GetComponent<TempestController>();
        selfTransform = GetComponent<RectTransform>();
    }

    void Update()
    {
        // angle of orbiting debris relative to player
        Vector3 orbitingDirection = orbitTargetTransform.localPosition;
        float orbitingAngle = Mathf.Atan2(orbitingDirection.z, orbitingDirection.x) * Mathf.Rad2Deg;
        Debug.Log($"[HUD] orbit target angle: {orbitingAngle}");

        // angle of player camera orientation relative to player
        Vector3 aimDirection = (cam.position - _player.transform.position).normalized;
        float aimAngle = Mathf.Atan2(aimDirection.z, aimDirection.x) * Mathf.Rad2Deg;
        Debug.Log($"[HUD] cam aim angle: {orbitingAngle}");

        // angle of debris relative to cam orientation
        selfTransform.rotation = Quaternion.Euler(0,0,orbitingAngle - aimAngle + 90);

        
    }
}
