using UnityEngine;
using System.Collections.Generic;
using System.Transactions;

public class AbsorbRange : MonoBehaviour
{
    [SerializeField] private TempestMain selfTempest;
    [SerializeField] private List<TempestMain> tempestList = new List<TempestMain>();
    [SerializeField] private List<DebrisStateManager> rubbleList = new List<DebrisStateManager>();

    [SerializeField] private BoxCollider col;

    private void Awake()
    {
    }


    // Update is called once per frame
    void Update()
    {
        CheckAbsorbList();
        col.size = Vector3.one * (selfTempest.size * 4f);
    }

    private void CheckAbsorbList()
    {
        foreach (TempestMain tempest in new List<TempestMain>(tempestList))
        {
            tempest.ModifyStability(-selfTempest.stabilityDamageRate * Time.deltaTime);
            if (IsAbsorbable(tempest)) 
            {
                selfTempest.ChangeSize(tempest.size);
                tempestList.Remove(tempest);
                tempest.GetAbsorbed();
            }
        }

        if (GameManager.Instance.player.isSucking)
        {
            foreach (DebrisStateManager rubble in rubbleList)
            {
                if (rubble.currentState == rubble.idleState)
                {
                    rubble.SwitchState(rubble.suckedState);
                }
            }
        }
    }

    public bool IsAbsorbable(TempestMain tempest)
    {
        if (tempest.size < selfTempest.size && tempest.Stability <= selfTempest.stabilityAbsorbThreshold)
        {
            return true;
        }
        return false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("NPC_Tempest") || other.CompareTag("Player"))
        {
            // should be try get component to be safe but ehhh
            tempestList.Add(other.gameObject.GetComponent<TempestMain>());
        }

        if (other.CompareTag("Rubble"))
        {
            rubbleList.Add(other.gameObject.GetComponent<DebrisStateManager>());
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("NPC_Tempest") || other.CompareTag("Player"))
        {
            tempestList.Remove(other.gameObject.GetComponent<TempestMain>());
        }

        if (other.CompareTag("Rubble"))
        {
            DebrisStateManager rubble = other.gameObject.GetComponent<DebrisStateManager>();
            rubble.SwitchState(rubble.idleState);
            rubbleList.Remove(rubble);
        }
    }
}
