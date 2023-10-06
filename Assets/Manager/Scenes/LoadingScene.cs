using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class LoadingScene : MonoBehaviour
{
    [SerializeField] private GameObject _loadingUI;
    [SerializeField] private Slider _slider;
    private AsyncOperation async;
    public void LoadNextScene()
    {
        _loadingUI.SetActive(true);
        StartCoroutine(LoadScene());
    }
    IEnumerator LoadScene()
    {
        float progress = SceneManager.Instance.LoadingProgress;

        while (progress!=1.0f)
        {
            Debug.Log(progress);
            _slider.value = progress;
            yield return null;
        }
        if (progress==1.0f)
        {
            Debug.Log("ì«Ç›çûÇ›äÆóπ");
        }
    }
}