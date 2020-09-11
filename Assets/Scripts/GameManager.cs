using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public float waitAfterDying = 2f;
    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;

    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            PauseUnpause();
        }
    }

    public void PlayerDied()
    {
        StartCoroutine(PlayerDiedCoroutine());
    }

    public IEnumerator PlayerDiedCoroutine()
    {
        yield return new WaitForSeconds(waitAfterDying);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void PauseUnpause()
    {
        if(UIController.instance.pauseScreen.activeInHierarchy)
        {
            UIController.instance.pauseScreen.SetActive(false);
            Cursor.lockState = CursorLockMode.Locked;
            Time.timeScale = 1f;
        } else
        {
            UIController.instance.pauseScreen.SetActive(true);
            Cursor.lockState = CursorLockMode.None;
            Time.timeScale = 0f;
        }
    }

}
