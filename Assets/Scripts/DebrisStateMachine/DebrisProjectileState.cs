using UnityEngine;

public class DebrisProjectileState : DebrisBaseState
{
    private float timer;
    private float timeBeforeIdle = 5f;
    public override void EnterState(DebrisStateManager debris)
    {
        debris.player.projectile = null;
        GameObject indicatorPivot = GameObject.Find("IndicatorPivot");
        HUDAimIndicator aimIndicator = indicatorPivot.GetComponent<HUDAimIndicator>();


        // send forward when angle = [-40,40]
        if (aimIndicator.angle>=-40 && aimIndicator.angle<=40)
        {
            // orient this to face whereever the player is pointed at
            //RaycastHit hit;
            Vector3 targetAngle = new Vector3(Camera.main.transform.forward.x, 0, Camera.main.transform.forward.z);
            debris.rb.AddForce(targetAngle.normalized * 100f, ForceMode.VelocityChange);
            // if (Physics.Raycast(Camera.main.transform.position, targetAngle, out hit, Mathf.Infinity))
            // {
            //    debris.transform.LookAt(hit.point);
            // }
            // debris.transform.localRotation = Camera.main.transform.rotation;
        }
        // or send backward when [140-220]
        else if (aimIndicator.angle>=140 && aimIndicator.angle<=220)
        {
            debris.transform.rotation = Quaternion.Euler(0,180,0);
            debris.rb.AddForce(debris.transform.forward.normalized * 100f, ForceMode.VelocityChange);

        }
        else
        {
            debris.rb.AddForce(debris.transform.forward.normalized * 100f, ForceMode.VelocityChange);
        }
        timer = 0f;
        debris.rb.useGravity = true;
    }

    public override void UpdateState(DebrisStateManager debris)
    {
        
        timer += Time.deltaTime;
        if (timer > timeBeforeIdle)
        {
            debris.SwitchState(debris.idleState);
        }
    }

    public override void OnTriggerEnter(DebrisStateManager debris, Collider other)
    {
        if (other.CompareTag("NPC_Tempest"))
        {
            TempestMain tempest = other.GetComponent<TempestMain>();
            tempest.ModifyStability(-debris.stabilityDamage);
            tempest.ChangeSize(-tempest.size * (debris.sizePercentDamage / 100f));
            EventBus.Instance.DoDamage(other.gameObject, EventBus.DamageType.PROJECTILE);
            debris.DestroySelf();
        }
    }
}