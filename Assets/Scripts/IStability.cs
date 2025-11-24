using UnityEngine;

public interface IStability
{
    float Stability{ get; set; }
    void ModifyStability(float amount);
}
