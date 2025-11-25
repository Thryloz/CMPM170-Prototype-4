using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class TempestMain : MonoBehaviour, IAbsorbable, IStability
{
    [Header("Stats")]
    public float size;
    [field: SerializeField, Range(0, 100)] public float Stability { get; set; }
    public float stabilityAbsorbThreshold = 20f;
    [Tooltip("Amount the stability passively increases every second.")]
    public float stabilityRate = 1f;
    [Tooltip("Amount the size decreases every second in percentages.")]
    public float sizeDecayPercentage = 1f;
    public float maxSize = 50f;


    [Header("Colors")]
    [ColorUsage(true, true)]
    public Color playerColor;
    [ColorUsage(true, true)]
    public Color absorbableEnemyColor;
    [ColorUsage(true, true)]
    public Color unabsorbableEnemyColor;
    [ColorUsage(true, true)]
    public Color coreColor;

    [Header("Level 1")]
    [SerializeField] public float level1Threshold = 1f;
    [SerializeField] private ParticleSystem core;
    [SerializeField] private ParticleSystem outerWhite;

    [Header("Level 2")]
    [SerializeField] public float level2Threshold = 10f;
    [SerializeField] private ParticleSystem outerBlack;

    [Header("Level 3")]
    [SerializeField] public float level3Threshold = 25f;
    [SerializeField] private ParticleSystem outermostWhite;

    private Material coreMaterial;
    private Material outerWhiteMaterial;
    private Material outerMaterialBlack;
    private Material outermostMaterialWhite;
    
    [Header("Movement (should be influenced by size)")]
    public float maxSpeed;
    public float acceleration;

    [Header("References")]
    [SerializeField] private GameObject VFXRoot;
    [SerializeField] private AbsorbRange absorbRange;

    [Header("Debug")]
    [SerializeField] private float stabilityTimer = 0f;

    private void Start()
    {
        if (TryGetComponent<TempestController>(out _))
        {
            coreColor = playerColor;
        }

        GetMaterials();

        outerBlack.gameObject.SetActive(false);
        outermostWhite.gameObject.SetActive(false);

        Stability = 100f;
        StartCoroutine(SizeDecay());
    }


    private void Update()
    {
        coreMaterial.SetColor("_Color", coreColor);
        HandleSizeVisuals();
        HandleStabilityVisuals();
        
        PassiveStability();


        Mathf.Clamp(size, 1f, maxSize);
    }

    private void GetMaterials()
    {
        coreMaterial = core.GetComponent<ParticleSystemRenderer>().material;
        outerWhiteMaterial = outerWhite.GetComponent<ParticleSystemRenderer>().material;
        outerMaterialBlack = outerBlack.GetComponent<ParticleSystemRenderer>().material;
        outermostMaterialWhite = outermostWhite.GetComponent<ParticleSystemRenderer>().material;
    }

    private void HandleSizeVisuals()
    {
        if (size <= level2Threshold)
        {
            outerBlack.gameObject.SetActive(false);
            outermostWhite.gameObject.SetActive(false);

            // remaps the tempestSize (1, thresholds) to scale (0.5 to 1) for scaling
            float coreValue = Remap(size, level1Threshold, level2Threshold, 0.5f, 1f);

            core.transform.localScale = new Vector3(coreValue, coreValue, coreValue);
            outerWhite.transform.localScale = new Vector3(coreValue * 1.2f, coreValue, coreValue * 1.2f);


            // remaps the tempestSize (1, thresholds) to speed (40 to 30)
            maxSpeed = Remap(size, level1Threshold, level2Threshold, 40f, 30f);
        }
        else if (size > level2Threshold && size <= level3Threshold)
        {
            outerBlack.gameObject.SetActive(true);
            outermostWhite.gameObject.SetActive(false);


            // remaps the tempestSize (threshold 2 to threshold 3) to scale (1f, 2f)
            float coreValue = Remap(size, level2Threshold, level3Threshold, 1f, 2f);

            core.transform.localScale = new Vector3(coreValue, 1f, coreValue);
            outerBlack.transform.localScale = new Vector3(coreValue, 1f, coreValue);
            outerWhite.transform.localScale = new Vector3(coreValue * 1.2f, 1f, coreValue * 1.2f);

        }
        else if (size > level3Threshold)
        {
            outerBlack.gameObject.SetActive(true);
            outermostWhite.gameObject.SetActive(true);


            // remaps the tempestSize (threshold 3 to maxSize) to x, z scale (2f, 5f)
            float coreValue = Remap(size, level3Threshold, maxSize, 2f, 5f);
            float y_value = Remap(size, level3Threshold, maxSize, 1f, 3f);

            core.transform.localScale = new Vector3(coreValue, y_value, coreValue);
            outerWhite.transform.localScale = outerBlack.transform.localScale = new Vector3(coreValue * 1.2f, y_value, coreValue * 1.2f);
            outermostWhite.transform.localScale = new Vector3(coreValue * 2, y_value, coreValue * 2);
        }
    }

    private void HandleStabilityVisuals()
    {
        float wobbleValue = Remap(Stability, 100f, 0f, .2f, 2f);
        float wobbleSpeed = Remap(Stability, 100f, 0f, 3f, 5f);

        coreMaterial.SetFloat("_WobbleValue", wobbleValue);
        coreMaterial.SetFloat("_WobbleSpeed", wobbleSpeed);

        outerWhiteMaterial.SetFloat("_WobbleValue", wobbleValue);
        outerWhiteMaterial.SetFloat("_WobbleSpeed", wobbleSpeed);

        outerMaterialBlack.SetFloat("_WobbleValue", wobbleValue);
        outerMaterialBlack.SetFloat("_WobbleSpeed", wobbleSpeed);

        outermostMaterialWhite.SetFloat("_WobbleValue", wobbleValue);
        outermostMaterialWhite.SetFloat("_WobbleSpeed", wobbleSpeed);
    }

    public static float Remap(float value, float from1, float to1, float from2, float to2)
    {
        return (value - from1) / (to1 - from1) * (to2 - from2) + from2;
    }

    public void ModifyStability(float amount)
    {
        Stability += amount;
    }

    public void ChangeSize(float value)
    {
        size += value;
    }

    private IEnumerator SizeDecay()
    {
        while (true) 
        {
            yield return new WaitForSeconds(1f);
            ChangeSize(-(size * (sizeDecayPercentage * 0.01f)));
        }
    }

    private void PassiveStability()
    {
        if (stabilityTimer > 1f)
        {
            Stability += stabilityRate;
            stabilityTimer = 0f;
        }
        stabilityTimer += Time.deltaTime;
    }

    public void GetAbsorbed()
    {
        Destroy(gameObject);
    }

    //float intensity = Remap(size, level1Threshold, level2Threshold, 0.5f, 4f);
    //float speed = Remap(size, level1Threshold, level2Threshold, -.5f, -.8f);
    //coreMaterial.SetFloat("_NoiseSpeed", speed);
    //coreMaterial.SetFloat("_WobbleIntensity", intensity);
    //coreMaterial.SetFloat("_WobbleSpeed", intensity * 3f / 4f);
    //coreMaterial.SetFloat("_WobbleFrequency", intensity / 2);

    //outerWhiteMaterial.SetFloat("_NoiseSpeed", speed);
    //outerWhiteMaterial.SetFloat("_WobbleIntensity", intensity);
    //outerWhiteMaterial.SetFloat("_WobbleSpeed", intensity * 3f / 4f);
    //outerWhiteMaterial.SetFloat("_WobbleFrequency", intensity / 2);
}

