using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallBehaviour : MonoBehaviour
{
    private Camera cam;
    public GameObject objecttospawn;
    bool _pressed;
    public float _ballspeed;
    public float _ballsize;
    public float _speed;
    public Vector3 _mouseposition;

    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
    }

    // Update is called once per frame
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {

            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit))
            {   
                gameObject.transform.position = hit.point;
                GameObject _ball = Instantiate(objecttospawn);
                _ball.transform.position = new Vector3(hit.transform.position.x, hit.transform.position.y, gameObject.transform.parent.position.z);
                _ball.transform.localScale = new Vector3(_ballsize, _ballsize, _ballsize);
                Vector3 forcedir = -_ball.transform.position + hit.transform.position;
                _ball.GetComponent<Rigidbody>().AddForce(_ball.transform.forward * _ballspeed);
            }
        }    
    }           
   




}
