using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public string trainingScene = "TrainingScene";
    public static LevelManager Instance;
	public string Scene_Mission = "Scene_Mission";
	public string MenuScene = "MenuScene";

	//[SerializeField] private GameObject_loaderCanvas;
	//[SerializeField] private ImageConversion crosshair_spaceshipt_White;
	//private float _target;



	//ici c'est pour changer de scene dans le jeu y suffit de rajouter une fonction similaire a goToTrainingScene pour ça

	public void goToMenuScene()
    {
        SceneManager.LoadScene(MenuScene);
    }

	/*private void Awake()
	{
		if (Instance == null)
		{
            Instance = this;
            DontDestroyOnLoad(gameObject);
		}
		else
		{
			Destroy(gameObject);

		}
	}

	public async void LoadScene(string MenuScene)
	{
		_target = 0;
		crosshair_spaceshipt_White.FillAmount = 0;
		var scene = SceneManager.LoadSceneAsync(MenuScene);
		scene.allowSceneActivation = false;

		_loaderCanvas.SetActive(true);

		do
		{
			await Task.Delay(100);

			_target = scene.progress;
			while (scene.progress < 0.9f);
		}

		scene.allowSceneActivation = true;
		__loaderCanvas.SetActive(false);

	}

	private void Update()
	{
		crosshair_spaceshipt_White.FillAmount = Mathf.MoveTowards(crosshair_spaceshipt_White.FillAmount, _target, 3 * Time.deltaTime);
	}*/
}
