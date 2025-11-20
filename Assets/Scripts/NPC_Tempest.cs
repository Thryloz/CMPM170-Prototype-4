using UnityEngine;

public class NPC_Tempest : MonoBehaviour
{
    [SerializeField] private TempestMain tempest;

    private void Start()
    {
        tempest.size = Random.Range(tempest.level1Threshold, tempest.maxSize);
    }
}
