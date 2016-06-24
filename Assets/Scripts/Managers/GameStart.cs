using UnityEngine;
using System.Collections;

public class GameStart : MonoBehaviour {

	void Awake()
	{
		GetComponent<OpenClose>().InitializeVariables();
		transform.root.Find("UI").Find("Controls").GetComponent<Fade>().InitializeVariables();
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
									transform.root.Find("UI").Find("Controls").GetComponent<Fade>().FadeOut());

		yield break;
	}
}
