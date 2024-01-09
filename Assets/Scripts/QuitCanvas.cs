using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuitCanvas : MonoBehaviour
{
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape) || Input.GetMouseButtonDown(1)) EndGame();
    }
    public void GoBack()
    {
        GameManager.STOP();
        Destroy(gameObject);
    }
    public void EndGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
