using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TitleController : MonoBehaviour
{
    GameObject[] _balls;
    public GameObject ObjectToSpawn;
    float _xSpacing;
    float _ySpacing;
    public GameObject _titleObjects;
    public float _attractionForce;
    // Start is called before the first frame update
    void Start()
    {   _balls = new GameObject[_titleObjects.transform.childCount];
        for (int i = 0; i < _balls.Length; i++)
        {
            
            GameObject newball = Instantiate(ObjectToSpawn);
            _balls[i] = newball;
           
            
           

            
        }
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < _balls.Length; i++)
        {

           
            Vector3 targetVec = _titleObjects.transform.GetChild(i).transform.position;
            _balls[i].GetComponent<Rigidbody>().AddForce((-(_balls[i].transform.position) + targetVec) * _attractionForce);




        }
    }
}
