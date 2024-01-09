using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RightTurnPos : MonoBehaviour
{
    [SerializeField]
    [Header("マネージャー関係")]
    private GameObject managerObj;
    private Tittle_Manager managerScript;

    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        GetManagerScript();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision != null)
        {
            if (collision.gameObject.tag == "Player1")
            {
                Debug.Log(managerScript.playerTurnFlag);

                managerScript.playerTurnFlag = true;
            }
        }
    }

    private void GetManagerScript()
    {
        managerObj = GameObject.Find("Manager");
        managerScript = managerObj.GetComponent<Tittle_Manager>();
    }
}
