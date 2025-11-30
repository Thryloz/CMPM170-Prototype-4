using UnityEngine;
using System.Collections.Generic;

public class AbsorbRange : MonoBehaviour
{
    [SerializeField] private TempestMain selfTempest;
    [SerializeField] private List<TempestMain> list = new List<TempestMain>();

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
        foreach (TempestMain tempest in new List<TempestMain>(list))
        {

            tempest.ModifyStability(-selfTempest.stabilityDamageRate * Time.deltaTime);
            if (IsAbsorbable(tempest)) 
            {
                selfTempest.ChangeSize(tempest.size);
                list.Remove(tempest);
                tempest.GetAbsorbed();
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
            list.Add(other.gameObject.GetComponent<TempestMain>());
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.CompareTag("NPC_Tempest") || other.CompareTag("Player"))
        {
            list.Remove(other.gameObject.GetComponent<TempestMain>());
        }
    }
}
