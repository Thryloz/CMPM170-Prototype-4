using System;
using UnityEngine;

public class EventBus {
    static EventBus _theInstance;

    public static EventBus Instance {
        get { return _theInstance ??= new EventBus(); }
    }

    // should prob use this elsewhere but only goes to hitmarkers for now
    public Action<GameObject> OnDamage; 
    public void DoDamage(GameObject target)
    {
        OnDamage?.Invoke(target);
    }
}