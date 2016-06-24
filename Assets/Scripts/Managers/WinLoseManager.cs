using UnityEngine;
using System.Collections;

public class WinLoseManager : MonoBehaviour {

	public void PlayerWins()
	{
		Debug.Log("You win! Happiness and joy are yours.");
	}

	public IEnumerator PlayerLoses()
	{
		yield return StartCoroutine(GetComponent<OpenClose>().GoToWaitLoc());

		Debug.Log("You lose! The hopelessness of your love is spirit-crushing.");
	}
}
