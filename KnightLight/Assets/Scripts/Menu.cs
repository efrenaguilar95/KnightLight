using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public void PlayGame()
	{
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
	}

	public void SeeCredits()
	{
		SceneManager.LoadScene("Credits");
	}

	public void QuitGame()
	{
		Application.Quit();
	}

	public void ReturnToStart()
	{
		SceneManager.LoadScene(0);
	}
}
