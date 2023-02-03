using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SplashManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Invoke(nameof(ChangeScene),5.0f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void ChangeScene()
    {
        SceneManager.LoadScene(1);
        SceneManager.SetActiveScene(SceneManager.GetSceneByBuildIndex(1));
    }
}
