using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceManager : MonoBehaviour
{
	public const int STARTING_PEOPLE = 100;
	public const int STARTING_FOOD = 50;
	public const int STARTING_FUEL = 20;
	//public static const int STARTING_MINERAL = 100;

	public const int FOOD_PER_PERSON_PER_TIME = 1;
	public const int STARVATION_RATIO = 2; //1 over this number of the percentage of starvers that die
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

	public void JumpToNewPlanet()
	{
		currentTime += TIME_FOR_JUMP;
		currentFuel -= FUEL_TO_JUMP;

		UpdateResources();
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
		for (int i = 0; i < timeDelta; i++)
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
}
