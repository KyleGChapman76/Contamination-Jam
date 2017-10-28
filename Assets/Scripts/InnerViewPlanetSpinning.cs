using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InnerViewPlanetSpinning : MonoBehaviour
{
	public float rotationSpeed = .1f;

	public void Update()
	{
		transform.RotateAround(transform.position, Vector3.up, Time.deltaTime * rotationSpeed);
	}
}
