using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropItem : MonoBehaviour
{
    public enum ItemName
    {
        HealItem,SpeedUpItem,PierceBulletItem,ShieldItem
    }
    [SerializeField]
    public ItemName itemName;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "Player1" || collision.gameObject.name == "Player2")
        {
            Destroy(this.gameObject);
        }
    }
}
