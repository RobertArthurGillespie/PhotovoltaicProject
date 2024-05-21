using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;

public class MovingPoints : MonoBehaviour
{
    public RuntimePlacementExample placer;
    public List<Transform> pointsTracker = new List<Transform>();
    public List<GameObject> UIDialogObjs = new List<GameObject>();
    public GameObject hitBinding;
    public TextMeshProUGUI readingLabel;
    public bool readingTaken = false;
    public bool shouldBlur = false;
    public bool isGroundCable = false;
    public bool groundCableFinished = false;
    public Material[] groundCableMats;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(MovePoint());
        placer.CreateNewPoint(Camera.main.transform.position - new Vector3(-3f, -3f, -3f));
        placer.CreateNewPoint(Camera.main.transform.position - new Vector3(-3f, -3f, -3f));
        placer.CreateNewPoint(Camera.main.transform.position - new Vector3(-3f, -3f, -3f));
        placer.CreateNewPoint(Camera.main.transform.position - new Vector3(-3f, -3f, -3f));
        placer.CreateNewPoint(Camera.main.transform.position - new Vector3(-3f, -3f, -3f));
        placer.CreateNewPoint(Camera.main.transform.position - new Vector3(-3f, -3f, -3f));
        placer.CreateNewPoint(Camera.main.transform.position - new Vector3(-3f, -3f, -3f));
    }

    // Update is called once per frame
    void Update()
    {
        /*if (Input.GetKeyDown(KeyCode.Space))
        {
            if(!GameObject.Find("Main Camera").GetComponent<Volume>().enabled)
            {
                GameObject.Find("Main Camera").GetComponent<Volume>().enabled = true;

            }
            else if(GameObject.Find("Main Camera").GetComponent<Volume>().enabled)
            {
                GameObject.Find("Main Camera").GetComponent<Volume>().enabled = false;
            }
            foreach(GameObject g in UIDialogObjs)
            {
                if (!g.active)
                {
                    g.SetActive(true);
                }
                else if (g.active)
                {
                    g.SetActive(false);
                }
            }
        }*/
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
                    if (hit.transform.gameObject.name == "Cube"||hit.transform.gameObject.tag=="binding")
                    {
                        if (hit.transform.gameObject.tag == "binding")
                        {
                            if (hit.transform.gameObject.GetComponent<MeshRenderer>().material.color != Color.red)
                            {

                            }
                            hitBinding = hit.transform.gameObject;
                            hit.transform.gameObject.GetComponent<MeshRenderer>().material.color = Color.red;
                            if (!groundCableFinished)
                            {
                                groundCableFinished = true;
                            }
                            else if (!readingTaken)
                            {
                                readingTaken = true;
                                GenerateReading();
                            }
                            
                            Debug.Log("hit object is: " + hit.transform.gameObject.name);
                        }
                        if (hit.transform.gameObject.tag != "binding")
                        {
                            if (hitBinding != null)
                            {
                                hitBinding.GetComponent<MeshRenderer>().material.color = Color.white;
                                readingTaken = false;

                            }
                            
                        }
                        Debug.Log("hit cube");
                        placer.CreateNewPoint(hit.point);
                        if (placer.dragPoints.Count > 7)
                        {
                            Debug.Log("deleting point");
                            int prevPoint = placer.dragPoints.Count - 2;
                            UnityEngine.Object.Destroy(placer.dragPoints[prevPoint].gameObject);
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
            else if (Input.GetMouseButton(1))
            {
                Debug.Log("Right Mouse Button down");
                if (groundCableFinished)
                {
                    if (Physics.Raycast(ray, out hit, 1000))
                    {
                        if (!isGroundCable)
                        {
                            isGroundCable = true;
                            placer.newMaterials = groundCableMats;
                            placer.CreateNewCable();
                            placer.CreateNewPoint(Camera.main.transform.position - new Vector3(-3f, -3f, -3f));
                            placer.CreateNewPoint(Camera.main.transform.position - new Vector3(-3f, -3f, -3f));
                            placer.CreateNewPoint(Camera.main.transform.position - new Vector3(-3f, -3f, -3f));
                            placer.CreateNewPoint(Camera.main.transform.position - new Vector3(-3f, -3f, -3f));
                            placer.CreateNewPoint(Camera.main.transform.position - new Vector3(-3f, -3f, -3f));
                            placer.CreateNewPoint(Camera.main.transform.position - new Vector3(-3f, -3f, -3f));
                            placer.CreateNewPoint(Camera.main.transform.position - new Vector3(-3f, -3f, -3f));
                        }

                        if (hit.transform.gameObject.name == "Cube" || hit.transform.gameObject.tag == "binding")
                        {
                            if (hit.transform.gameObject.tag == "binding")
                            {
                                if (hit.transform.gameObject.GetComponent<MeshRenderer>().material.color != Color.red)
                                {

                                }
                                hitBinding = hit.transform.gameObject;
                                hit.transform.gameObject.GetComponent<MeshRenderer>().material.color = Color.red;
                                if (!readingTaken)
                                {
                                    readingTaken = true;
                                    GenerateReading();
                                }

                                Debug.Log("hit object is: " + hit.transform.gameObject.name);
                            }
                            if (hit.transform.gameObject.tag != "binding")
                            {
                                if (hitBinding != null)
                                {
                                    hitBinding.GetComponent<MeshRenderer>().material.color = Color.white;
                                    readingTaken = false;

                                }

                            }
                            Debug.Log("hit cube");
                            Debug.Log("ground points count currently: " + placer.dragGroundPoints.Count);
                            placer.CreateNewPoint(hit.point);
                            if (placer.dragGroundPoints.Count > 7)
                            {
                                Debug.Log("deleting point");
                                int prevPoint = placer.dragGroundPoints.Count - 2;
                                UnityEngine.Object.Destroy(placer.dragGroundPoints[prevPoint].gameObject);
                                placer.dragGroundPoints[prevPoint].onPointDestroy.Invoke(placer.dragGroundPoints[prevPoint].transform);
                                /*int prevPoint2 = placer.dragPoints.Count - 2;
                                Object.Destroy(placer.dragPoints[prevPoint2].gameObject);
                                placer.dragPoints[prevPoint2].onPointDestroy.Invoke(placer.dragPoints[prevPoint2].transform);
                                placer.dragPoints.RemoveAt(prevPoint);
                                placer.dragPoints.RemoveAt(prevPoint2);*/
                            }



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

    public void GenerateReading()
    {
        Debug.Log("generating reading");
        float randomFloat = UnityEngine.Random.Range(0f, 50f);
        double randomRounded = System.Math.Round(randomFloat, 2);
        readingLabel.text = "Value: " + randomRounded;
        GameObject.Find("noteValueText").GetComponent<TextMeshPro>().text = randomRounded.ToString();
        GameObject.Find("noteValueTextForm").GetComponent<TextMeshPro>().text = randomRounded.ToString();
        GameObject.Find("clampMeterDisplay").GetComponent<TextMeshPro>().text = "VOC: "+randomRounded.ToString();
        StartCoroutine(FadeInAndFillOutForm());
    }

    public IEnumerator FadeInAndOpenNotebook()
    {
        Debug.Log("fading in notebook");
        Material notepadMat = GameObject.Find("Notepad").GetComponent<MeshRenderer>().material;
        while (true)
        {
            if (notepadMat.color.a < 1f)
            {
                float notePadAlpha = notepadMat.color.a;
                notePadAlpha += 0.1f;
                notepadMat.color = new Color(notepadMat.color.r,notepadMat.color.g,notepadMat.color.b,notePadAlpha);
                GameObject.Find("Notepad").GetComponent<MeshRenderer>().material = notepadMat;
                GameObject.Find("notepad_bottom").GetComponent<MeshRenderer>().material = notepadMat;
                GameObject.Find("notepad_top").GetComponent<MeshRenderer>().material = notepadMat;
                Debug.Log("notepad alpha is: "+notepadMat.color.a);
            }
            else
            {
                break;
            }
            yield return new WaitForSeconds(0.05f);
        }
        GameObject.Find("Notepad").GetComponent<Animator>().SetBool("OpenNotebook",true);
        yield return new WaitForSeconds(1f);
        //Material noteTextMat = GameObject.Find("noteValueText").GetComponent<MeshRenderer>().material;
        
        while (true)
        {
            if (GameObject.Find("noteValueText").GetComponent<TextMeshPro>().color.a < 1f)
            {
                float noteTextAlpha = GameObject.Find("noteValueText").GetComponent<TextMeshPro>().color.a;
                noteTextAlpha += 0.1f;
                GameObject.Find("noteValueText").GetComponent<TextMeshPro>().color = new Color(GameObject.Find("noteValueText").GetComponent<TextMeshPro>().color.r, GameObject.Find("noteValueText").GetComponent<TextMeshPro>().color.g, GameObject.Find("noteValueText").GetComponent<TextMeshPro>().color.b, noteTextAlpha);
                //GameObject.Find("noteValueText").GetComponent<MeshRenderer>().material = noteTextMat;
            }
            else
            {
                break;
            }
            yield return new WaitForSeconds(0.05f);
        }
        yield return null;
    }

    public IEnumerator FadeInAndFillOutForm()
    {
        Debug.Log("fading in notebook");
        Material notepadMat = GameObject.Find("Form").GetComponent<MeshRenderer>().material;
        while (true)
        {
            if (notepadMat.color.a < 1f)
            {
                float notePadAlpha = notepadMat.color.a;
                notePadAlpha += 0.1f;
                notepadMat.color = new Color(notepadMat.color.r, notepadMat.color.g, notepadMat.color.b, notePadAlpha);
                GameObject.Find("Form").GetComponent<MeshRenderer>().material = notepadMat;
                //GameObject.Find("notepad_bottom").GetComponent<MeshRenderer>().material = notepadMat;
                //GameObject.Find("notepad_top").GetComponent<MeshRenderer>().material = notepadMat;
                Debug.Log("notepad alpha is: " + notepadMat.color.a);
            }
            else
            {
                break;
            }
            yield return new WaitForSeconds(0.05f);
        }
        //GameObject.Find("Notepad").GetComponent<Animator>().SetBool("OpenNotebook", true);
        yield return new WaitForSeconds(1f);
        //Material noteTextMat = GameObject.Find("noteValueText").GetComponent<MeshRenderer>().material;

        while (true)
        {
            if (GameObject.Find("noteValueTextForm").GetComponent<TextMeshPro>().color.a < 1f)
            {
                float noteTextAlpha = GameObject.Find("noteValueTextForm").GetComponent<TextMeshPro>().color.a;
                noteTextAlpha += 0.1f;
                GameObject.Find("noteValueTextForm").GetComponent<TextMeshPro>().color = new Color(GameObject.Find("noteValueTextForm").GetComponent<TextMeshPro>().color.r, GameObject.Find("noteValueTextForm").GetComponent<TextMeshPro>().color.g, GameObject.Find("noteValueTextForm").GetComponent<TextMeshPro>().color.b, noteTextAlpha);
                //GameObject.Find("noteValueText").GetComponent<MeshRenderer>().material = noteTextMat;
            }
            else
            {
                break;
            }
            yield return new WaitForSeconds(0.05f);
        }
        yield return null;
    }
}
