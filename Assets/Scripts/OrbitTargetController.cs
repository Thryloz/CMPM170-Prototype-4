using UnityEngine;

public class OrbitTargetController : MonoBehaviour
{
    public Transform target;
    public float spinSpeed = 10;

    // Update is called once per frame
    void Update()
    {
        transform.RotateAround(target.position, Vector3.up, spinSpeed * Time.deltaTime);
    }
}
