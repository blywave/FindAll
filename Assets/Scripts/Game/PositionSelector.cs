using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PositionSelector : MonoBehaviour
{
    public Vector3[] _positions = new Vector3[15];  //позициии для утановки дощечек
    [Space]
    public Vector3 _upperLeftPos = new Vector3(-1.04f, -3f, 0.12f); //позиция первой дощечки
    [Space]
    public Vector2 _indentation = new Vector2(1.04f, 1.2f); //расстояние между дощечками x - z; y - z.
    [Space]
    public Transform _plankContainer;
    public float _TimePlankStart = 3;                     //время до начала установки первой таблички
    public float _DeltaTimePlankStart = 0.4f;                //время до начала установки следующей таблички (относительно предыдущей)



    void Start()
    {
        SetPositions();
        for (int i = 0; i < _positions.Length; i++)
        {
            _plankContainer.GetChild(i).GetComponent<PlankEngine>().SetPosition(_positions[i], _TimePlankStart + (_DeltaTimePlankStart * i + 1));
        }
    }

    public void SetPositions() //заполняем массив позициями
    {
        _positions[0] = _upperLeftPos;
        int xNum = 1;
        for (int i = 1; i < _positions.Length; i++) //перебираем элементы массива
        {
            if (xNum <= 2)                            //горизонтальное заполнение
            {
                _positions[i] = _positions[i - 1] + Vector3.right * _indentation.x;
                xNum += 1;
            }
            else                                     //вертикальное заполнение
            {

                _positions[i] = _positions[i - 1] - Vector3.forward * _indentation.y - Vector3.right * (2 * _indentation.x);
                xNum = 1;
            }
        }
    }
}
