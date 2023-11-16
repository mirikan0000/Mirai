using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fragreceiver : MonoBehaviour
{
    public bool misileflog;
    public bool armorflog;
    public bool Hpflog;
    public bool penetratBulletflog;
   [SerializeField] private PlayerHealth P_Health;//HPƒtƒ‰ƒO‚©‚çHP‚ðŒ¸‚ç‚·‚½‚ß‚Ì” 
    private void Start()
    {
        penetratBulletflog = false;
        misileflog = false;
    }
    private void Update()
    {
        
    }
}
