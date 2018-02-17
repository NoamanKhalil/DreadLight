using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class loadingManager : MonoBehaviour {

    public int levelIndexToLoad = 1;


    public Slider loadingBar;
    public GameObject loadingUI;

	void Start () 
    {
        loadingBar.value = 0;
        LoadLevel(levelIndexToLoad);
	}

    public void LoadLevel(int index)
    {
        StartCoroutine(Load(index));
    }

    IEnumerator Load(int index)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(index);

        loadingUI.SetActive(true);

        while(!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / 0.9f);
            loadingBar.value = progress;
            yield return null;
        }

    }
}
