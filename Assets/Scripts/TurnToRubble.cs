using UnityEngine;

public class TurnToRubble : MonoBehaviour, IStability
{
    [field: SerializeField, Range(0, 100)] public float Stability { get; set; }
    [SerializeField] private GameObject rubble;
    

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        transform.localScale = Vector3.one * Random.Range(1f, 3f);

        if (transform.childCount > 0)
        {
            Transform[] allChildTransforms = GetComponentsInChildren<Transform>();

            foreach (Transform child in allChildTransforms)
            {
                Vector3 randomRotation = new Vector3(0f, Random.Range(0, 360f), 0f);
                child.localEulerAngles = randomRotation;
            }
        }

        transform.localEulerAngles = new Vector3(0f, Random.Range(0, 360f), 0f);
    }

    // Update is called once per frame
    void Update()
    {
        if (Stability <= 50)
        {
            Instantiate(rubble, new Vector3(transform.position.x, transform.position.y + 2f, transform.position.z), Quaternion.identity);
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
