using UnityEngine;

public class TurnToRubble : MonoBehaviour, IStability
{
    [field: SerializeField, Range(0, 100)] public float Stability { get; set; }
    [SerializeField] private GameObject rubble;
    

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Stability >= 0)
        {
            Instantiate(rubble, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }

    public void ModifyStability(float amount)
    {
        //EventBus.Instance.DoDamage(gameObject, EventBus.DamageType.PASSIVE);
        if (Stability + amount >= 100f)
        {
            Stability = 100f;
        }
        else
        {
            Stability += amount;
        }
    }
}
