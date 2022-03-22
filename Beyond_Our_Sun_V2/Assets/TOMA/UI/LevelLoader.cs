using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelLoader : MonoBehaviour
{
	public GameObject loadingScreen;
	public GameObject BoutonMenuControlles;
	//public GameObject MenuControlles;
	public Slider slider;
	public Text ProgressText;
	public bool isloadingScreenActive;
	public bool MenuControllesActive;
	public Image Logo;
	public bool LogoActive;


	void Start()
	{

		Logo.enabled = true;
		LogoActive = true;
	}
	public void LoadLevel(int sceneIndex)
	{
		//AsyncOperation operation = SceneManager.LoadSceneAsync(sceneIndex);
		StartCoroutine(LoadAsynchronously(sceneIndex));

	}

	
	

	IEnumerator LoadAsynchronously(int sceneIndex)
	{
		AsyncOperation operation = SceneManager.LoadSceneAsync(sceneIndex);

		loadingScreen.SetActive(true);

		isloadingScreenActive = true;

		MenuControllesActive = false;


		while (!operation.isDone)
		{
			float progress = Mathf.Clamp01(operation.progress / .9f);
			slider.value = progress;
			ProgressText.text = progress * 100f + "%";

			yield return null;
		}
	}

	void Update()
	{
		if (isloadingScreenActive == true)
		{ 
			BoutonMenuControlles.SetActive(false);
			Logo.enabled = false;
			LogoActive = false;
		}
		if (isloadingScreenActive == false)
		{
			BoutonMenuControlles.SetActive(true);
		}
		if (BoutonMenuControlles == true)
		{
			Logo.enabled = false;
			LogoActive = false;
			isloadingScreenActive = false;
		}
			
			
			

		

	}


}
