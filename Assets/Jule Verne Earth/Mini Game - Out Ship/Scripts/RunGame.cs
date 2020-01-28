using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunGame : MonoBehaviour {
	public GameObject[] gameObjects;
	public EscapeFromShip _escapeFromShip;
	public RandomSpawn _randomSpawn;

	public void OnMouseDown(){
		Run ();
	}
    void Start()
    {
        Run();
    }
    public void Run(){
        if (gameObjects != null)
        {
            for (int i = 0; i < gameObjects.Length; i++)
            {
                gameObjects[i].SetActive(false);
            }
        }
        gameObjects[1].SetActive(true);
        _randomSpawn.AddingInfoPointsBad ();
		_randomSpawn.AddingInfoPointsGood ();
		_randomSpawn.MergeInfoPointsPositions ();
		_escapeFromShip.OnGame ();
	}
}
