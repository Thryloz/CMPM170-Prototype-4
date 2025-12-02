using System.Threading;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject objectToSpawn;
    public float range = 200f;

    public int initialAmount = 10;
    public float y_pos = 0f;

    [Header("Spawn Over Time")]
    public bool spawnOverTime = false;
    public float timeInterval = 5f;

    private float timer;

    void Start()
    {
        for (int i = 0; i < initialAmount; i++)
        {
            Instantiate(objectToSpawn, transform.position + new Vector3(Random.Range(-range, range), y_pos, Random.Range(-range, range)), Quaternion.identity);
        }
    }

    void Update()
    {
        if (spawnOverTime)
        {
            if (timer > timeInterval)
            {
                Instantiate(objectToSpawn, transform.position + new Vector3(Random.Range(-range, range), y_pos, Random.Range(-range, range)), Quaternion.identity);
            }
        }
        
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }

}
