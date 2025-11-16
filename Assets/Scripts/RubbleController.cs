using Unity.VisualScripting;
using UnityEngine;

/*
rubble lies on ground (idle)

player is sucking && colliding with player --> rubble chase an orbiting transform

when close enough to orbiting transform, apply a dynamic graviational force to keep
this orbiting (but no longer chasing)

when space is released stop applying gravitational force but keep other horizontal forces

if collided with enemy then lower enemy stability
if collided with building then do something with building (healthbar or realtime destruction)
*/


public class RubbleController : MonoBehaviour
{
    TempestController player;
    Transform target;

    void Start()
    {
        player = GameManager.Instance.player.GetComponent<TempestController>();
        target = player.transform.Find("OrbitTarget");
    }


    void OnTriggerStay(Collider other)
    {
        // check if in orbit
        if (other.gameObject.TryGetComponent<OrbitTargetController>(out OrbitTargetController hasTarget))
        {
            if (player.projectile)
            {
                player.projectile.gameObject.transform.localScale += Vector3.one;
                Destroy(gameObject);
            }
            else
            {
                player.CreateProjectile();
            } 
        }
        // move to orbit
        if (other.gameObject.CompareTag("Player"))
        {
            if (player.isSucking)
            {
                ChaseTarget();
            }
        }
    }


    void ChaseTarget()
    {
        float suckSpeed = player.suckSpeed;

        // look at OrbitTarget
        Vector3 dir = (target.position - transform.position).normalized;
        if (dir != Vector3.zero)
        {
            Quaternion rot = Quaternion.LookRotation(dir);
            transform.rotation = Quaternion.Slerp(transform.rotation, rot, Time.deltaTime * suckSpeed);
        }

        // move to OrbitTarget
        transform.position += transform.forward * suckSpeed * Time.deltaTime;
    }
}
