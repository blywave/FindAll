using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuUIManager : MonoBehaviour
{
    public Text _bestScoreText;
    // Start is called before the first frame update
    void Start()
    {
        if(PlayerPrefs.GetInt("best") != 0)
        {
            _bestScoreText.text = "ЛУЧШИЙ РЕЗУЛЬТАТ: " + PlayerPrefs.GetInt("best").ToString();
        } else { _bestScoreText.enabled = false; }
    }

    // Update is called once per frame
    public void Play()
    {
        SceneManager.LoadScene(1);
    }
}
