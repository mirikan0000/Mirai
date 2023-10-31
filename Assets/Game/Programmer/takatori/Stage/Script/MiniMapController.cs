using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniMapController : MonoBehaviour
{
    [Header("Mini Map Camera")]
    [SerializeField] private Transform miniMapCameraTransform;
    [SerializeField] private Vector3 defaultMiniMapCameraPosition;
    [SerializeField] private float miniMapCameraMoveSpeed;

    [Header("Player Objects")]
    [SerializeField] private GameObject player1Object;
    [SerializeField] private GameObject player2Object;
    [SerializeField] private GameObject player1FakeObject;
    [SerializeField] private GameObject player2FakeObject;

    public PlayerManager_ player1Controller;
    public PlayerManager_ player2Controller;

    private bool playersInSameMap;

    public List<GameObject> MapList;
    private void Start()
    {
        defaultMiniMapCameraPosition = gameObject.transform.position;
        miniMapCameraTransform.position = defaultMiniMapCameraPosition;
        miniMapCameraMoveSpeed = 10f;
    }

    private void Update()
    {
        GetPlayerControllers();
        ComparePlayerMaps();
        MoveMiniMapCamera();
        //UpdatePlayerFakeObjectScale();
    }

    private void GetPlayerControllers()
    {
        player1Controller = player1Object.GetComponent<PlayerManager_>();
        player2Controller = player2Object.GetComponent<PlayerManager_>();
    }

    private void ComparePlayerMaps()
    {
        if (player1Controller.CurrentMap == player2Controller.CurrentMap)
        {
            playersInSameMap = true;
        }
        else
        {
            playersInSameMap = false;
        }
    }

    private void MoveMiniMapCamera()
    {
        if (playersInSameMap)
        {
            Vector3 targetPosition = CalculateCameraTargetPosition(player1Controller.CurrentMap);
            miniMapCameraTransform.position = new Vector3(targetPosition.x,targetPosition.y, targetPosition.z);
        }
        else
        {
            miniMapCameraTransform.position = Vector3.MoveTowards(
                miniMapCameraTransform.position, defaultMiniMapCameraPosition, miniMapCameraMoveSpeed );
        }
    }

    private Vector3 CalculateCameraTargetPosition(int mapNumber)
    {
        return MapList[mapNumber].transform.position;


    }

    private void UpdatePlayerFakeObjectScale()
    {
        float scaleFactor = playersInSameMap ? 2.0f : 1.0f;
        player1FakeObject.transform.localScale = new Vector3(scaleFactor, 0.2f, scaleFactor);
        player2FakeObject.transform.localScale = new Vector3(scaleFactor, 0.2f, scaleFactor);
    }
}
