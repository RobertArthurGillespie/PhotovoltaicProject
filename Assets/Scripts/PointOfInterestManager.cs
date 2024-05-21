using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointOfInterestManager : MonoBehaviour
{
    public bool simHasStarted = false;
    public bool endGlow = false;
    public bool glowHasStarted = false;
    public bool isOverPOI = false;
    public bool hasBeenSelected = false;
    public SolarVoltaicManager solarVoltaicManager;

    public Color startColor;
    public Color endColor;
    public IEnumerator GlowEffect()
    {
        // Ensure the material has the '_InnerGlowColor' property
        if (GameObject.Find("IrradianceHitSphere").GetComponent<MeshRenderer>().material.HasProperty("_InnerGlowColor"))
        {
            // Set the color property value
            GameObject.Find("IrradianceHitSphere").GetComponent<MeshRenderer>().material.SetColor("_InnerGlowColor", Color.red);
        }
        else
        {
            Debug.LogWarning("Material does not have the '_InnerGlowColor' property.");
        }
        yield return null;
    }

    IEnumerator TransitionAlpha()
    {
        float transitionDuration = 2f;
        while (true)
        {

            float elapsedTime = 0f;
            while (elapsedTime < transitionDuration)
            {
                float t = elapsedTime / transitionDuration;
                GameObject.Find("IrradianceHitSphere").GetComponent<MeshRenderer>().material.SetColor("_InnerGlowColor", Color.Lerp(startColor, endColor, t));
                elapsedTime += Time.deltaTime;
                yield return null;
            }

            // Transition from end color to start color
            elapsedTime = 0f;
            while (elapsedTime < transitionDuration)
            {
                float t = elapsedTime / transitionDuration;
                GameObject.Find("IrradianceHitSphere").GetComponent<MeshRenderer>().material.SetColor("_InnerGlowColor", Color.Lerp(endColor, startColor, t));
                elapsedTime += Time.deltaTime;
                yield return null;
            }
            if (endGlow)
            {
                glowHasStarted = false;
                break;
            }
            if (hasBeenSelected)
            {
                GameObject.Find("IrradianceHitSphere").GetComponent<MeshRenderer>().material.SetColor("_InnerGlowColor", Color.green);
                glowHasStarted = false;
                break;
            }
            // Transition from start color to end color

        }
        if (!hasBeenSelected)
        {
            GameObject.Find("IrradianceHitSphere").GetComponent<MeshRenderer>().enabled = false;
        }
        if (hasBeenSelected)
        {
            GameObject.Find("IrradianceHitSphere").GetComponent<MeshRenderer>().material.SetColor("_InnerGlowColor", Color.green);
            solarVoltaicManager.PlayIrradianceMScene();
        }
        
    }

    private void OnMouseOver()
    {
        if (!glowHasStarted&&!hasBeenSelected)
        {
            GameObject.Find("IrradianceHitSphere").GetComponent<MeshRenderer>().enabled = true;
            StartCoroutine(TransitionAlpha());
            glowHasStarted = true;
        }
        isOverPOI = true;
    }

    private void OnMouseExit()
    {
        endGlow = true;
        isOverPOI = false;
    }

    // Start is called before the first frame update
    void Start()
    {
        solarVoltaicManager = GameObject.Find("EventSystem").GetComponent<SolarVoltaicManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0)&&isOverPOI&&!hasBeenSelected)
        {
            Debug.Log("POI has been selected");
            hasBeenSelected = true;
            GameObject.Find("IrradianceHitSphere").GetComponent<MeshRenderer>().material.SetColor("_InnerGlowColor", Color.green);
        }
    }
}
