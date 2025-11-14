using UnityEngine;

public class TempestMain : MonoBehaviour
{
    [Header("Stats")]
    public float size;
    [ColorUsage(true, true)]
    public Color coreColor;

    [Header("LEVEL 1")]
    public float level1Threshold = 1.0f;
    [SerializeField] private GameObject level1Root;
    [SerializeField] private ParticleSystem tempestCoreLevel1;
    [SerializeField] private ParticleSystem tempestOuterLevel1;
    private Material level1CoreMaterial;
    private Material level1OuterMaterial;

    [Header("LEVEL 2")]
    public float level2Threshold = 10.0f;
    [SerializeField] private GameObject level2Root;
    [SerializeField] private ParticleSystem tempestCoreLevel2;
    [SerializeField] private ParticleSystem tempestOuterWhiteLevel2;
    [SerializeField] private ParticleSystem tempestOuterBlackLevel2;
    private Material level2CoreMaterial;
    private Material level2OuterMaterialWhite;
    private Material level2OuterMaterialBlack;
    
    [Header("LEVEL 3")]
    public float level3Threshold = 25.0f;
    
    [Header("Movement (should be influenced by size)")]
    public float maxSpeed;
    public float acceleration;

    [Header("References")]
    [SerializeField] private GameObject VFXRoot;

    private void Start()
    {
        level1CoreMaterial = tempestCoreLevel1.GetComponent<ParticleSystemRenderer>().material;
        level1OuterMaterial = tempestOuterLevel1.GetComponent<ParticleSystemRenderer>().material;

        level2CoreMaterial = tempestCoreLevel2.GetComponent<ParticleSystemRenderer>().material;
        level2OuterMaterialWhite = tempestOuterWhiteLevel2.GetComponent<ParticleSystemRenderer>().material;
        level2OuterMaterialBlack = tempestOuterBlackLevel2.GetComponent<ParticleSystemRenderer>().material;

        level1Root.SetActive(true);
        level2Root.SetActive(false);
        //level3Root.SetActive(false);
    }

    private void Update()
    {
        level1CoreMaterial.SetColor("_Color", coreColor);
        if (size <= level2Threshold)
        {
            level1Root.SetActive(true);
            level2Root.SetActive(false);

            // remaps the tempestSize (1, thresholds) to scale (0.5 to 1) for scaling
            float coreValue = Remap(size, 1f, level2Threshold, 0.5f, 1f);

            tempestCoreLevel1.transform.localScale = new Vector3(coreValue, coreValue, coreValue);
            tempestOuterLevel1.transform.localScale = new Vector3(coreValue * 1.2f, coreValue, coreValue * 1.2f);

            float intensity = Remap(size, 1f, level2Threshold, 0.5f, 4f);
            float speed = Remap(size, 1f, level2Threshold, -.5f, -.8f);
            level1CoreMaterial.SetFloat("_NoiseSpeed", speed);
            level1CoreMaterial.SetFloat("_WobbleIntensity", intensity);
            level1CoreMaterial.SetFloat("_WobbleSpeed", intensity * 3f/4f);
            level1CoreMaterial.SetFloat("_WobbleFrequency", intensity/2);

            level1OuterMaterial.SetFloat("_NoiseSpeed", speed);
            level1OuterMaterial.SetFloat("_WobbleIntensity", intensity);
            level1OuterMaterial.SetFloat("_WobbleSpeed", intensity * 3f / 4f);
            level1OuterMaterial.SetFloat("_WobbleFrequency", intensity / 2);

            // remaps the tempestSize (1, thresholds) to speed (40 to 30)
            maxSpeed = Remap(size, 1f, level2Threshold, 40f, 30f);
        }

        if (size > level2Threshold && size <= level3Threshold)
        {
            level1Root.SetActive(false);
            level2Root.SetActive(true);

            float coreValue = Remap(size, level2Threshold, level3Threshold, 1f, 2f);

            tempestCoreLevel2.transform.localScale = new Vector3(coreValue, 1f, coreValue);
            tempestOuterWhiteLevel2.transform.localScale = new Vector3(coreValue * 1.2f, 1f, coreValue * 1.2f);
            tempestOuterBlackLevel2.transform.localScale = new Vector3(coreValue * 1.2f, 1f, coreValue * 1.2f);
        }
    }

    public static float Remap(float value, float from1, float to1, float from2, float to2)
    {
        return (value - from1) / (to1 - from1) * (to2 - from2) + from2;
    }

}

