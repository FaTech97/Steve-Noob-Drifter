using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowVersions : MonoBehaviour
{
	private Text text;

	void Start()
	{
		text = GetComponent<Text>();
		text.text = $"--version-{Application.version}-U-{Application.unityVersion}";
	}
}
