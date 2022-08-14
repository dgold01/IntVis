using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManager1 : MonoBehaviour
{
    private void Awake()
    {
       
    }
    public GameObject TC;
    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        bool _finsihed = TC.GetComponent<TitleController>().returnIfFinish();
        if (_finsihed == true)
        {
            StartCoroutine(ChangeScene());

        }
            
    }

    IEnumerator ChangeScene()
    {
        
       
            yield return new WaitForSeconds(5);
            SceneManager.LoadScene(1);
            yield return new WaitForSeconds(0.5f);
            SceneManager.SetActiveScene(SceneManager.GetSceneByBuildIndex(1));
            SceneManager.UnloadSceneAsync(0);
            
    }


    
   
}
