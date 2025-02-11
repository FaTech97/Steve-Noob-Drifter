using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemAnimation : MonoBehaviour
{
	[SerializeField] private Vector3 finalPosition;
	private Vector3 initialPosition;

	private void Awake()
	{
		initialPosition = transform.position - new Vector3(-100, 0, 0);
	}

	private void Update()
	{
		transform.position = Vector3.Lerp(transform.position, finalPosition, 0.1f);
		transform.RotateAround(gameObject.transform.position, Vector3.up, 20 * Time.deltaTime);
	}

	private void OnDisable()
	{
		transform.position = initialPosition;
	}
}
