using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fragreceiver : MonoBehaviour
{
    public bool misileflog;
    public bool armorflog;
    public bool Hpflog;
    public bool penetratBulletflog;
   [SerializeField] private PlayerHealth P_Health;//HP�t���O����HP�����炷���߂̔�
    private void Start()
    {
        penetratBulletflog = false;
        misileflog = false;
    }
    private void Update()
    {
        
    }
}
