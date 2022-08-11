using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shader_behaviour : MonoBehaviour
{   public AudioSpectrum _FFT;
    
    public Shader _shaderBlue;
    public Shader  _shaderRed;
    public Shader _shadergreen;
    // Start is called before the first frame update
    void Start()
    {
        _FFT = GameObject.FindWithTag("Audio").GetComponent<AudioSpectrum>();
    }

    // Update is called once per frame
    void Update()
    {   

        if(_FFT._AmplitudeBuffer>0 && _FFT._AmplitudeBuffer<=0.33)
        {
            GetComponent<MeshRenderer>().material.shader = _shaderBlue;

        }
        if (_FFT._AmplitudeBuffer > 0.33 && _FFT._AmplitudeBuffer <= 0.66)
        {
            GetComponent<MeshRenderer>().material.shader = _shaderRed;

        }
        if (_FFT._AmplitudeBuffer > 0.66 && _FFT._AmplitudeBuffer <= 1)
        {
            GetComponent<MeshRenderer>().material.shader = _shadergreen;

        }
    }
}
