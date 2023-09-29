using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapArea : MonoBehaviour
{
    [SerializeField]
    [Header("マップ番号を指定")]
    public AreaNum areaNum;
    public enum AreaNum
    {
        Area1, Area2, Area3, Area4, Area5, Area6, Area7, Area8, Area9
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
