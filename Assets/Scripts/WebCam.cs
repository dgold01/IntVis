using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WebCam: MonoBehaviour
{
    WebCamTexture webCamTexture;
    // Start is called before the first frame update
    void Start()
    {
        webCamTexture = new WebCamTexture();
        webCamTexture.Play();

    }

    // Update is called once per frame
    void Update()
    {
        gameObject.GetComponent<MeshRenderer>().material.mainTexture = webCamTexture;

    }
}
