using UnityEngine;
using System.Collections;

public class GameStart : MonoBehaviour {

	void Awake()
	{
		GetComponent<OpenClose>().InitializeVariables();
	}

	void Start()
	{
		StartCoroutine(GetComponent<OpenClose>().Open());
	}
}
