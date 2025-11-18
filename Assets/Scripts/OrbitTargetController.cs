using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;

public class OrbitTargetController : MonoBehaviour
{
    // right now OrbitTarget keeps the same rotation at every point of orbit
    // make it so it rotates in the direction its moving

    public Transform target;
    public float spinSpeed = 10;

    // Update is called once per frame
    void Update()
    {
        // translate
        transform.RotateAround(target.position, Vector3.up, spinSpeed * Time.deltaTime);
        // rotate
        float futureAngle = Time.deltaTime;
        Vector3 displacement = transform.position - transform.parent.position;
        Quaternion futureRotation = Quaternion.AngleAxis(spinSpeed * futureAngle, Vector3.up);
        Vector3 futureDisplacement = futureRotation * displacement;

        transform.LookAt(transform.parent.position + futureDisplacement);
    }
}
