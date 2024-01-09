using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartSceneManager : MonoBehaviour
{
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F5)) PlayerPrefs.DeleteAll();

        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetMouseButtonDown(1)) Instantiate(Resources.Load("Quit_Canvas"));
    }
    public void StartBtn()
    {
        SceneManager.LoadScene(1);
    }
}
