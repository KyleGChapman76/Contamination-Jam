using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceManager : MonoBehaviour
{
	public const int STARTING_PEOPLE = 100;
	public const int STARTING_FOOD = 50;
	public const int STARTING_FUEL = 20;

	public const int FOOD_PER_PERSON_PER_TIME = 1;
	public const int STARVATION_RATIO = 2; //1 over this number is the percentage of starvers that die
	public const int GROWTH_RATE = 1;

	public const int TIME_FOR_JUMP = 5;
	public const int TIME_PER_SPREAD_OF_CONTAMINATION = 15;

	public const int FUEL_TO_JUMP = 3;
	public const int PEOPLE_TO_WIN = 2;

	public const double FOOD_TO_FUEL_RATIO = .5f;

	private int currentTime;
	private int currentPeople;
	private int currentFood;
	private int currentFuel;

	private int mostRecentlyUpdatedTime;

	public void Start()
	{
		DontDestroyOnLoad(gameObject);

		currentPeople = STARTING_PEOPLE;
		currentFood = STARTING_FOOD;
		currentFuel = STARTING_FUEL;
		currentTime = 0;
	}

	public void CalculateExcursion()
	{
		currentTime += 1;
		UpdateResources();
	}

	public void CalculateJump()
	{
		currentTime += TIME_FOR_JUMP;
		currentFuel -= FUEL_TO_JUMP;

		UpdateResources();
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

	private void UpdateResources()
	{
		int timeDelta = currentTime - mostRecentlyUpdatedTime;

		if (timeDelta <= 0)
		{
			print("Uh oh");
			return;
		}

		//for each unit of time, simulate the resource changes
		for (int i = 0; i < timeDelta && currentPeople > 0; i++)
		{
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
		}

		//check if the player has lost the game
		if (currentPeople == 0)
		{
			LoseGame();
		}

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
}
