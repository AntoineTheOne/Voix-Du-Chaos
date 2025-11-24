using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangementScene : MonoBehaviour
{

    
    [SerializeField] private string sceneName;

    
    public void ChangeScene()
    {
      
        SceneManager.LoadScene(sceneName);
    }
}