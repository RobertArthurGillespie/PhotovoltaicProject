using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BinderBoxInteraction : MonoBehaviour
{
    public void OnTriggerExit(Collider other)
    {
        this.gameObject.GetComponent<MeshRenderer>().material.color = Color.white;
        Debug.Log("trigger fired");
    }

    public void OnTriggerEnter(Collider other)
    {
        Debug.Log("generating reading");
        float randomFloat = UnityEngine.Random.Range(0f, 50f);
        double randomRounded = System.Math.Round(randomFloat, 2);
        GameObject.Find("MeasurementText").GetComponent<TextMeshProUGUI>().text = "Value: " + randomRounded;
        if (GameObject.Find("noteValueTextForm").GetComponent<TextMeshPro>() != null)
        {
            Debug.Log("Textmesh isn't null");
            GameObject.Find("noteValueTextForm").GetComponent<TextMeshPro>().text = randomRounded.ToString();
        }
        else
        {
            Debug.Log("Textmesh is null");
        }
        
        this.gameObject.GetComponent<MeshRenderer>().material.color = Color.white;
        Debug.Log("trigger fired");
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
