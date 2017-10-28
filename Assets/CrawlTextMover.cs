using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CrawlTextMover : MonoBehaviour
{
	public float speed;
	public string mainScene;
	public int waitingTime;

	void Update ()
	{
		transform.Translate(Vector3.up * speed * Time.deltaTime);

		StartCoroutine(MoveToGameAfterTime());
	}

	private IEnumerator MoveToGameAfterTime()
	{
		yield return new WaitForSeconds(waitingTime);
		MoveToGame();
	}

	public void MoveToGame()
	{
		SceneManager.LoadScene(mainScene);
	}
}
