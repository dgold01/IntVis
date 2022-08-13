using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManager1 : MonoBehaviour
{
    private void Awake()
    {
        SceneManager.UnloadSceneAsync(1);
    }
    public GameObject TC;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(ChangeScene());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator ChangeScene()
    {
        bool _finsihed = TC.GetComponent<TitleController>().returnIfFinish();
        if (_finsihed == true)
        {   

            yield return new WaitForSeconds(5);
            SceneManager.LoadScene(1);
            
        }


    }
   
}
