using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class InnerViewManager : MonoBehaviour
{
	private Planet planetAt;
	private OuterViewManager outerViewManager;
	private FullGameManager fullGameManager;

	public Text foodAndFuelText;
	public Renderer innerViewRenderer;
	public Text planetNameText;

	public string outerViewSceneName;

	public void InitializeManager(Planet planetArrivedAt, OuterViewManager outerViewManager, FullGameManager fullGameManager)
	{
		planetAt = planetArrivedAt;

		this.outerViewManager = outerViewManager;
		this.fullGameManager = fullGameManager;

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
			planetAt.CurrentFood = planetAt.CurrentFood - 50;
			fullGameManager.AddFood(50);
			fullGameManager.CalculateExcursion();
		}
		UpdateUI();
	}

	public void CollectFuel()
	{
		if (planetAt.CurrentFuel >= 0)
		{
			planetAt.CurrentFuel = planetAt.CurrentFuel - 50;
			fullGameManager.AddFuel(50);
			fullGameManager.CalculateExcursion();
		}
		UpdateUI();
	}

	public void LeavePlanet()
	{
		if (fullGameManager.CanLeavePlanet())
		{
			fullGameManager.CalculateJump();
			Destroy(outerViewManager);
			Destroy(planetAt.gameObject);
			SceneManager.LoadScene(outerViewSceneName);
		}
	}
}
