using UnityEngine;
using UnityEngine.Rendering;

public class DebrisProjectileState : DebrisBaseState
{
    public override void EnterState(DebrisStateManager debris)
    {
        debris.transform.parent = null;
        debris.player.projectile = null;
        
        // send forward when angle = [-40,40]
        if (debris.aimIndicator.angle>=-40 && debris.aimIndicator.angle<=40)
        {
            // orient this to face whereever the player is pointed at
            RaycastHit hit;
            if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, Mathf.Infinity))
            {
                debris.transform.LookAt(hit.point);
            }
        }
        // or send backward when [140-220]
        else if (debris.aimIndicator.angle>=140 && debris.aimIndicator.angle<=220)
        {
            debris.transform.rotation = Quaternion.Euler(0,180,0);
        }
    }
    
    public override void UpdateState(DebrisStateManager debris)
    {
        debris.transform.position += debris.transform.forward * 100 * Time.deltaTime;
    }

    public override void OnTriggerEnter(DebrisStateManager debris, Collider other)
    {
        if (other.CompareTag("NPC_Tempest"))
        {
            TempestMain tempest = other.GetComponent<TempestMain>();
            tempest.ModifyStability(-debris.stabilityDamage);
            tempest.ChangeSize(1 - debris.sizeDamage/100, "scale");
            EventBus.Instance.DoDamage(debris.player.gameObject);
            // tempest.size *= 1 - debris.sizeDamage/100; 
            // tempest.Stability -= debris.stabilityDamage;
        }
    }
}