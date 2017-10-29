using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FullGameManager : MonoBehaviour
{
	public const int STARTING_PEOPLE = 20;
	public const int STARTING_FOOD = 1000;
	public const int STARTING_FUEL = 20;

	public const int FOOD_PER_PERSON_PER_TIME = 1;
	public const int STARVATION_RATIO = 2; //1 over this number is the percentage of starvers that die
	public const int GROWTH_RATE = 1;

	public const int TIME_FOR_JUMP = 5;
	public const int TIME_PER_SPREAD_OF_CONTAMINATION = 10;

	public const int FUEL_TO_JUMP = 3;
	public const int PEOPLE_TO_WIN = 2;

	public const double FOOD_TO_FUEL_RATIO = .5f;

	private int currentPeople;
	private int currentFood;
	private int currentFuel;

	public Planet currentOccupiedPlanet;

	private int currentTime;
	private int timeOfNextContamination;

	private Text timingText;
	private Text resourceText;

	private int mostRecentlyUpdatedTime;

	public void Start()
	{
		DontDestroyOnLoad(gameObject);

		currentPeople = STARTING_PEOPLE;
		currentFood = STARTING_FOOD;
		currentFuel = STARTING_FUEL;
		currentTime = 0;

		timeOfNextContamination = TIME_PER_SPREAD_OF_CONTAMINATION + 2;
	}

	public void Update()
	{
		if (timingText != null)
		{
			timingText.text = "Day: " + currentTime + "   Day of next contamination: " + timeOfNextContamination;
		}
		if (resourceText != null)
		{
			resourceText.text = "People: " + currentPeople + " Fuel: " + currentFuel + " Food: " + currentFood;
		}
	}

	public void CalculateExcursion()
	{
		currentTime += 1;
		UpdateResourcesAndContamination();
	}

	public void CalculateJump()
	{
		currentTime += TIME_FOR_JUMP;
		currentFuel -= FUEL_TO_JUMP;

		UpdateResourcesAndContamination();
	}

	public void AddFood(int foodAdded)
	{
		if (foodAdded <= 0)
		{
			Debug.Log("Trying to add 0 or negative food to the resources.");
		}
		else
		{
			currentFood += foodAdded;
		}
	}

	public void RemoveFood(int foodRemoved)
	{
		if (foodRemoved <= 0)
		{
			Debug.Log("Trying to remove 0 or negative food from the resources.");
		}
		else
		{
			currentFood -= foodRemoved;
			if (currentFood < 0)
			{
				currentFood = 0;
			}
		}
	}

	public void AddFuel(int fuelAdded)
	{
		if (fuelAdded <= 0)
		{
			Debug.Log("Trying to add 0 or negative fuel to the resources.");
		}
		else
		{
			currentFuel += fuelAdded;
		}
	}

	public void RemoveFuel(int fuelRemoved)
	{
		if (fuelRemoved <= 0)
		{
			Debug.Log("Trying to remove 0 or negative fuel from the resources.");
		}
		else
		{
			currentFuel -= fuelRemoved;
			if (currentFuel < 0)
			{
				currentFuel = 0;
			}
		}
	}

	private void UpdateResourcesAndContamination()
	{
		int timeDelta = currentTime - mostRecentlyUpdatedTime;

		if (timeDelta <= 0)
		{
			print("Uh oh");
			return;
		}

		//for each unit of time, simulate the resource changes and contamination changes
		for (int i = 0; i < timeDelta && currentPeople > 0; i++)
		{
			//food consumption
			int foodConsumed = currentPeople * FOOD_PER_PERSON_PER_TIME;
			if (foodConsumed >= currentFood) //people are starving
			{
				int peopleFed = currentFood / FOOD_PER_PERSON_PER_TIME;
				int peopleUnfed = currentPeople - peopleFed;
				int peopleStarving = peopleUnfed / STARVATION_RATIO;
				currentPeople -= peopleStarving;
			}
			else
			{
				currentFood -= foodConsumed;
				currentPeople += GROWTH_RATE; //flat people growth
			}

			if (mostRecentlyUpdatedTime + i == timeOfNextContamination)
			{
				ContaminatePlanet();
				//TODO choose next contamination time
				timeOfNextContamination = mostRecentlyUpdatedTime + i + TIME_PER_SPREAD_OF_CONTAMINATION;
			}
		}

		//check if the player has lost the game
		if (currentPeople == 0)
		{
			LoseGame();
		}

		//if current planet is contaminated, you lose the game
		//TODO

		mostRecentlyUpdatedTime = currentTime;
	}

	private void LoseGame()
	{
		//TODO
	}

	public bool CanLeavePlanet()
	{
		return currentFuel >= FUEL_TO_JUMP;
	}

	public void ContaminatePlanet()
	{
		bool success = false;
		Planet[] planets = FindObjectsOfType<Planet>();
		foreach (Planet planetCandidate in planets)
		{
			if (!planetCandidate.IsContaminated)
			{
				return;
			}
			foreach (Planet planetToContaminateCandidate in planetCandidate.listOfJumpablePlanets)
			{
				if (!planetToContaminateCandidate.IsContaminated)
				{
					planetToContaminateCandidate.IsContaminated = true;
					success = true;
				}
			}
		}

		if (!success)
		{
			Debug.Log("Couldn't contaminate a planet!");
		}
	}

	void OnEnable()
	{
		SceneManager.sceneLoaded += OnSceneLoaded;
	}

	void OnDisable()
	{
		SceneManager.sceneLoaded -= OnSceneLoaded;
	}

	private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
	{
		resourceText = GameObject.FindGameObjectWithTag("ResourcesText").GetComponent<Text>();
		timingText = GameObject.FindGameObjectWithTag("TimingText").GetComponent<Text>();
	}
	
}
