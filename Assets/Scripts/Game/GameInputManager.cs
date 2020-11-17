using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameInputManager : MonoBehaviour
{
    GameManager _gm;
    void Start()
    {
        _gm = this.GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(touch.position);

            if (Physics.Raycast(ray, out hit))
            {
                if (touch.phase == TouchPhase.Ended && hit.transform.parent.tag != "Untagged")
                {
                    _gm.GetPlank(hit.transform.parent.gameObject, hit.transform.parent.gameObject.tag);
                }
            }
        }
    }
}
