using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;


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
    public void WhenApplyClicked()
    {   

        Canvas.SetActive(false);
        Canvas1.SetActive(true);
        //Music
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
            VisualController.GetComponent<WebCameraFeed2>().ObjectToSpawn = ObjectsToSPAWN[0];
            VisualController.GetComponent<WebCameraFeed2>()._numberOfQuads = 100;
            VisualController.GetComponent<WebCameraFeed2>()._xSpacing = 1;
            VisualController.GetComponent<WebCameraFeed2>()._ySpacing = 1;
        }
        if (ObjectName == "Spheres")
        {
            VisualController.GetComponent<WebCameraFeed2>().ObjectToSpawn = ObjectsToSPAWN[1];
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
}
