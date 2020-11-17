using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoleSetter : MonoBehaviour
{
    public int[] _rolesCounts = new int[5];     //0 - A; 1 - B; 2 - C; 3 - D; 4 - E;
    public List<GameObject> _planks = new List<GameObject>();
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            _planks.Add(transform.GetChild(i).gameObject);
        }
        StartRoleDistribution();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void StartRoleDistribution() //распределение троек 
    {
        for (int i = 0; i < 5; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                SendRole(Random.Range(0, _planks.Count), i);
            }
        }
    }
    public void SendRole(int numInList, int type) 
    {


        switch (type)
        {
            case 0: //A
                _planks[numInList].GetComponent<PlankEngine>().SetRole("A");                
                break;
            case 1: //B
                _planks[numInList].GetComponent<PlankEngine>().SetRole("B");
                break;
            case 2: //C
                _planks[numInList].GetComponent<PlankEngine>().SetRole("C");
                break;
            case 3: //D
                _planks[numInList].GetComponent<PlankEngine>().SetRole("D");
                break;
            case 4: //E
                _planks[numInList].GetComponent<PlankEngine>().SetRole("E");
                break;
        }
        _planks.Remove(_planks[numInList]);

    }
}
