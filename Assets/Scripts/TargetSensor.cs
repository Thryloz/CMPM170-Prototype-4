using System;
using UnityEngine;

/*
make targets a sorted array
if a new target enters, place it relative to self based on size in the array
when a target exits remove it

also when it dies remove 
*/

public class TargetSensor : MonoBehaviour
{
    private SphereCollider col;
    public Action<GameObject> OnTargetEnter;
    public Action<GameObject> OnTargetExit;
    public float range;

    void Start()
    {
        col = GetComponent<SphereCollider>();
        range = col.radius;
    }

    void OnTriggerEnter(Collider other)
    {
        OnTargetEnter?.Invoke(other.gameObject);
    }

    void OnTriggerExit(Collider other)
    {
        OnTargetExit?.Invoke(other.gameObject);
    }
}
