using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlankEngine : MonoBehaviour
{
    public Vector3 _position;
    public Animator _animator;
    public SpriteRenderer _sprite;
    public float _movingSpeed = 0.3f;
    public float _timeTostart;
    public bool _isMooving = false;
    public string _typeSprytePath;

    public UnityEvent PlankReady;

    void Update()
    {
        if (_isMooving)
        {
            transform.position = Vector3.Lerp(transform.position, _position, _movingSpeed * Time.deltaTime);
            if (transform.position == _position) 
            {
                //AnimationClipChange("Idle", true);
                PlankReady.Invoke();
                _isMooving = false;

            }
        }
    }
    public void SetPosition(Vector3 pos, float time)
    {
        _position = pos;
        Invoke("Moving", time);
    }
    public void Moving()
    {
        _isMooving = !_isMooving;
        AnimationClipChange("GoOnPosition", true);
    }

    public void AnimationClipChange(string anim, bool state) 
    {
        CloseAnimTransitions();
        switch (anim)
        {
            case "GoOnPosition":
                _animator.SetBool("OnPos", state);
                break;
            case "Open":
                _animator.SetBool("Open", state);
                break;
            case "StartOpen":
                _animator.SetBool("StartOpen", state);
                break;
            case "StertClose":
                _animator.SetBool("StertClose", state);
                break;
            case "Idle":
                _animator.SetBool("Idle", state);
                break;
            case "Nope":
                _animator.SetBool("Nope", state);
                Invoke("CloseAnimTransitions", 0.3f);
                break;
        }
    }
    public void SetRole(string role)
    {
        gameObject.tag = role;
        _sprite.sprite = Resources.Load<Sprite>(_typeSprytePath + role);
    }
    public void CloseAnimTransitions()
    {
        for (int i = 0; i < _animator.parameterCount; i++)
        {
            string parName = _animator.GetParameter(i).name;
            _animator.SetBool(parName, false);

        }
    }
}
