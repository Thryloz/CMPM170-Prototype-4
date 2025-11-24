using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/*
make targets a sorted array
if a new target enters, place it relative to self based on size in the array
when a target exits remove it

also when it dies remove 
*/

public class FindTargets : MonoBehaviour
{
    public List<GameObject> targets;
    public GameObject prey;
    public List<GameObject> predators;

    [SerializeField] private float detectionRange = 40;
    private SphereCollider sc;
    private GameObject _self;


    void Start()
    {
        sc = GetComponent<SphereCollider>();
        _self = transform.parent.gameObject;
        // EventBus.Instance.OnNPCDeath += KillTarget;
    }

    // for testing purposes
    void Update()
    {
        sc.radius = detectionRange;
        UpdateTargets();

    }

    void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<TempestMain>(out TempestMain component))
        {
            targets.Add(other.gameObject);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent<TempestMain>(out TempestMain component))
        {
            targets.Remove(other.gameObject);
            predators.Remove(other.gameObject);
            if (prey == other.gameObject)
                prey = null;
        }
    }

    public void UpdateTargets()
    {
        if (targets.Count == 0)
            return;

        float selfSize = _self.GetComponent<TempestMain>().size;
        GameObject smallest = _self;
        foreach (GameObject target in targets.ToList())
        {
            if (target == null)
            {
                targets.Remove(target);
                continue;
            }
                

            float targetSize = target.GetComponent<TempestMain>().size;
            if (targetSize >= selfSize)
            {
                if (!predators.Find(o => o == target))
                    predators.Add(target);
            }
            else if (targetSize < smallest.GetComponent<TempestMain>().size)
            {
                smallest = target;
            }
        }

        if (smallest == _self)
            return;

        prey = smallest;
    }
}
