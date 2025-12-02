using UnityEngine;
using System.Collections.Generic;
public class RubbleSucker : MonoBehaviour
{
    public List<Rigidbody> nearbyRubble = new List<Rigidbody>();
    public float pullForce;
    public float maxVelocity = 5f;
    private Vector3 tempestCenter;

    private void Start()
    {
        
    }

    private void Update()
    {
        tempestCenter = new Vector3(transform.position.x, transform.position.y + 10f, transform.position.z);
        if (nearbyRubble.Count > 0)
        {
            for (int i = 0; i < nearbyRubble.Count; i++)
            {
                if (nearbyRubble[i].angularVelocity.magnitude < maxVelocity)
                {
                    nearbyRubble[i].AddForce((tempestCenter - nearbyRubble[i].transform.position).normalized * pullForce, ForceMode.Acceleration);  
                }
            }
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Rubble"))
        {
            nearbyRubble.Add(other.gameObject.GetComponent<Rigidbody>());
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Rubble"))
        {
            nearbyRubble.Remove(other.gameObject.GetComponent<Rigidbody>());
        }
    }
}
