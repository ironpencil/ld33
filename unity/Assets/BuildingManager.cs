using UnityEngine;
using System.Collections;

public class BuildingManager : MonoBehaviour {
    
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        CheckForBuildings();
	}

    public void CheckForBuildings()
    {
        bool buildingsLeft = true;

        if (transform.childCount == 0)
        {
            buildingsLeft = false;
        }

        if (!buildingsLeft)
        {
            //monster wins, reset game
            Globals.Instance.endGameHandler.MonsterWon();
        }
    }
}
