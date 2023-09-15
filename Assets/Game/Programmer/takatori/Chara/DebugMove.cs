using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugMove : MonoBehaviour
{
    Vector3 Pos;
    [SerializeField]
    float MoveZ;
    [SerializeField]
    float MoveX;
    // Start is called before the first frame update
    void Start()
    {
        Pos = this.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
       
        if (Input.GetKey(KeyCode.UpArrow))
        {
            Debug.Log("GetW");
            transform.position += new Vector3(0,0,MoveZ);
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            Debug.Log("GetA");
            transform.position += new Vector3(-MoveX, 0,0 );
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            Debug.Log("GetD");
            transform.position += new Vector3(MoveX, 0,0);
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            Debug.Log("GetS");
            transform.position += new Vector3(0, 0, -MoveZ);
        }
    }
}
