
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseManager : SingletonMonoBehaviour<PauseManager>
{
    public string inputStopActionMapName;
    public bool isPause;

    private GameObject canvasObj;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    /// <summary>
    /// �|�[�Y�ɂ���
    /// (�E�v���C���[���͂��~�߂�)
    /// </summary>
    public void PauseOn()
    {
        InputManager.Instance.SetStopActionMap(inputStopActionMapName, true);
        Time.timeScale = 0;

        //canvasObj = GameObject.FindGameObjectWithTag("Canvas");
        isPause = true;
        if (canvasObj != null)
        {
            canvasObj.SetActive(false);
        }
    }
    /// <summary>
    /// �|�[�Y����
    /// (�E�v���C���[���͂����)
    /// </summary>
    public void PauseOff()
    {
        InputManager.Instance.SetStopActionMap(inputStopActionMapName, false);
        Time.timeScale = 1;

        isPause = false;
        if (canvasObj != null)
        {
            canvasObj.SetActive(true);
        }
    }
}
