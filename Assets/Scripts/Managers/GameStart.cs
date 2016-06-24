using UnityEngine;
using System.Collections;

public class GameStart : MonoBehaviour {

	void Awake()
	{
		GetComponent<OpenClose>().InitializeVariables();
		transform.root.Find("UI").Find("Controls").GetComponent<FadeImage>().InitializeVariables();
	}

	void Start()
	{
		StartCoroutine(BeginGame());
	}

	IEnumerator BeginGame()
	{
		yield return StartCoroutine(GetComponent<OpenClose>().Open());

		yield return StartCoroutine(GetComponent<OpenClose>().GoToStart());

		yield return StartCoroutine(
									transform.root.Find("UI").Find("Controls").GetComponent<FadeImage>().FadeOut());

		yield break;
	}
}
