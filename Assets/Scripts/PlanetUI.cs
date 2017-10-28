using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetUI : MonoBehaviour {

	void OnMouseDown()
	{
		FindObjectOfType<OuterViewManager>().GoToPlanet();
	}

	void OnMouseOver()
	{
		FindObjectOfType<OuterViewManager>().SelectPlanet(this.gameObject);
	}
}
