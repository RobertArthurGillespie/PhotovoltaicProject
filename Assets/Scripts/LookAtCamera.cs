using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtCamera : MonoBehaviour
{
    public Camera mainCamera;
    // Start is called before the first frame update
    void Start()
    {
        mainCamera = GameObject.Find("ExampleCamera").GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        if (mainCamera != null)
        {
            //Debug.Log("looking at camera");
            // Calculate the direction from the quad to the camera
            Vector3 lookAtDirection = mainCamera.transform.position - this.gameObject.transform.position;

            // Adjust the rotation to look at the camera
            this.gameObject.transform.rotation = Quaternion.LookRotation(lookAtDirection, Vector3.up);

            this.gameObject.transform.Rotate(Vector3.up, 180f);
        }
    }
}
