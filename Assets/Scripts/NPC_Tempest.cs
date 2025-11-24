using UnityEngine;

public class NPC_Tempest : MonoBehaviour
{
    [SerializeField] private TempestMain tempest;
    [SerializeField] private GameObject player;

    private TempestMain playerTempest;

    private void Awake()
    {
        player = GameObject.Find("Player_Tempest");
        playerTempest = player.GetComponent<TempestMain>();
    }

    private void Start()
    {
        tempest.size = Random.Range(tempest.level1Threshold, tempest.maxSize);
    }

    private void Update()
    {
        if (player != null)
        {
            if (tempest.size < playerTempest.size)
            {
                tempest.coreColor = tempest.absorbableEnemyColor;
            }
            else
            {
                tempest.coreColor = tempest.unabsorbableEnemyColor;
            }
        }
    }
}
