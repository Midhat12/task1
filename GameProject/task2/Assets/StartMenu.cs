using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
//Khadeeja Gilani
public class StartMenu : MonoBehaviour
{
	public void StartM()
	{
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
		
	}
	public void ExitG()
	{
		Application.Quit();
	}
}
