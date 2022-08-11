using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class WebCameraFeed : MonoBehaviour
{
    WebCamTexture webCamTexture;
    public AudioSpectrum _FFT;
 
    Color[] Pixels;

    public GameObject ObjectToSpawn;
    GameObject[] _Quads;
    public GameObject _Set;
    public float _xSpacing;
    public float _ySpacing;
    Color[][] PixelArray;
    bool _newLine;
    bool _newSquare;
    bool NewLineSquare;
    public bool IsExploding;
    public bool IsScaling;
    public bool Is3D;
    public float _strength;
    public float _speed;
    public float _Speed;
    Vector3[] _originalPos;
    Vector3[] _angles;
    bool[] isLerping;
    bool _isLerpingAll;
    Vector3[] TargetPos;
    Quaternion[] TargetQua;
    Vector3[] OriginPos;
    public bool _reset;
    public bool _movePixel;
    public float _timeDelay;

    void Start()
    {
        
        
        Vector3 center = _Set.transform.position;
        _angles = new Vector3[180];
        for (int i = 0, a = 0; i < 360; i += 2)
        {
            Quaternion rot = Quaternion.AngleAxis(i, center);
            _angles[a] = center + rot * new Vector3(0, 1, 0);
            a++;
        }

        _Quads = new GameObject[100];
        TargetPos = new Vector3[_Quads.Length];
        TargetQua = new Quaternion[_Quads.Length];
        OriginPos = new Vector3 [_Quads.Length];
        isLerping = new bool[_Quads.Length];
        _originalPos = new Vector3[_Quads.Length];
        webCamTexture = new WebCamTexture();
        
       
        webCamTexture.Play();
        for (int i = 0, x = 0, y = 0; i < _Quads.Length; i++)
        {
            
            if (i % 10 == 0)
            {
                y++;
                x = 0;
            }
            GameObject newQuad = Instantiate(ObjectToSpawn);
            newQuad.transform.parent = _Set.transform;
            newQuad.transform.localPosition = new Vector3(x * _xSpacing, y * _ySpacing, 0);
            _Quads[i] = newQuad;
            TargetQua[i] = _Quads[i].transform.rotation;

            x++;
        }
        if(_movePixel)
        {
            StartCoroutine(StartMovePixelsToCamera());
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        SplitWebCam();
        AnimateObjects();
        



        if(_isLerpingAll)
        {
            for (int i = 0; i < _Quads.Length; i++)
            {
                _isLerpingAll = false;
                if (isLerping[i]==true)
                {
                    MovePixelsToCamera(_Quads[i], OriginPos[i], TargetPos[i], i);
                    _isLerpingAll = true;
                }
            }


        }

        if(_reset == true)
        {
            for (int i = 0; i < _Quads.Length; i++)
            {
                 _Quads[i].transform.localPosition = TargetPos[i];
                _Quads[i].transform.rotation = TargetQua[i];
                _Quads[i].GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);
                _Quads[i].GetComponent<Rigidbody>().freezeRotation = true;


            }



        }

        
       
    }
    /*private void OnDrawGizmos()
    {
        Vector3 center = new Vector3(0, 0, 0);
        _angles = new Vector3[180];
        for (int i = 0,a=0; i < 360; i += 2)
        {
            Quaternion rot = Quaternion.AngleAxis(i,center);
            _angles[a] = center + rot * Vector3.forward;
            
            Gizmos.DrawLine(center, _angles[a]);
            Gizmos.DrawLine(center, _angles[a+1]);
            a++;
        }
    }
    */
    void SplitWebCam()
    {
        int width = webCamTexture.width;
        int height = webCamTexture.height;
        Pixels = webCamTexture.GetPixels();

        PixelArray = new Color[100][];
        for (int i = 0; i < 100; i++)
        {
            PixelArray[i] = new Color[Pixels.Length / 100];
        }


        int _xspace;
        int _yspace;

        int _num = 0;
        int _Pixelnum = 0;
        int _Num = 1;




        //Splits webcam texture into squares
        for (int y = 0, m = 0, p = 0; y < height; y++)
        {

            for (int x = 0 + _num; x < width + _num; x++)
            {
                _xspace = width / 10;
                _yspace = height / 10;
                //New square on y-xis
                if (x % (width * _yspace) == 0)
                {
                    if (x != 0)
                    {

                        m = 10 * _Num;
                        p = 0;
                        _Num++;
                        _newSquare = true;
                        NewLineSquare = true;
                        _Pixelnum = 0;
                    }
                }
                //New Sqaure on x-axis
                if (x % (_xspace) == 0)
                {
                    if (_newSquare == false)
                    {
                        if (x != 0)
                        {
                            m++;
                            if (_newLine == true)
                            {
                                m--;
                            }

                            p = 0 + (_Pixelnum / 10);

                            if (NewLineSquare == true)
                            {
                                p = 0;
                            }

                        }
                    }
                }


                if (m < 100)
                {
                    if (p < Pixels.Length / 100)
                    {

                        PixelArray[m][p] = Pixels[x];
                    }

                }


                _newSquare = false;
                _newLine = false;

                p++;

            }

            _num += width;
            _Pixelnum += width;
            _newLine = true;
            NewLineSquare = false;
            m -= 9;
        }



        for (int i = 0; i < 100; i++)
        {
            Texture2D texture = new Texture2D(width / 10, height / 10);
            texture.SetPixels(PixelArray[i]);
            texture.Apply();
            _Quads[i].GetComponent<MeshRenderer>().material.mainTexture = texture;
        }



    }

    IEnumerator StartMovePixelsToCamera()
    {
        if (_movePixel)
        {

            for (int i = 53; i < 57; i++)
            {
                OriginPos[i] = _Quads[i].transform.localPosition;
                TargetPos[i] = new Vector3(_Quads[i].transform.localPosition.x, _Quads[i].transform.localPosition.y, _Quads[i].transform.localPosition.z - 22);
                yield return new WaitForSeconds(_timeDelay);
                isLerping[i] = true;
                _isLerpingAll = true;

                MovePixelsToCamera(_Quads[i], OriginPos[i], TargetPos[i], i);
            }

            for (int i = 0; i < 100; i++)
            {   if(i>=53 && i<57 )
                {

                }
                else
                {

                    OriginPos[i] = _Quads[i].transform.localPosition;
                    TargetPos[i] = new Vector3(_Quads[i].transform.localPosition.x, _Quads[i].transform.localPosition.y, _Quads[i].transform.localPosition.z - 22);
                    yield return new WaitForSeconds(_timeDelay);
                    isLerping[i] = true;
                    _isLerpingAll = true;

                    MovePixelsToCamera(_Quads[i], OriginPos[i], TargetPos[i], i);
                }
               
            }


        }



    }
    void MovePixelsToCamera(GameObject i,Vector3 origin, Vector3 target, int index)
    {
        if(_movePixel)
        {
            i.transform.localPosition = Vector3.Lerp(i.transform.localPosition, target, Time.deltaTime * _Speed);
            if (i.transform.localPosition.z <= target.z * 0.6)
            {
                i.transform.localPosition = new Vector3(target.x, target.y, target.z + 8);
                isLerping[index] = false;
              
                TargetPos[index] = i.transform.localPosition;


            }


        }
              
        
            
    }
    void AnimateObjects()
    {
        if (Is3D == true)
        {
            for (int i = 0,m=0; i < _Quads.Length; i++)
            {

               if (m == 63)
               {
                    m = 0;
               }

                _Quads[i].transform.localPosition = TargetPos[i];



                Vector3 _newPos = new Vector3(_Quads[i].transform.localPosition.x, _Quads[i].transform.localPosition.y, _Quads[i].transform.localPosition.z - (_FFT._audioBandBuffer64[m] * _strength));

                _Quads[i].transform.localPosition = Vector3.Lerp(_Quads[i].transform.localPosition, _newPos, Time.deltaTime * _speed);
                 m++;
                
                

            }
        }
        if (IsExploding == true)
        {
            for (int i = 0,a=0; i < _angles.Length; i++)
            {
                _Quads[a].transform.position = _originalPos[a];



                Vector3 _newPos = new Vector3(_angles[i].x*_strength, _angles[i].y*_strength, _originalPos[a].z);

                _Quads[a].transform.position = Vector3.Lerp(_Quads[a].transform.position, _newPos, Time.deltaTime * _speed);

                a++;
                if (a==59)
                {
                    a -= 59;

                }
            }

        }
    }   
}
