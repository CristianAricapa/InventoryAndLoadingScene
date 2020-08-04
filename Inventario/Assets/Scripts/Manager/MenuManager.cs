using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    public Text ScenePercentText;
    public Image ScenePercentImage;

    // Use this for initialization
    void Start()
    {
        StartCoroutine(LoadScene());
    }


    IEnumerator LoadScene()
    {
        yield return null;

        AsyncOperation asyncScene = SceneManager.LoadSceneAsync(1);
        asyncScene.allowSceneActivation = false;

        while (!asyncScene.isDone)
        {
            ScenePercentText.text = (asyncScene.progress * 100) + "%";
            ScenePercentImage.fillAmount = asyncScene.progress;

            if (asyncScene.progress >= 0.9f)
            {
                asyncScene.allowSceneActivation = true;
            }

            yield return null;
        }
    }
}
