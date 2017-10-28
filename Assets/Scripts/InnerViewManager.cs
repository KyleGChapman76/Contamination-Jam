using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class InnerViewManager : MonoBehaviour
{
	private Planet planetAt;
	private FullGameManager manager;

	public Text foodAndFuelText;
	public Renderer innerViewRenderer;
	public Text planetNameText;

	public string outerViewSceneName;

	public void InitializeManager(Planet planetArrivedAt, FullGameManager manager)
	{
		planetAt = planetArrivedAt;
		this.manager = manager;

		innerViewRenderer.material = planetArrivedAt.currentMaterial;

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
			manager.CalculateJump();
			SceneManager.LoadScene(outerViewSceneName);
		}
	}
}
