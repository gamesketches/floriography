using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class OpenClose : MonoBehaviour {

	Transform petal1;
	Transform petal2;
	Transform petal3;
	Transform petal4;
	Transform petal5;
	Transform petal6;

	List<Transform> petals = new List<Transform>();
	Dictionary<Transform, Vector3> closedPos = new Dictionary<Transform, Vector3>();
	Dictionary<Transform, Vector3> openPos = new Dictionary<Transform, Vector3>();

	public void InitializeVariables()
	{
		AssignPetals();
		SetUpDataStructures();
	}

	public float duration = 3.0f;
	float timer = 0.0f;
	public AnimationCurve animCurve;

	public IEnumerator Open()
	{
		while (timer <= duration)
		{
			timer += Time.deltaTime;

			foreach (Transform petal in petals)
			{
				Vector3 rot = Vector3.Lerp(closedPos[petal], openPos[petal], animCurve.Evaluate(timer/duration));
				petal.localEulerAngles = rot;
			}

			yield return null;
		}

		yield break;
	}

	void AssignPetals()
	{
		petal1 = transform.root.Find("Tulip").Find("Petal 1");
		petal2 = transform.root.Find("Tulip").Find("Petal 2");
		petal3 = transform.root.Find("Tulip").Find("Petal 3");
		petal4 = transform.root.Find("Tulip").Find("Petal 4");
		petal5 = transform.root.Find("Tulip").Find("Petal 5");
		petal6 = transform.root.Find("Tulip").Find("Petal 6");
	}

	void SetUpDataStructures()
	{
		List<GameObject> petalObjs = new List<GameObject>();
		petalObjs.AddRange(GameObject.FindGameObjectsWithTag("Petal"));
		foreach (GameObject petal in petalObjs) { petals.Add(petal.transform); }

		closedPos.Add(petal1, new Vector3(-25.08f, 7.35f, 10.91f));
		closedPos.Add(petal2, new Vector3(0.0f, 20.65f, 0.0f));
		closedPos.Add(petal3, new Vector3(-20.58f, -16.9f, 0.0f));
		closedPos.Add(petal4, new Vector3(0.0f, -26.31f, 0.0f));
		closedPos.Add(petal5, new Vector3(29.7f, 42.4f, 36.9f));
		closedPos.Add(petal6, new Vector3(23.6f, 5.5f, 0.0f));

		openPos.Add(petal1, new Vector3(0.0f,0.0f,0.0f));
		openPos.Add(petal2, new Vector3(0.0f,0.0f,0.0f));
		openPos.Add(petal3, new Vector3(0.0f,0.0f,0.0f));
		openPos.Add(petal4, new Vector3(0.0f,0.0f,0.0f));
		openPos.Add(petal5, new Vector3(15.0f,61.0f,20.0f));
		openPos.Add(petal6, new Vector3(0.0f,0.0f,0.0f));
	}
}
