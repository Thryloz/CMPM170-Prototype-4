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

    [Header("LEVEL 3")]
    public float level3Threshold = 25.0f;
    [SerializeField] private GameObject level3Root;
    [SerializeField] private ParticleSystem tempestCoreLevel3;
    [SerializeField] private ParticleSystem tempestOuterWhiteLevel3;
    [SerializeField] private ParticleSystem tempestOuterBlackLevel3;
    [SerializeField] private ParticleSystem tempestOutermost;

    public float maxSize = 50f;

    
    [Header("Movement (should be influenced by size)")]
    public float maxSpeed;
    public float acceleration;

    [Header("References")]
    [SerializeField] private GameObject VFXRoot;
    [SerializeField] private AbsorbRange absorbRange;

    private void Start()
    {
        level1CoreMaterial = tempestCoreLevel1.GetComponent<ParticleSystemRenderer>().material;
        level1OuterMaterial = tempestOuterLevel1.GetComponent<ParticleSystemRenderer>().material;

        level1Root.SetActive(true);
        level2Root.SetActive(false);
        level3Root.SetActive(false);
    }

    private void Update()
    {
        level1CoreMaterial.SetColor("_Color", coreColor);
        if (size <= level2Threshold)
        {
            level1Root.SetActive(true);
            level2Root.SetActive(false);
            level3Root.SetActive(false);

            // remaps the tempestSize (1, thresholds) to scale (0.5 to 1) for scaling
            float coreValue = Remap(size, level1Threshold, level2Threshold, 0.5f, 1f);

            tempestCoreLevel1.transform.localScale = new Vector3(coreValue, coreValue, coreValue);
            tempestOuterLevel1.transform.localScale = new Vector3(coreValue * 1.2f, coreValue, coreValue * 1.2f);

            float intensity = Remap(size, level1Threshold, level2Threshold, 0.5f, 4f);
            float speed = Remap(size, level1Threshold, level2Threshold, -.5f, -.8f);
            level1CoreMaterial.SetFloat("_NoiseSpeed", speed);
            level1CoreMaterial.SetFloat("_WobbleIntensity", intensity);
            level1CoreMaterial.SetFloat("_WobbleSpeed", intensity * 3f/4f);
            level1CoreMaterial.SetFloat("_WobbleFrequency", intensity/2);

            level1OuterMaterial.SetFloat("_NoiseSpeed", speed);
            level1OuterMaterial.SetFloat("_WobbleIntensity", intensity);
            level1OuterMaterial.SetFloat("_WobbleSpeed", intensity * 3f / 4f);
            level1OuterMaterial.SetFloat("_WobbleFrequency", intensity / 2);

            // remaps the tempestSize (1, thresholds) to speed (40 to 30)
            maxSpeed = Remap(size, level1Threshold, level2Threshold, 40f, 30f);


        }
        else if (size > level2Threshold && size <= level3Threshold)
        {
            level1Root.SetActive(false);
            level2Root.SetActive(true);
            level3Root.SetActive(false);

            // remaps the tempestSize (threshold 2 to threshold 3) to scale (1f, 2f)
            float coreValue = Remap(size, level2Threshold, level3Threshold, 1f, 2f);

            tempestCoreLevel2.transform.localScale = new Vector3(coreValue, 1f, coreValue);
            tempestOuterWhiteLevel2.transform.localScale = new Vector3(coreValue * 1.2f, 1f, coreValue * 1.2f);
            tempestOuterBlackLevel2.transform.localScale = new Vector3(coreValue * 1.2f, 1f, coreValue * 1.2f);

        }
        else if (size > level3Threshold)
        {
            level1Root.SetActive(false);
            level2Root.SetActive(false);
            level3Root.SetActive(true);

            // remaps the tempestSize (threshold 3 to maxSize) to x, z scale (2f, 5f)
            float coreValue = Remap(size, level3Threshold, maxSize, 2f, 5f);
            float y_value = Remap(size, level3Threshold, maxSize, 1f, 3f);

            tempestCoreLevel3.transform.localScale = new Vector3(coreValue, y_value, coreValue);
            tempestOuterWhiteLevel3.transform.localScale = tempestOuterBlackLevel3.transform.localScale = new Vector3(coreValue * 1.2f, y_value, coreValue * 1.2f);
            tempestOutermost.transform.localScale = new Vector3(coreValue * 2, y_value, coreValue * 2);
        }
    }

    public static float Remap(float value, float from1, float to1, float from2, float to2)
    {
        return (value - from1) / (to1 - from1) * (to2 - from2) + from2;
    }
}

