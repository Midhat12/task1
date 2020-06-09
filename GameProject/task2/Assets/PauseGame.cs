using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
//Khadeeja Gilani
public class PauseGame : MonoBehaviour
{
	public void pause()
	{
		//Time.timeScale = 0;
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 2);
		
	}
	
	public void resume()
	{
		//Time.timeScale = 1;
		SceneManager.LoadScene(1);
		
	}

	public void exit()
	{
		SceneManager.LoadScene(0);
	}
}
