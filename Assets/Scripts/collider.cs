using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class collider : MonoBehaviour
{
    [SerializeField] private bool _hasreached;
    public GameObject Ding;
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Mouse"))
        {
            Ding.GetComponent<AudioSource>().Play();

        }
        if (other.gameObject.transform.GetSiblingIndex() == gameObject.transform.GetSiblingIndex())
        {
          
           
            _hasreached = true;
        }
    }
    public bool _hasreachedvalue()
    {
        return _hasreached;
    }

}
