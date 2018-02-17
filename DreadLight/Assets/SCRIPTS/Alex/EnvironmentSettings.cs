using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "New Environment", menuName = "Environment Settngs")]
public class EnvironmentSettings : ScriptableObject 
{
    
    [Header("LIGHT SETTINGS")]
    [Range(0.01f, 5.0f)]
    public float LightIntensity;
    public Color LightColour;
    public float LightTransitionSpeed;
    [Space(10)]

    [Header("FOG SETTINGS")]
    public bool FogEnabled;
    [Range(0.001f, 0.1f)]
    public float FogIntensity;
    public Color FogColour;
    public float FogTransitionSpeed;
}
