using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class environmentController : MonoBehaviour
{

    public EnvironmentSettings settings; // SCRIPTABLE OBJECT TO HANDLE ALL SETTINGS


    // ALL THESE CAN GO, USING SCRIPTABLE OBJECT INSTEAD
    [Header("LIGHT SETTINGS")]
    private GameObject wL;
    public Light worldLight;

    bool tranistionFog = false;
    bool tranistionLight = false;


    private void Awake()
    {
        wL = GameObject.FindWithTag("MainWorldLight");
        worldLight = wL.GetComponent<Light>();

        if(wL == null)
        {
            Debug.LogWarning("MAIN LIGHT NOT FOUND!");
        }
    }

    private void Start()
    {
        worldLight.intensity = settings.LightIntensity;
        worldLight.color = settings.LightColour;

        RenderSettings.fog = settings.FogEnabled;

        if(settings.FogEnabled)
        {
            RenderSettings.fogDensity = settings.FogIntensity;
        }
    }



    private void Update()
    {
        RenderSettings.fog = settings.FogEnabled;

        //trying to smoothly transition between the light & fog intensties

        if(tranistionLight)
        {
            worldLight.intensity = Mathf.Lerp(worldLight.intensity, settings.LightIntensity, settings.LightTransitionSpeed);
            Debug.Log("LightIntensity = " + worldLight.intensity);

            //worldLight.intensity = settings.LightIntensity;
            worldLight.color = settings.LightColour;
        }

        if (settings.FogEnabled && tranistionFog)
        {

            //RenderSettings.fogDensity = settings.FogIntensity;
            RenderSettings.fogDensity = Mathf.Lerp(RenderSettings.fogDensity, settings.FogIntensity, settings.FogTransitionSpeed);
            Debug.Log("FogIntensity = " + RenderSettings.fogDensity);

            RenderSettings.fogColor = settings.FogColour;
        } 

    }

    void DisableFogTranstion()
    {
        tranistionFog = false;
        tranistionLight = false;
    }


    public void UpdateEnvironment()
    {
        tranistionFog = true;
        tranistionLight = true;
        Invoke("DisableFogTranstion", 5f);
    }

    public void IncreaseLighthing(float amount)
    {
        worldLight.intensity += amount;
    }

    public void DecreaseLighthing(float amount)
    {
        worldLight.intensity -= amount;
    }

}
