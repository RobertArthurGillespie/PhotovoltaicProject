using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class SolarVoltaicManager : MonoBehaviour
{
    public PlayableDirector solarDirector;
    public CinemachineVirtualCamera IrradianceVCam;
    // Start is called before the first frame update
    void Start()
    {
        //solarDirector.time = solarDirector.time;
        //solarDirector.playableGraph.GetRootPlayable(0).SetSpeed(0);
        //solarDirector.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            //StartCoroutine(PlayIrradianceMScene());
            /*solarDirector.enabled = true;
            solarDirector.time = solarDirector.time;
            solarDirector.Evaluate();
            solarDirector.playableGraph.GetRootPlayable(0).SetSpeed(1);*/
            //StartCoroutine(EndPlaceIRCutscene());
        }
    }

    public IEnumerator EndPlaceIRCutscene()
    {
        while (true)
        {
            if (solarDirector.time >= 1.2333)
            {
                solarDirector.time = solarDirector.time;
                solarDirector.Evaluate();
                solarDirector.playableGraph.GetRootPlayable(0).SetSpeed(0);
                solarDirector.enabled = false;
                break;
            }
            yield return null;
        }
        

    }

    public IEnumerator PlayIrradianceMSceneCoroutine()
    {
        IrradianceVCam.enabled = true;
        GameObject.Find("IR_Meter").GetComponent<Animator>().Play("place_irradiance");
        yield return new WaitForSeconds(2f);
        IrradianceVCam.enabled = false;
        yield return null;
    }

    public void PlayIrradianceMScene()
    {
        StartCoroutine(PlayIrradianceMSceneCoroutine());
    }
}
