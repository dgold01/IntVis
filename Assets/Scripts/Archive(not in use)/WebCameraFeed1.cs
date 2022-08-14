using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class WebCameraFeed1 : MonoBehaviour
{
    WebCamTexture webCamTexture;
    public GameObject _Quad;
    public Color[] Pixels;
    public Color[] newPixels;
   


    void Start()
    {
        webCamTexture = new WebCamTexture();
        Renderer renderer = GetComponent<Renderer>();
        renderer.material.mainTexture = webCamTexture;
        webCamTexture.Play();
        
    }

    // Update is called once per frame
    void Update()
    {   int width = webCamTexture.width;
        int height = webCamTexture.height;
        Pixels = webCamTexture.GetPixels();
        newPixels = new Color[Pixels.Length];
        int _xspace;
        int _yspace;
        int _num = 0;
        int _Num = 1;

        for (int y = 0; y < height; y++)
        {
          
            for (int x = 0+_num; x < width+_num; x++)
            {
                _xspace = width / 10;
                _yspace = height / 10;
                if (x % (_xspace) == 0 || x % (_yspace) == 0)
                {
                    _Num++;
                }
                if (_Num % 2 == 0)
                {
                    newPixels[x] = Pixels[x];
                }
                else
                {
                    newPixels[x] = new Color(0, 0, 0);
                }

            }
            _num+=width;
        }
        Texture2D texture = new Texture2D(width,height);
        texture.SetPixels(newPixels);
        texture.Apply();
        _Quad.GetComponent<MeshRenderer>().material.mainTexture = texture;
        
       






     
    }
}
