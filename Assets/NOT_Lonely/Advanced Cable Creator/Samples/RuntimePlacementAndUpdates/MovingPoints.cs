using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MovingPoints : MonoBehaviour
{
    public RuntimePlacementExample placer;
    public List<Transform> pointsTracker = new List<Transform>();
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(MovePoint());
    }

    // Update is called once per frame
    void Update()
    {
        if (GameObject.Find("Point(Clone)") != null)
        {
            /*GameObject point = GameObject.Find("Point(Clone)");
            Debug.Log("transform is: " + point.transform.position.x + "," + point.transform.position.y + "," + point.transform.position.z);
            float zPosition = point.transform.position.z;
            point.transform.position = new Vector3(Input.mousePosition.x, Input.mousePosition.y, zPosition);
            placer.CreateNewPoint(Input.mousePosition);*/
        }

        if (Input.GetMouseButton(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);


            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 100))
            {
                if (hit.transform.gameObject.name == "Cube")
                {

                }
            }
        }
        
    }

    public IEnumerator MovePoint()
    {
        while (true)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);


            RaycastHit hit;

            
            if (Input.GetMouseButton(0))
            {
                if (Physics.Raycast(ray, out hit, 1000))
                {
                    if (hit.transform.gameObject.name == "Cube")
                    {
                        Debug.Log("hit cube");
                        placer.CreateNewPoint(hit.point);
                        if (placer.dragPoints.Count > 7)
                        {
                            Debug.Log("deleting point");
                            int prevPoint = placer.dragPoints.Count - 2;
                            Object.Destroy(placer.dragPoints[prevPoint].gameObject);
                            placer.dragPoints[prevPoint].onPointDestroy.Invoke(placer.dragPoints[prevPoint].transform);
                            /*int prevPoint2 = placer.dragPoints.Count - 2;
                            Object.Destroy(placer.dragPoints[prevPoint2].gameObject);
                            placer.dragPoints[prevPoint2].onPointDestroy.Invoke(placer.dragPoints[prevPoint2].transform);
                            placer.dragPoints.RemoveAt(prevPoint);
                            placer.dragPoints.RemoveAt(prevPoint2);*/
                        }
                        
                        
                        
                    }
                }
                
            }
            else if (Input.GetKeyDown(KeyCode.Space))
            {
                break;
            }
            //placer.CreateNewPoint(new Vector3(Input.mousePosition.x,Input.mousePosition.y,0f));
            yield return null;
        }
        
    }
}
