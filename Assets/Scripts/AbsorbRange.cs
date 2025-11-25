using UnityEngine;
using System.Collections.Generic;

public class AbsorbRange : MonoBehaviour
{
    [SerializeField] private TempestMain selfTempest;
    [SerializeField] private List<TempestMain> list = new List<TempestMain>();

    [SerializeField] private BoxCollider col;

    private float stabilityDamageTimer = 0f;


    private void Awake()
    {
    }


    // Update is called once per frame
    void Update()
    {
        CheckAbsorbList();
        col.size = Vector3.one * (selfTempest.size * 3f);
    }

    private void CheckAbsorbList()
    {
        foreach (TempestMain tempest in new List<TempestMain>(list))
        {
            stabilityDamageTimer += Time.deltaTime;
            if (stabilityDamageTimer > 1f)
            {
                tempest.ModifyStability(-selfTempest.stabilityDamageRate);
                stabilityDamageTimer = 0f;
            }

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
        if (other.CompareTag("NPC_Tempest"))
        {
            // should be try get component to be safe but ehhh
            list.Add(other.gameObject.GetComponent<TempestMain>());
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.CompareTag("NPC_Tempest"))
        {
            list.Remove(other.gameObject.GetComponent<TempestMain>());
        }
    }
}
