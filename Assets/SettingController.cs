using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;


public class SettingController : MonoBehaviour
{
    public GameObject Canvas;
    public GameObject Canvas1;
    public TMPro.TMP_Dropdown DropdownMusic;
    public TMPro.TMP_Dropdown Filters;
    public TMPro.TMP_Dropdown ObjectShapes;
    public AudioClip[] Audios;
    public GameObject[] ObjectsToSPAWN;
    public GameObject Audio;
    public GameObject VisualController;
    public GameObject BallController;
    public Shader[] _Shaders;
    public GameObject titleBalls;
    public GameObject Set;
    public Toggle T1;
    public Toggle T2;
    public Slider SL1;
   
    public void WhenApplyClicked()
    {   
        if(VisualController.activeSelf == true)
        {
            VisualController.SetActive(false);
            for (int i = 0; i < Set.transform.childCount; i++)
            {
                Destroy(Set.transform.GetChild(i).gameObject);
            }


        }
      
        


        Canvas.SetActive(false);
        Canvas1.SetActive(true);
        //Music]

        string SongName = DropdownMusic.options[DropdownMusic.value].text;
        string FilterName = Filters.options[Filters.value].text;
        string ObjectName = ObjectShapes.options[ObjectShapes.value].text;
        if (SongName == "Jon Hopkins - Emerald Rush")
        {
            Audio.GetComponent<AudioSource>().clip = Audios[1];
           
        }
        if (SongName == "Duke - So In Love With You")
        {
            Audio.GetComponent<AudioSource>().clip = Audios[0];

        }
        if (SongName == "Manila - Maribou State")
        {
            Audio.GetComponent<AudioSource>().clip = Audios[2];

        }
        Audio.GetComponent<AudioSource>().Play();

        //Projection Objects
        if(ObjectName=="Quads")
        {
            titleBalls.GetComponent<Rigidbody>().mass = 1;
            BallController.GetComponent<BallBehaviour>()._ballspeed = 500;
            if (FilterName == "Sobel")
            {
                VisualController.GetComponent<WebCameraFeed2>().ObjectToSpawn = ObjectsToSPAWN[0];

            }
            else
            {
                VisualController.GetComponent<WebCameraFeed2>().ObjectToSpawn = ObjectsToSPAWN[3];
            }
            VisualController.GetComponent<WebCameraFeed2>()._numberOfQuads = 100;
            VisualController.GetComponent<WebCameraFeed2>()._xSpacing = 1;
            VisualController.GetComponent<WebCameraFeed2>()._ySpacing = 1;
            VisualController.GetComponent<WebCameraFeed2>()._movePixel = true;
            VisualController.GetComponent<WebCameraFeed2>().zlocalDistance = 0;
        }
        if (ObjectName == "Spheres")
        {
            VisualController.GetComponent<WebCameraFeed2>()._movePixel = false;
            titleBalls.GetComponent<Rigidbody>().mass = 2;
            BallController.GetComponent<BallBehaviour>()._ballspeed = 1000;
            if (FilterName == "Sobel")
            {
                VisualController.GetComponent<WebCameraFeed2>().ObjectToSpawn = ObjectsToSPAWN[1];

            }
            else
            {
                VisualController.GetComponent<WebCameraFeed2>().ObjectToSpawn = ObjectsToSPAWN[2];

            }
            VisualController.GetComponent<WebCameraFeed2>().zlocalDistance = -100;

            VisualController.GetComponent<WebCameraFeed2>()._numberOfQuads = 10000;
            VisualController.GetComponent<WebCameraFeed2>()._xSpacing = 0.1f;
            VisualController.GetComponent<WebCameraFeed2>()._ySpacing = 0.1f;
        }
        
        
        VisualController.SetActive(true);
        BallController.SetActive(true);
       
       
    }
    public void WhenSettingClicked()
    {
        Canvas.SetActive(true);
        Canvas1.SetActive(false);

    }
   
    public void WhenToggledT2()
    {
        if (T2.isOn == true)
        {
            VisualController.GetComponent<WebCameraFeed2>().Is3D = true;
        }
        if (T2.isOn == false)
        {
            VisualController.GetComponent<WebCameraFeed2>().Is3D = false;

        }


    }
    public void WhenToggeledT1()
    {   
     
         VisualController.GetComponent<WebCameraFeed2>()._reset = true;


    }
    private void Update()
    {
        VisualController.GetComponent<WebCameraFeed2>()._strength = SL1.value;
        VisualController.GetComponent<WebCameraFeed2>()._reset = false;

        T1.isOn = false;
    }
}
