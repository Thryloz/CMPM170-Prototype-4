using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject objectToSpawn;
    public float range;

    public int amount = 10;


    

    void Start()
    {
        for (int i = 0; i < amount; i++)
        {
            Instantiate(objectToSpawn, transform.position + new Vector3(Random.Range(-range, range), 0, Random.Range(-range, range)), Quaternion.identity);
        }
    }

    void Update()
    {
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }

}
