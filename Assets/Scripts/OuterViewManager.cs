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

	[SerializeField]
	private ResourceManager resourceManager;

	[SerializeField]
	private string innerViewSceneName;

	void Start ()
	{
		DontDestroyOnLoad(this.gameObject);
		MoveCamera();
	}

	public void SelectPlanet(GameObject planetObject)
	{
		selectedPlanetObject = planetObject;
		selectedPlanet = selectedPlanetObject.GetComponent<Planet>();
		MoveCamera();
	}

	public void MoveCamera()
	{
		gameCamera.transform.position = selectedPlanetObject.transform.position + cameraOffset;
	}

	public void GoToPlanet()
	{
		SceneManager.LoadScene(innerViewSceneName);
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
			FindObjectOfType<InnerViewManager>().InitializeManager(selectedPlanet, resourceManager);
		}
	}
}
