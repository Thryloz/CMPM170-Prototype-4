using UnityEngine;
using System.Collections.Generic;

public class AbsorbRange : MonoBehaviour
{
    private SphereCollider sc;
    [SerializeField] private List<GameObject> list = new List<GameObject>();

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
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("NPC_Tempest"))
        {
            list.Add(other.gameObject);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.CompareTag("NPC_Tempest"))
        {
            list.Remove(other.gameObject);
        }
    }
}
