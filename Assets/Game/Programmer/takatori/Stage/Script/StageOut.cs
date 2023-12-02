using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageOut : MonoBehaviour
{
    [Range(0,100)][SerializeField] [Header("フィールドダメージ")] private int FieldDamage=10;
    [SerializeField] private PlayerHealth playerHealth1P;
    [SerializeField] private PlayerHealth playerHealth2P;
    float hels;
    private void Start()
    {
        playerHealth1P= GameObject.FindGameObjectWithTag("Player1").GetComponent<PlayerHealth>();
        playerHealth2P = GameObject.FindGameObjectWithTag("Player2").GetComponent<PlayerHealth>();
    }
    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player1"))
        {
            playerHealth1P.TakeDamage(FieldDamage, false);
        }
        if (collision.gameObject.CompareTag("Player2"))
        {
           playerHealth2P.TakeDamage(FieldDamage,false);
        }

    }
}
