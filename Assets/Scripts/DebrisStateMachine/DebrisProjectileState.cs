using UnityEditor.UI;
using UnityEngine;

public class DebrisProjectileState : DebrisBaseState
{
    private float timer;
    private float timeBeforeIdle = 5f;
    public override void EnterState(DebrisStateManager debris)
    {
        debris.player.projectile = null;
        
        // send forward when angle = [-40,40]
        if (debris.aimIndicator.angle>=-40 && debris.aimIndicator.angle<=40)
        {
            // orient this to face whereever the player is pointed at
            //RaycastHit hit;
            //if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, Mathf.Infinity))
            //{
            //    debris.transform.LookAt(hit.point);
            //}
            debris.transform.localRotation = Camera.main.transform.rotation;
        }
        // or send backward when [140-220]
        else if (debris.aimIndicator.angle>=140 && debris.aimIndicator.angle<=220)
        {
            debris.transform.rotation = Quaternion.Euler(0,180,0);
        }
        timer = 0f;
        debris.GetComponent<Rigidbody>().useGravity = true;
        debris.GetComponent<Rigidbody>().AddForce(debris.transform.forward.normalized * 100f, ForceMode.VelocityChange);

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
            // tempest.size *= 1 - debris.sizeDamage/100; 
            // tempest.Stability -= debris.stabilityDamage;
        }
    }
}