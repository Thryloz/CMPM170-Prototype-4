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

    public List<TempestMain> tempestList = new List<TempestMain>();

    private void Start()
    {
        
    }

    private void Update()
    {
        foreach (TempestMain main in tempestList)
        {
            ApplyZoneEffect();
        }
    }

    private void ApplyZoneEffect()
    {
        switch (zoneType)
        {
            case Zones.Neutral:
                break;
            case Zones.Warm:

                break;
            case Zones.Cold:
                break;
            case Zones.Windy:
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
