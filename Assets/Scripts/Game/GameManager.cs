using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public List<GameObject> _planks = new List<GameObject>();
    public List<GameObject> _set = new List<GameObject>(); //до трех элементов, заполняется выбираемыми дощечками
    public UIManager _uiManager;
    public GameInputManager _gameInput;
    public int _maxPlanks = 15;
    public int _score;
    public int _bestScore;
    public int _heals = 5;
    public float _visibleTime = 3;
    

    
    // Start is called before the first frame update
    void Start()
    {
        _bestScore = PlayerPrefs.GetInt("best");
    }

    public void AddInputObj(GameObject obj)
    {
        _planks.Add(obj);
        PlanksReady();
    }
    public void PlanksReady()
    {
        if (_planks.Count == _maxPlanks)
        {
            for (int i = 0; i < _planks.Count; i++)
            {
                _planks[i].GetComponentInChildren<PlankEngine>().AnimationClipChange("StartOpen", true);
            }
            Invoke("PlanksClosing", _visibleTime);
        }
    }
    public void PlanksClosing()
    {
        for (int i = 0; i < _planks.Count; i++)
        {
            _planks[i].GetComponentInChildren<PlankEngine>().AnimationClipChange("StertClose", true);
        }
        _gameInput.enabled = true;
    }
    public void GetPlank(GameObject pl, string tag)
    {
        if (_set.Count < 3)
        {
            if (_set.Count == 0 || tag == _set[0].tag)
            {
                pl.GetComponent<PlankEngine>().AnimationClipChange("Open", true);
                pl.GetComponentInChildren<BoxCollider>().enabled = false;
                if (!_set.Contains(pl)) _set.Add(pl);
                if (_set.Count > 1) AddScore(_set.Count);
            }
            else
            {
                if (!_set.Contains(pl)) _set.Add(pl);
                pl.GetComponent<PlankEngine>().AnimationClipChange("Nope", true);
                pl.GetComponentInChildren<BoxCollider>().enabled = false;
                Invoke("Misstake", 1.2f);
            }
        }
    }
    public void AddScore(int k)
    {
        
        switch (k)
        {
            case 2:
                _score += 1 * _heals;
                break;
            case 3:
                _score += 3 * _heals;
                PlankSetComplite();
                break;
        }
        if (_score > _bestScore)
        {
            _bestScore = _score;
        }
        _uiManager.Scorer(_score);

    }
    public void Misstake()
    {
        _heals--;
        _uiManager.Damage(_heals);
        _gameInput.enabled = false;
        Invoke("WaitInput", 0.3f) ;
        if (_set.Count > 0)
        {
            for (int i = 0; i < _set.Count; i++)
            {
                _set[i].GetComponent<PlankEngine>().AnimationClipChange("Idle", true);
                _set[i].GetComponentInChildren<BoxCollider>().enabled = true;
            }
            
        }
        _set.Clear();
        if (_heals <= 0)
        {
            Debug.Log("Die");
            GameFailed();
        }

    }
    private void WaitInput()
    {
        _gameInput.enabled = true;
    }
    public void PlankSetComplite() //если собраны все три плитки одной группы
    {
        for (int i = 0; i < _set.Count; i++)
        {
            _planks.Remove(_set[i]);
            Destroy(_set[i], 1f);
        }
        _set.Clear();
        if (_planks.Count <= 0) Win();
    }
    private void GameFailed()
    {
        _gameInput.enabled = false;
        _uiManager.EndGame(_score, _bestScore, false);
    }
    private void Win()
    {
        _gameInput.enabled = false;
        _uiManager.EndGame(_score, _bestScore, true);
    }

}
