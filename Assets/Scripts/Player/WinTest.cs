﻿using UnityEngine;
using System.Collections;

public class WinTest : MonoBehaviour {

	WinLoseManager winLoseManager;

	public void InitializeVariables()
	{
		winLoseManager = transform.root.Find("Scene manager").GetComponent<WinLoseManager>();
	}

	void OnTriggerEnter(Collider other)
	{
		if (other.transform.name.Contains("Bee")) { winLoseManager.PlayerWins(); }
	}
}
