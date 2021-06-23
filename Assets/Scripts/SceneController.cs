using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    public static string KeyWord;

    public void Start()
    {


    }
    public void Quit()
    {
        Application.Quit();
        //Debug.Log("Application Quit");
    }
    public void ChangeTheScene(string sceneName)
    {
        SceneManager.LoadSceneAsync(sceneName);
    }
    public void AskGoogle(string keywordName)
    {
        KeyWord = keywordName;
        Debug.Log(KeyWord);
    }

}