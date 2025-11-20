using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject objectToSpawn;
    public float range;

    public float minInterval = 1f;
    public float maxInterval = 2f;


    float timeSinceSpawn = 0f;
    float newSpawnTime = 1.5f;

    

    void Start()
    {
        newSpawnTime = Random.Range(minInterval, maxInterval);
    }

    void Update()
    {
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(transform.position, range);
    }

}
