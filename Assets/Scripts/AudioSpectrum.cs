using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;



//[RequireComponent(typeof(AudioSource))]
public class AudioSpectrum : MonoBehaviour
{   
    
    AudioSource _audioSource;
    private float[] _samplesLeft = new float[512];
    private float[] _samplesRight = new float[512];
    public float _SmoothDownRate = 0;

    //Audio8
    private float[] _freqBand = new float[8];
    private float[] _bandBuffer = new float[8];
    private float[] _freqBandHighest = new float[8];
    //Audio64
    private float[] _freqBand64 = new float[64];
    private float[] _bandBuffer64 = new float[64];
    private float[] _freqBandHighest64 = new float[64];
    
    [HideInInspector]
    public float[] _audioBand, _audioBandBuffer;

    [HideInInspector]
    public float[] _audioBand64, _audioBandBuffer64;
  
    [HideInInspector]
    public float _Amplitude, _AmplitudeBuffer;
    private float _AmplitudeHighest;
    public float _AudioProfile;
    public float _amplitude;

    //Microphone input
   // public AudioMixer _audioMixer;
   
    public string _selectedDevice;
    public AudioClip _audioClip;
    public bool _useMicrophone;
    public enum _channel
    {
        Stereo,
        Left,
        Right,

    }
   
    public _channel channel = new _channel();
    // Start is called before the first frame update
    void Start()
    {
      
         




        _audioBand = new float[8];
        _audioBandBuffer = new float[8];
        _audioBand64 = new float[64];
        _audioBandBuffer64 = new float[64];
        _audioSource = GetComponent<AudioSource>();
        AudioProfile(_AudioProfile);
        AudioProfile64(_AudioProfile);


        GetMicAudio();
      



    }
    void CreateAudioBands()
    {
        for (int i = 0; i < 8; i++)
        {
            if (_freqBand[i] > _freqBandHighest[i])
            {
                _freqBandHighest[i] = _freqBand[i];

            }
            _audioBand[i] = (_freqBand[i] / _freqBandHighest[i]);
            _audioBandBuffer[i] = (_bandBuffer[i] / _freqBandHighest[i]);


        }



    }
    void CreateAudioBands64()
    {
        for (int i = 0; i < 64; i++)
        {
            if (_freqBand64[i] > _freqBandHighest64[i])
            {
                _freqBandHighest64[i] = _freqBand64[i];

            }
            _audioBand64[i] = (_freqBand64[i] / _freqBandHighest64[i]);
            _audioBandBuffer64[i] = (_bandBuffer64[i] / _freqBandHighest64[i]);


        }



    }
    // Update is called once per frame
    void Update()
    {

        
        GetSpectrumAudioSource();
        MakeFrequencyBands();
        MakeFrequencyBands64();
        BandBuffer64();
        BandBuffer();
        CreateAudioBands();
        CreateAudioBands64();
        GetAmplitude();
    }
    
    void GetSpectrumAudioSource()
    {
        _audioSource.GetSpectrumData(_samplesLeft, 0, FFTWindow.Blackman);
        _audioSource.GetSpectrumData(_samplesRight, 1, FFTWindow.Blackman);
    }
    void GetMicAudio()
    {
        //Microphone input
        if (_useMicrophone)
        {

            if (Microphone.devices.Length > 0)
            {
                
                _selectedDevice = Microphone.devices[0].ToString();
                _audioSource.clip = Microphone.Start(_selectedDevice, true, 10, AudioSettings.outputSampleRate);
                _audioClip = _audioSource.clip;

            }
            else
            {
                _useMicrophone = false;

            }

        }
        if (!_useMicrophone)
        {

            _audioClip = _audioSource.clip;



        }

        _audioSource.Play();

    }
    void BandBuffer()
    {
        for (int i = 0; i < 8; i++)
        {
            if (  _freqBand[i] > _bandBuffer[i])
            {
                _bandBuffer[i] = _freqBand[i];
               
            }
            else
            {
                _bandBuffer[i] = Mathf.Lerp(_bandBuffer[i], _freqBand[i], _SmoothDownRate);
            }
        }


    }
    void BandBuffer64()
    {
        for (int i = 0; i < 64; i++)
        {
            if (_freqBand64[i] > _bandBuffer64[i])
            {
                _bandBuffer64[i] = _freqBand64[i];

            }
            else
            {
                _bandBuffer64[i] = Mathf.Lerp(_bandBuffer64[i], _freqBand64[i], _SmoothDownRate);
            }
        }


    }
    void AudioProfile(float audioProfile)
    {
        for (int i = 0; i < 8; i++)
        {
            _freqBandHighest[i] = audioProfile;
        }


    }

