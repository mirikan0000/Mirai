using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class peneHit : MonoBehaviour
{
   [SerializeField]private PlayerHealth ph;
    [SerializeField] private int damage;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("penetrait"))
        {
            ph.TakeDamage(damage,false);
        }
    }
}
