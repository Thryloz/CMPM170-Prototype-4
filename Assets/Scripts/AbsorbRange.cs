using UnityEngine;
using System.Collections.Generic;

public class AbsorbRange : MonoBehaviour
{
    [SerializeField] private TempestMain selfTempest;
    [SerializeField] private List<TempestMain> tempestList = new List<TempestMain>();
    [SerializeField] private List<DebrisStateManager> rubbleList = new List<DebrisStateManager>();
    [SerializeField] private List<TurnToRubble> stabilityList = new List<TurnToRubble>();

    [SerializeField] private BoxCollider col;

    private void Awake()
    {
    }

    private void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        CheckAbsorbList(); // oh boy
        col.size = Vector3.one * (selfTempest.size * 4f);
    }

    private void CheckAbsorbList()
    {
        foreach (TempestMain tempest in new List<TempestMain>(tempestList))
        {
            tempest.ModifyStability(-selfTempest.stabilityDamageRate * Time.deltaTime);
            if (tempest == null)
            {
                tempestList.Remove(tempest);
                return;
            }
            if (IsAbsorbable(tempest)) 
            {
                selfTempest.ChangeSize(tempest.size);
                tempestList.Remove(tempest);
                tempest.GetAbsorbed();
            }
        }

        foreach (TurnToRubble rubbleable in new List<TurnToRubble>(stabilityList))
        {
            if (rubbleable == null)
            {
                stabilityList.Remove(rubbleable);
                return;
            }
            rubbleable.ModifyStability(-selfTempest.stabilityDamageRate * 1.5f * Time.deltaTime);
        }



        if (selfTempest.isPlayer)
        {
            if (selfTempest.gameObject.GetComponent<TempestController>().isSucking)
            {
                foreach (DebrisStateManager rubble in rubbleList)
                {
                    if (rubble == null)
                    {
                        rubbleList.Remove(rubble);
                        return;
                    }
                    if (rubble.currentState == rubble.idleState)
                    {
                        rubble.SwitchState(rubble.suckedState);
                    }
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
        else if (other.CompareTag("Rubble"))
        {
            DebrisStateManager thisRubble = other.GetComponent<DebrisStateManager>();
            if (thisRubble.currentState == thisRubble.projectileState)
            {
                selfTempest.ModifyStability(-thisRubble.stabilityDamage);
                selfTempest.ChangeSize(-selfTempest.size * (thisRubble.sizePercentDamage / 100f));
            }
            rubbleList.Add(thisRubble);
        }
        else if (other.CompareTag("TurnToRubble"))
        {
            stabilityList.Add(other.gameObject.GetComponent<TurnToRubble>());
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

            if (rubble.currentState == rubble.suckedState)
            {
                rubble.SwitchState(rubble.idleState);
            }

            rubbleList.Remove(rubble);
        }
    }
}
