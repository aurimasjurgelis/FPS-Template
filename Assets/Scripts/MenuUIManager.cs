using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuUIManager : MonoBehaviour
{
    public string loadLevelName = "TestScene";
    public TextMeshProUGUI versionText;

    public void Start()
    {
        versionText.text = "Ver. " + Application.version;
    }

    public void PlayGame()
    {
        SceneManager.LoadScene(loadLevelName);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

}
