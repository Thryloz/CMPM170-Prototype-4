using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

public class NPC_Tempest : MonoBehaviour
{
    [SerializeField] private TempestMain selfTempest;
    [SerializeField] private GameObject player;
    [SerializeField] private AbsorbRange absorbRange;

    [SerializeField] private TargetSensor targetSensor;
    [SerializeField] private NavMeshAgent agent;

    // sorted by closest to farthest
    [SerializeField] private List<GameObject> predators;
    [SerializeField] private List<GameObject> prey;


    private TempestMain playerTempest;

    private void Awake()
    {
        player = GameObject.Find("Player_Tempest");
        playerTempest = player.GetComponent<TempestMain>();
        selfTempest = GetComponent<TempestMain>();
    }

    private void Start()
    {
        selfTempest.size = Random.Range(selfTempest.level1Threshold, selfTempest.level2Threshold);
        predators = new List<GameObject>();
        prey = new List<GameObject>();

        targetSensor = GetComponentInChildren<TargetSensor>();
        targetSensor.OnTargetEnter += HandleTempestEntered;
        targetSensor.OnTargetExit += HandleTempestExited;
        selfTempest.OnSizeChange += HandleSelfGrowth;
        selfTempest.OnAbsorbed += HandleSelfDeath;

        agent.SetDestination(RandomPoint(targetSensor.range));
    }

    private void Update()
    {
        if (player != null)
        {
            if (playerTempest.size > selfTempest.size)
            {
                selfTempest.coreColor = selfTempest.absorbableEnemyColor;
            }
            else
            {
                selfTempest.coreColor = selfTempest.unabsorbableEnemyColor;
            }
        }

        foreach (GameObject predator in predators.ToList())
        {
            if (predator == null)
                predators.Remove(predator);
        }
        foreach (GameObject _prey in prey.ToList())
        {
            if (_prey == null)
                prey.Remove(_prey);
        }

        // MOVEMENT
        /* https://www.youtube.com/watch?v=7EALNQ9tFlw */
        agent.speed = selfTempest.maxSpeed;

        // chase nearby prey otherwise wander
        if (prey.Count == 0)
        {
            if (agent.remainingDistance <= agent.stoppingDistance)
                agent.SetDestination(RandomPoint(targetSensor.range));
            return;
        }

        agent.SetDestination(prey[0].transform.position);

        // continue chase / wander but avoid predators
        if (predators.Count == 0)
            return;

        Vector3 dirToClosestPredator = predators[0].transform.position - transform.position;
        agent.velocity = Vector3.Lerp(
            a: agent.desiredVelocity,
            b: -dirToClosestPredator.normalized * agent.speed * 1.5f,
            t: Mathf.Clamp01(targetSensor.range - dirToClosestPredator.magnitude
                / selfTempest.size * 3)
        );
    }

    Vector3 RandomPoint(float range)
    {
        Vector3 randomPoint = transform.position + (Random.insideUnitSphere * range);
        NavMeshHit hit;

        if (NavMesh.SamplePosition(randomPoint, out hit, 1.0f, NavMesh.AllAreas))
        {
            return hit.position;
        }
        return transform.position;
    }

    void HandleTempestEntered(GameObject target)
    {
        if (target.TryGetComponent<TempestMain>(out TempestMain targetTempest))
        {
            targetTempest.OnSizeChange += HandleOtherGrowth;
            targetTempest.OnAbsorbed += HandleOtherDeath;

            if (targetTempest.size >= selfTempest.size)
            {
                predators.Add(target);
                SortByDistance(predators);
            }
            else
            {
                prey.Add(target);
                SortByDistance(prey);
            }
        }
    }

    void HandleTempestExited(GameObject target)
    {
        if (target.TryGetComponent<TempestMain>(out TempestMain tempest))
        {
            tempest.OnSizeChange -= HandleOtherGrowth;
            tempest.OnAbsorbed -= HandleOtherDeath;
        }


        try
        {
            predators.Remove(target);
            prey.Remove(target);
        }
        catch { }
    }

    void HandleSelfGrowth(GameObject self, float newSize)
    {
        foreach (GameObject tempest in predators.ToList())
        {
            if (tempest.GetComponent<TempestMain>().size < newSize)
            {
                predators.Remove(tempest);
                prey.Add(tempest);
            }
        }

        SortByDistance(prey);
    }

    void HandleOtherGrowth(GameObject target, float newSize)
    {
        if (newSize > selfTempest.size)
        {
            prey.Remove(target);
            predators.Add(target);
        }

        SortByDistance(predators);
    }

    void HandleSelfDeath(GameObject deceased)
    {
        targetSensor.OnTargetEnter -= HandleTempestEntered;
        targetSensor.OnTargetExit -= HandleTempestExited;
        selfTempest.OnSizeChange -= HandleSelfGrowth;
        selfTempest.OnAbsorbed -= HandleSelfDeath;
    }

    void HandleOtherDeath(GameObject deceased)
    {
        TempestMain tempest = deceased.GetComponent<TempestMain>();
        tempest.OnSizeChange -= HandleOtherGrowth;
        tempest.OnAbsorbed -= HandleOtherDeath;
    }

    void SortByDistance(in List<GameObject> list)
    {
        list.Sort(comparison: (a, b) =>
            Vector3.SqrMagnitude(vector: b.transform.position - transform.position)
                .CompareTo(
                    Vector3.SqrMagnitude(vector: a.transform.position - transform.position)
                )
        );
    }
}
