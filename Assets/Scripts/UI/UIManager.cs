using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public GameObject _endPanel;
    public GameObject _background;
    public GameObject _isBestScoreText;
    public GameObject _topPanel;
    public GameObject _healthBar;
    public Text _scoreInGameText;
    public Text _scoreText;
    public Text _besrScoreText;
    public Text _resultText;
    public List<Image> _hearts = new List<Image>();
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < _healthBar.transform.childCount; i++)
        {
            _hearts.Add(_healthBar.transform.GetChild(i).GetComponent<Image>());
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void EndGame(int score, int bScore, bool result)
    {
        
        _background.SetActive(true);
        _endPanel.SetActive(true);
        //_topPanel.SetActive(false);
        if (result)
        {
            if (bScore > PlayerPrefs.GetInt("best"))
            {
                bScore = score;
                _besrScoreText.text = "ЛУЧШИЙ РЕЗУЛЬТАТ: " + bScore.ToString();
                _isBestScoreText.SetActive(true);
            } 
            _scoreText.text = "ВАШ РЕЗУЛЬТАТ: " + score.ToString();
            _besrScoreText.text = "ЛУЧШИЙ РЕЗУЛЬТАТ: " + bScore.ToString();
            _resultText.text = "ВЫ ПОБЕДИЛИ!";
        }
        else
        {
            if (bScore > PlayerPrefs.GetInt("best"))
            {
                bScore = score;
                _besrScoreText.text = "ЛУЧШИЙ РЕЗУЛЬТАТ: " + bScore.ToString();
                _isBestScoreText.SetActive(true);
            }
            _scoreText.text = "ВАШ РЕЗУЛЬТАТ: " + score.ToString();
            _besrScoreText.text = "ЛУЧШИЙ РЕЗУЛЬТАТ: " + bScore.ToString();
            _resultText.text = "ВЫ ПРОИГРАЛИ!";
        }
        PlayerPrefs.SetInt("best", bScore);
    }
    public void RePlay()
    {
        SceneManager.LoadScene(1);
    }
    public void GoBack()
    {
        SceneManager.LoadScene(0);
    }
    public void Scorer(int scr)
    {
        _scoreInGameText.text = scr.ToString();
    }
    public void Damage(int health)
    {
        for (int i = health; i < _healthBar.transform.childCount; i++)
        {
            _hearts[i].color = Color.white;
        }
    }
}
