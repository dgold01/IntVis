using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class WebCameraFeed2 : MonoBehaviour
{


    Vector3[] TargetPos;
    Quaternion[] TargetQua;
    Vector3[] OriginPos;
    public int _childnumber;
    WebCamTexture webCamTexture;
    public AudioSpectrum _FFT;
    public GameObject _Set;
    public GameObject ObjectToSpawn;
    GameObject[] _Quads;
    public float _xSpacing;
    public float _ySpacing;
    public GameObject object1;
    public bool _reset;
    public bool _movePixel;
    public float _timeDelay;
    bool[] isLerping;
    bool _isLerpingAll;
    public int _numberOfQuads;
    public float zlocalDistance;

    public float _Speed;
    [Header("3D Settings")]
    public bool Is3D;
    public float _speed;
    public float _strength;



    void Start()
    {


        webCamTexture = new WebCamTexture();
        webCamTexture.Play();

        CreateObjects();



    }
    void CreateObjects()
    {
        _Quads = new GameObject[_numberOfQuads];
        TargetQua = new Quaternion[_Quads.Length];
        
        isLerping = new bool[_Quads.Length];
        TargetPos = new Vector3[_Quads.Length];
        OriginPos = new Vector3[_Quads.Length];

        //gameObject.GetComponent<MeshRenderer>().material.mainTexture = webCamTexture;
        //object1.GetComponent<MeshRenderer>().material.mainTexture = webCamTexture;
        
        for (int i = 0, x = 0, y = 0, uvY = -1; i < _Quads.Length; i++)
        {
            if (i % Mathf.Sqrt(_numberOfQuads) == 0)
            {
                y++;
                x = 0;
                uvY++;
            }
            GameObject newQuad = Instantiate(ObjectToSpawn);
            newQuad.transform.parent = _Set.transform;
            newQuad.transform.localPosition = new Vector3(x * _xSpacing, y * _ySpacing, zlocalDistance);
            _Quads[i] = newQuad;
            TargetQua[i] = _Quads[i].transform.rotation;
            TargetPos[i] = _Quads[i].transform.localPosition;
            
            //newQuad.GetComponent<MeshRenderer>().material = new Material(newQuad.GetComponent<MeshRenderer>().material);


            newQuad.GetComponent<MeshRenderer>().material.SetFloat("_numx", x);

            newQuad.GetComponent<MeshRenderer>().material.SetFloat("_numy", uvY);

            x++;
        }
        if (_movePixel)
        {

            StartCoroutine(StartMovePixelsToCamera());

        }

    }
    private void OnEnable()
    {
        CreateObjects();
    }
    private void Update()
    {

        AnimateObjects();

        for (int i = 0; i < _Quads.Length; i++)
        {

            _Quads[i].GetComponent<MeshRenderer>().material.mainTexture = webCamTexture;

        }
        if (_isLerpingAll)
        {
            for (int i = 0; i < _Quads.Length; i++)
            {
                _isLerpingAll = false;
                if (isLerping[i] == true)
                {
                    MovePixelsToCamera(_Quads[i], OriginPos[i], TargetPos[i], i);
                    _isLerpingAll = true;
                }
            }


        }

        if (_reset == true)
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
    IEnumerator StartMovePixelsToCamera()
    {
       

        for (int i = 0; i < _Quads.Length; i++)
        {
            
            {

                OriginPos[i] = _Quads[i].transform.localPosition;
                TargetPos[i] = new Vector3(_Quads[i].transform.localPosition.x, _Quads[i].transform.localPosition.y, _Quads[i].transform.localPosition.z - 100);
                yield return new WaitForSeconds(_timeDelay);
                isLerping[i] = true;
                _isLerpingAll = true;

                MovePixelsToCamera(_Quads[i], OriginPos[i], TargetPos[i], i);
            }

        }


        
    }
    void MovePixelsToCamera(GameObject i, Vector3 origin, Vector3 target, int index)
    {
        if (_movePixel)
        {
            i.transform.localPosition = Vector3.Lerp(i.transform.localPosition, target, Time.deltaTime * _Speed);
            if (i.transform.localPosition.z <= target.z * 0.9)
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
            for (int i = 0, m = 0; i < _Quads.Length; i++)
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
    }   

}