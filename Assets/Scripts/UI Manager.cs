using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    Text score;
    Text max_score;
    bool end;
    Canvas end_canvas;
    // Start is called before the first frame update
    void Start()
    {
        score = transform.Find("score").GetComponent<Text>();
        max_score = transform.Find("max_score").GetComponent <Text>();
        end = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetMouseButtonDown(1))
        {
            Instantiate(Resources.Load("Quit_Canvas"));
            GameManager.STOP();
        }
        score.text = GameManager.Get<int>(KEY.SCORE).ToString();
        max_score.text = PlayerPrefs.GetInt("MaxScore") > GameManager.Get<int>(KEY.SCORE) ? PlayerPrefs.GetInt("MaxScore").ToString() : GameManager.Get<int>(KEY.SCORE).ToString();
        if (GameManager.IsGameOver && !end)
        {
            end = true;
            GameEndUI();
        }
    }

    void GameEndUI()
    {
        if(GameManager.Get<int>(KEY.SCORE) > PlayerPrefs.GetInt("MAxScore")) PlayerPrefs.SetInt("MaxScore", GameManager.Get<int>(KEY.SCORE));

        end_canvas = Instantiate(Resources.Load<Canvas>("End_Canvas"));
        end_canvas.transform.GetComponentInChildren<Button>().onClick.AddListener(()=>TryAgainBtn());
    }

    void TryAgainBtn()
    {
        SceneManager.LoadScene(0);
        GameManager.init();
        Destroy(end_canvas.gameObject);
    }
}