    void AudioProfile64(float audioProfile)
    {
        for (int i = 0; i < 8; i++)
        {
            _freqBandHighest64[i] = audioProfile;
        }


    }
    void GetAmplitude()
    {
        float _CurrentAmplitude = 0;
        float _CurrentAmplitudeBuffer = 0;
        for (int i = 0; i < 8; i++)
        {
            _CurrentAmplitude += _audioBand[i];
            _CurrentAmplitudeBuffer += _audioBandBuffer[i];
        }
        if(_CurrentAmplitude>_AmplitudeHighest)
        {

            _AmplitudeHighest = _CurrentAmplitude;
        }
        _Amplitude = _CurrentAmplitude / _AmplitudeHighest;
        _AmplitudeBuffer = _CurrentAmplitudeBuffer / _AmplitudeHighest;
        _amplitude = _AmplitudeBuffer;
        Shader.SetGlobalFloat("Amplitude", _amplitude);
    }

    void MakeFrequencyBands()
    {
        /* 22050/512 = 43hertz per sample
         * 
         * 20-60 hertz
         * 60-250 hertz
         * 250-500 hetz
         * 500-2000 hertz
         * 2000-4000 hertz
         * 4000-6000 hertz
         * 6000-20000 hertz
         * 0 - 2 = 86 hertz
         * 1 - 4 = 172 hertz - 87 to 258
         * 2 - 8 = 344 hertz - 259 to 602
         * 3 - 16 = 688 hertz - 603 to 1290 
         * 4 - 32 = 1376       - 1291 to 2666                
         * 5 - 64 = 2752        - 2667 to 5418
         * 6 - 128 = 5504       - 5419 to 10922
         * 7 - 256 = 11008      - 10923-21930
         * 510
         */
        int count = 0;
        for (int i = 0; i < 8; i++)
        {
            float average = 0;
            int sampleCount = (int)Mathf.Pow(2, i)*2;
            if(i==7)
            {
                sampleCount += 2;

            }
           
            for (int j = 0; j < sampleCount; j++)
            {
                if(channel==_channel.Stereo)
                {
                    average += _samplesLeft[count] + _samplesRight[count];

                }
                if (channel == _channel.Left)
                {
                    average += _samplesLeft[count];
                }
                if(channel==_channel.Right)
                {

                    average += _samplesRight[count];
                }

                count++;
            }
            average /= count;

            _freqBand[i] = average * 10;

        }
    }
    void MakeFrequencyBands64()
    {
        /* 22050/512 = 43hertz per sample
         * 0-15 = 1 sample = 16
         * 16-31 = 2 sample = 32
         * 32-39 = 4 sample = 32
         * 40-47 = 6 samples = 48
         * 48-55 = 16 samples = 128
         * 56-63 = 32 samples = 256 +
         *                    ---
         *                    =512
         * 
         * 510
         */
        int count = 0;
        int sampleCount = 1;
        int power = 0;

        for (int i = 0; i < 64; i++)
        {
            float average = 0;

            if (i == 16 || i == 32 || i == 40 || i == 48 || i == 56)
            {
                power++;
                sampleCount = (int)Mathf.Pow(2, power);
                if (power == 3)
                    sampleCount -= 2;
            }

            for (int j = 0; j < sampleCount; j++)
            {
                if (channel == _channel.Stereo)
                {
                    average += _samplesLeft[count] + _samplesRight[count];

                }
                if (channel == _channel.Left)
                {
                    average += _samplesLeft[count];
                }
                if (channel == _channel.Right)
                {

                    average += _samplesRight[count];
                }
                count++;
            }

            average /= count;
            _freqBand64[i] = average * 80;
        }

    }
    

}