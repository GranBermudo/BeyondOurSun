/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelLoader : MonoBehaviour
{
    public void LoadLevel (int sceneIndex)
	{
		//AsyncOperation operation = SceneManager.LoadSceneAsync(sceneIndex);
	StartCoroutine(LoadAsynchronously(sceneIndex));

	public GameObject loadingScreen;
    public Slider slider;
	public Text ProgressText;

	}

	IEnumerator LoadAsynchronously (int sceneIndex)
	{
	AsyncOperation operation = SceneManager.LoadSceneAsync(sceneIndex);

	loadingScreen.SetActive(true);

		while (!operation.isDone)
		{
	    float progress = Mathf.Clamp01(operation.progress / .9f);
		slider.value = progress;
		ProgressText.text = progress * 100f + "%";

		yield return null;
		}
	}
}
*/