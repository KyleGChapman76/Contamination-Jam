using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OuterViewManager : MonoBehaviour
{
	[SerializeField]
	private GameObject selectedPlanetObject;
	private Planet selectedPlanet;

	[SerializeField]
	private Camera gameCamera;

	[SerializeField]
	private Vector3 cameraOffset;

	private FullGameManager fullGameManager;

	[SerializeField]
	private string innerViewSceneName;

	void Start ()
	{
		DontDestroyOnLoad(this.gameObject);
		MoveCamera();

		selectedPlanet = selectedPlanetObject.GetComponent<Planet>();

		fullGameManager = GameObject.FindObjectOfType<FullGameManager>();
		fullGameManager.currentOccupiedPlanet = selectedPlanet;
	}

	public void SelectPlanet(GameObject planetObject)
	{
		Planet planetToSelect = planetObject.GetComponent<Planet>();
		if (planetToSelect == fullGameManager.currentOccupiedPlanet || fullGameManager.currentOccupiedPlanet.CanJumpFromThisToThere(planetToSelect))
		{
			selectedPlanetObject = planetObject;
			selectedPlanet = selectedPlanetObject.GetComponent<Planet>();
			MoveCamera();
		}
	}

	public void MoveCamera()
	{
		gameCamera.transform.position = selectedPlanetObject.transform.position + cameraOffset;
	}

	public void GoToSelectedPlanet()
	{
		if (fullGameManager.currentOccupiedPlanet.CanJumpFromThisToThere(selectedPlanet))
		{
			fullGameManager.currentOccupiedPlanet = selectedPlanet;
			fullGameManager.CalculateJump();
			DontDestroyOnLoad(selectedPlanetObject);
			SceneManager.LoadScene(innerViewSceneName);
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
		if (scene.name.Equals(innerViewSceneName))
		{
			FindObjectOfType<InnerViewManager>().InitializeManager(selectedPlanet, this, fullGameManager);
		}
	}
}
