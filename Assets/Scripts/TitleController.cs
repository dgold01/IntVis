using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class TitleController : MonoBehaviour
{

    public GameObject SphereParent;
    GameObject[] _balls;
    public GameObject ObjectToSpawn;
    float _xSpacing;
    float _ySpacing;
    public float _maxMagnitude;
    public GameObject _titleObjects;
    public float _attractionForce;
    // Start is called before the first frame update
    void Start()
    {   
        _balls = new GameObject[_titleObjects.transform.childCount];
        for (int i = 0; i < _balls.Length; i++)
        {
            
            GameObject newball = Instantiate(ObjectToSpawn);
            _balls[i] = newball;
            newball.transform.parent = SphereParent.transform;
            newball.transform.position = new Vector3(i,i,i);
           
            
           

            
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        for (int i = 0; i < _balls.Length; i++)
        {
            Vector3 targetVec = _titleObjects.transform.GetChild(i).transform.position;
            Vector3 forcedirection = -_balls[i].transform.position + targetVec;
            bool _hasreached = _titleObjects.transform.GetChild(i).GetComponent<collider>()._hasreachedvalue();
            if (_hasreached == true) 
            {
                _balls[i].GetComponent<Rigidbody>().velocity = forcedirection *0.5f;

            }
            else
            {

                _balls[i].GetComponent<Rigidbody>().AddForce(forcedirection * _attractionForce);
            }
            
           
            if (_balls[i].GetComponent<Rigidbody>().velocity.magnitude > _maxMagnitude)
            {
                _balls[i].GetComponent<Rigidbody>().velocity = _balls[i].GetComponent<Rigidbody>().velocity.normalized * _maxMagnitude;
            }




        }
    }
    
}
