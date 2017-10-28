using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InnerViewManager : MonoBehaviour
{
	private Planet planetAt;
	private ResourceManager manager;

	public Text foodAndFuelText;
	public Renderer innerViewRenderer;
	public Text planetNameText;

	public void InitializeManager(Planet planetArrivedAt, ResourceManager manager)
	{
		planetAt = planetArrivedAt;
		this.manager = manager;

		innerViewRenderer.material = planetArrivedAt.material;

		planetNameText.text = "Planet " + planetArrivedAt.name;

		UpdateUI();
	}

	private void UpdateUI()
	{
		foodAndFuelText.text = "Food:\n" + planetAt.CurrentFood + "\nFuel:\n" + planetAt.CurrentFuel;
	}

	public void CollectFood()
	{
		if (planetAt.CurrentFood >= 0)
		{
			planetAt.CurrentFood = planetAt.CurrentFood - 1;
			manager.AddFood(1);
			manager.CalculateExcursion();
		}
		UpdateUI();
	}

	public void CollectFuel()
	{
		if (planetAt.CurrentFuel >= 0)
		{
			planetAt.CurrentFuel = planetAt.CurrentFuel - 1;
			manager.AddFuel(1);
			manager.CalculateExcursion();
		}
		UpdateUI();
	}

	public void LeavePlanet()
	{
		if (manager.CanLeavePlanet())
		{
			//TODO
		}
	}
}
