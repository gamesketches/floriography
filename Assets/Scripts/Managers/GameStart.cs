using UnityEngine;
using System.Collections;

public class GameStart : MonoBehaviour {

	void Awake()
	{
		GetComponent<OpenClose>().InitializeVariables();
	}

	void Start()
	{
		StartCoroutine(BeginGame());
	}

	IEnumerator BeginGame()
	{
		yield return StartCoroutine(GetComponent<OpenClose>().Open());

		yield return StartCoroutine(GetComponent<OpenClose>().GoToStart());

		yield break;
	}
}
