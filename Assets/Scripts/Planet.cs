using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Planet : MonoBehaviour
{
	[SerializeField]
	private int STARTING_FOOD;

	[SerializeField]
	private int STARTING_FUEL;

	public int CurrentFood
	{
		get
		{
			return currentFood;
		}
		set
		{
			if (value >= 0)
			{
				currentFood = value;
			}
			else
			{
				Debug.Log("Can't set the current food value of a planet to a negative value.");
			}
		}
	}
	private int currentFood;

	public int CurrentFuel
	{
		get
		{
			return currentFuel;
		}
		set
		{
			if (value >= 0)
			{
				currentFuel = value;
			}
			else
			{
				Debug.Log("Can't set the current fuel value of a planet to a negative value.");
			}
		}
	}
	private int currentFuel;

	public Material regularMaterial;
	public Material currentMaterial;
	public Material contaminatedMaterial;
	public string name;

	public bool IsContaminated
	{
		get
		{
			return isContaminated;
		}
		set
		{
			isContaminated = value;
			if (isContaminated)
			{
				currentMaterial = contaminatedMaterial;
			}
		}
	}
	private bool isContaminated;

	public Planet[] listOfJumpablePlanets;
	private HashSet<Planet> setOfJumpablePlanets;

	void Start ()
	{
		currentFood = STARTING_FOOD;
		currentFuel = STARTING_FUEL;

		foreach (Planet pla in listOfJumpablePlanets)
		{
			setOfJumpablePlanets.Add(pla);
		}

		regularMaterial = currentMaterial;
	}

	public bool CanJumpFromThisToThere(Planet destination)
	{
		if (destination.isContaminated)
		{
			return false;
		}
		return setOfJumpablePlanets.Contains(destination);
	}
}
