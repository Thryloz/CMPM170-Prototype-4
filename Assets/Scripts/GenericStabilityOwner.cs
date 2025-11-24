using UnityEngine;

public class GenericStabilityOwner : MonoBehaviour, IStability
{
    public float stability = 10f;

    public float Stability
    {
        get => stability;
        set => stability = Mathf.Clamp(value, 0 , 10);
    }

    public void ModifyStability(float amount)
    {
        Stability += amount;

        if (Stability <= 0)
        {

            //(This is where we would put the dissipate logic)

            //(This next part is what we would put on the player and npc tempest to reduce stability)
            //IStability stb = hit.collider.GetComponent<IStability>();
            //if (stb != null)
            //{
                //stb.ModifyStability(-10f);
            //}
        }
    }
}
