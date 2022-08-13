using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class collider : MonoBehaviour
{
    [SerializeField] private bool _hasreached;
    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.transform.GetSiblingIndex() == gameObject.transform.GetSiblingIndex())
        {
          
           
            _hasreached = false;
        }
    }
    public bool _hasreachedvalue()
    {
        return _hasreached;
    }

}
