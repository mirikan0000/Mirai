using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fragreceiver : MonoBehaviour
{
    public bool misileflog;
    public bool armorflog;
    public bool Hpflog;
    public bool penetratBulletflog;
   [SerializeField] private PlayerHealth P_Health;//HPフラグからHPを減らすための箱
    private void Start()
    {
        penetratBulletflog = false;
        misileflog = false;
    }
    private void Update()
    {
        
    }
}
