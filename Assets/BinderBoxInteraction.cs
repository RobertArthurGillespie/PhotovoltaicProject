using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BinderBoxInteraction : MonoBehaviour
{
    public void OnTriggerExit(Collider other)
    {
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
