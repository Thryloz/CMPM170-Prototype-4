using UnityEngine;
using System.Collections.Generic;

public class AbsorbRange : MonoBehaviour
{
    [SerializeField] private TempestMain selfTempest;
    [SerializeField] private List<TempestMain> list = new List<TempestMain>();

    private SphereCollider sc;
    private void Awake()
    {
        sc = GetComponent<SphereCollider>();
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        CheckAbsorbList();
    }

    private void CheckAbsorbList()
    {
        foreach (TempestMain tempest in list)
        {
            if (tempest.size < selfTempest.size)
            {
                selfTempest.ChangeSize(tempest.size);
                list.Remove(tempest);
                tempest.GetAbsorbed();
            }
        }
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
