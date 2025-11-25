using UnityEngine;

public class NPC_Tempest : MonoBehaviour
{
    [SerializeField] private TempestMain tempest;
    [SerializeField] private GameObject player;
    [SerializeField] private AbsorbRange absorb;


    private TempestMain playerTempest;

    private void Awake()
    {
        player = GameObject.Find("Player_Tempest");
        playerTempest = player.GetComponent<TempestMain>();
    }

    private void Start()
    {
        tempest.size = Random.Range(tempest.level1Threshold, tempest.level2Threshold);
    }

    private void Update()
    {
        if (player != null)
        {
            if (playerTempest.size > tempest.size && tempest.Stability <= tempest.stabilityAbsorbThreshold)
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
