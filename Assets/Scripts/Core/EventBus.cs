using System;
using UnityEngine;

public class EventBus {
    static EventBus _theInstance;

    public static EventBus Instance {
        get { return _theInstance ??= new EventBus(); }
    }

    public enum DamageType
    {
        PROJECTILE,
        PASSIVE
    }

    public Action<GameObject, DamageType> OnDamage; 
    public void DoDamage(GameObject target, DamageType type)
    {
        OnDamage?.Invoke(target, type);
    }

    public Action<bool> OnAttack;
    public void DoAttack(bool dummy)
    {
        OnAttack?.Invoke(dummy);
    }

}