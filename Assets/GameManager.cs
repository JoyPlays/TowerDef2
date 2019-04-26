using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
	private bool gameEnded = false;
	public GameObject gameOverUI;

	// Update is called once per frame
	void Update()
    {
		if (gameEnded)
		{
			return;
		}
		if (PlayerStats.Lives <= 0)
		{
			EndGame();
		}    
    }

	void EndGame()
	{
		gameEnded = true;
		Debug.Log("Game Over!");
		gameOverUI.SetActive(true);

	}
}
