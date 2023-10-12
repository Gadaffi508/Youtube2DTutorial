using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public Text current_text;
    public static GameManager instance;

    private void Awake() => instance = this;

    public void CurrentTextWrite(string text_key)
    {
        current_text?.gameObject.SetActive(true);
        current_text.text = text_key;
    }

    public void LoadScene(int loadScene)
    {
        SceneManager.LoadScene(loadScene);
        Time.timeScale = 1;
    }
}
