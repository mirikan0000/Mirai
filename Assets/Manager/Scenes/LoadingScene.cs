using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class LoadingScene : MonoBehaviour
{
    [SerializeField] private GameObject _loadingUI;
    [SerializeField] private Slider _slider;
    [SerializeField] private string NextSceneName;
    public void LoadNextScene()
    {
        if (_loadingUI==null&& _loadingUI.activeSelf==false)
        {
            _loadingUI.SetActive(true);
        }
        StartCoroutine(LoadScene());
    }
    
   IEnumerator LoadScene()
    {
        AsyncOperation async = UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(NextSceneName);
        while (!async.isDone)
        {
            _slider.value = async.progress;
            yield return null;
        }
    }
}