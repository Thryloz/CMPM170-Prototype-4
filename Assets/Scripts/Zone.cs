using UnityEngine;
using System.Collections.Generic;
using System;

public class Zone : MonoBehaviour
{
    public enum Zones 
    {
        Neutral,
        Warm,
        Cold,
        Windy
    }

    public Zones zoneType = Zones.Neutral;

    [Header("Warm")]
    public float warmSizeIncreaseRate = 0.1f;

    [Header("Cold")]
    [Tooltip("Keep it positive.")]
    public float coldSizeDecreaseRate = 0.1f;

    [Header("Windy")]
    public float windySizeDecreaseRate = 0.1f;
    public float windyStabilityDecreaseRate = 0.1f;


    [Header("References")]
    public List<TempestMain> tempestList = new List<TempestMain>();
    [SerializeField] private SphereCollider col;

    private void Start()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, col.radius);
        foreach (var hitCollider in hitColliders)
        {
            if (hitCollider.TryGetComponent<TempestMain>(out TempestMain tempest))
            {
                tempestList.Add(tempest);
            }
        }
    }

    private void Update()
    {
        foreach (TempestMain main in tempestList)
        {
            ApplyZoneEffect(main);
        }
    }

    private void ApplyZoneEffect(TempestMain tempest)
    {
        switch (zoneType)
        {
            case Zones.Neutral:
                break;
            case Zones.Warm:
                tempest.ChangeSize(warmSizeIncreaseRate * Time.deltaTime);
                break;
            case Zones.Cold:
                tempest.ChangeSize(-coldSizeDecreaseRate * Time.deltaTime);
                break;
            case Zones.Windy:
                tempest.ChangeSize(-windyStabilityDecreaseRate * Time.deltaTime);
                tempest.ModifyStability(-windyStabilityDecreaseRate * Time.deltaTime);
                break;
            default:
                break;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<TempestMain>(out TempestMain tempest))
        {
            tempestList.Add(tempest);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent<TempestMain>(out TempestMain tempest))
        {
            tempestList.Remove(tempest);
        }
    }
}
