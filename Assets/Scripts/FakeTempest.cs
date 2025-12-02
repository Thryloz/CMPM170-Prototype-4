using System;
using UnityEngine;

public class FakeTempest : MonoBehaviour
{
    [Header("Stats")]
    public float size;
    public float sizeDecayPercentage = 1f;
    public float maxSize = 50f;

    [Header("Stablity")]
    public float stabilityIncreaseRate = 1f;
    public float stabilityAbsorbThreshold = 20f;
    public float stabilityDamageRate;
    [field: SerializeField, Range(0, 100)] public float Stability { get; set; }


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
    [SerializeField] private ParticleSystem outerGray;

    [Header("Level 2")]
    [SerializeField] public float level2Threshold = 10f;
    [SerializeField] private ParticleSystem outerBlack;

    [Header("Level 3")]
    [SerializeField] public float level3Threshold = 25f;
    [SerializeField] private ParticleSystem outermostWhite;

    private Material coreMaterial;
    private Material outerGrayMaterial;
    private Material outerMaterialBlack;
    private Material outermostMaterialWhite;

    [Header("Movement (should be influenced by size)")]
    public float maxSpeed;
    public float acceleration;

    [Header("References")]
    [SerializeField] private GameObject VFXRoot;

    [Header("Debug")]

    // Observer/listener/event/thigns
    [NonSerialized] public Action<GameObject, float> OnSizeChange; // <self, newSize>
    [NonSerialized] public Action<GameObject> OnAbsorbed;

    public bool isPlayer = false;
    private void Start()
    {
        if (TryGetComponent<TempestController>(out _))
        {
            coreColor = playerColor;
            isPlayer = true;
        }

        GetMaterials();

        outerBlack.gameObject.SetActive(false);
        outermostWhite.gameObject.SetActive(false);

        Stability = 100f;
    }


    private void Update()
    {
        coreMaterial.SetColor("_Color", coreColor);

        HandleSizeVisuals();
        HandleStabilityVisuals();
    }

    private void GetMaterials()
    {
        coreMaterial = core.GetComponent<ParticleSystemRenderer>().material;
        outerGrayMaterial = outerGray.GetComponent<ParticleSystemRenderer>().material;
        outerMaterialBlack = outerBlack.GetComponent<ParticleSystemRenderer>().material;
        outermostMaterialWhite = outermostWhite.GetComponent<ParticleSystemRenderer>().material;
    }

    private void HandleSizeVisuals()
    {
        if (size <= level2Threshold)
        {
            outerBlack.gameObject.SetActive(false);
            outermostWhite.gameObject.SetActive(false);

            float coreValue = Remap(size, level1Threshold, level2Threshold, 0.5f, 1f);

            core.transform.localScale = new Vector3(coreValue, coreValue, coreValue);
            outerGray.transform.localScale = new Vector3(coreValue * 1.2f, coreValue, coreValue * 1.2f);
        }
        else if (size > level2Threshold && size <= level3Threshold)
        {
            outerBlack.gameObject.SetActive(true);
            outermostWhite.gameObject.SetActive(false);


            // remaps the tempestSize (threshold 2 to threshold 3) to scale (1f, 2f)
            float coreValue = Remap(size, level2Threshold, level3Threshold, 1f, 2f);

            core.transform.localScale = new Vector3(coreValue, 1f, coreValue);
            outerBlack.transform.localScale = new Vector3(coreValue, 1f, coreValue);
            outerGray.transform.localScale = new Vector3(coreValue * 1.2f, 1f, coreValue * 1.2f);

        }
        else if (size > level3Threshold)
        {
            outerBlack.gameObject.SetActive(true);
            outermostWhite.gameObject.SetActive(true);


            // remaps the tempestSize (threshold 3 to maxSize) to x, z scale (2f, 5f)
            float coreValue = Remap(size, level3Threshold, maxSize, 2f, 5f);
            float y_value = Remap(size, level3Threshold, maxSize, 1f, 3f);

            core.transform.localScale = new Vector3(coreValue, y_value, coreValue);
            outerGray.transform.localScale = outerBlack.transform.localScale = new Vector3(coreValue * 1.2f, y_value, coreValue * 1.2f);
            outermostWhite.transform.localScale = new Vector3(coreValue * 2, y_value, coreValue * 2);
        }
    }
    float wobbleSpeedCore;
    float wobbleAmountCore;

    float wobbleSpeedBlack;
    float wobbleAmountBlack;

    private void HandleStabilityVisuals()
    {
        if (size <= level2Threshold)
        {
            wobbleSpeedCore = Remap(Stability, 50f, 100f, 6f, 3f);
            wobbleAmountCore = Remap(Stability, 50f, 100f, 0.7f, 0.2f);
        }
        else if (size > level2Threshold && size <= level3Threshold)
        {
            wobbleSpeedCore = Remap(Stability, 50f, 100f, 5f, 3f);
            wobbleAmountCore = Remap(Stability, 50f, 100f, 1.5f, 0.2f);

            wobbleSpeedBlack = Remap(Stability, 50f, 100f, 5f, 3f);
            wobbleAmountBlack = Remap(Stability, 50f, 100f, 1.5f, 0.5f);
        }
        else if (size > level3Threshold)
        {
            wobbleSpeedCore = Remap(Stability, 50f, 100f, 7f, 3f);
            wobbleAmountCore = Remap(Stability, 50f, 100f, 5f, 0.2f);
        }
        coreMaterial.SetFloat("_WobbleSpeed", wobbleSpeedCore);
        coreMaterial.SetFloat("_WobbleAmount", wobbleAmountCore);

        outerGrayMaterial.SetFloat("_WobbleSpeed", wobbleSpeedCore);
        outerGrayMaterial.SetFloat("_WobbleAmount", wobbleAmountCore);

        outerMaterialBlack.SetFloat("_WobbleSpeed", wobbleSpeedBlack);
        outerMaterialBlack.SetFloat("_WobbleAmount", wobbleAmountBlack);

        outermostMaterialWhite.SetFloat("_WobbleSpeed", wobbleSpeedCore);
        outermostMaterialWhite.SetFloat("_WobbleAmount", wobbleAmountCore);
    }

    public static float Remap(float value, float fromMin, float fromMax, float toMin, float toMax)
    {
        return (toMax - toMin) * ((value - fromMin) / (fromMax - fromMin)) + toMin;
    }
}
