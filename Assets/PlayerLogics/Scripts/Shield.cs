using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour
{
    public int durability;
    
    private void OnTriggerEnter(Collider other)
    {
        
        if (other.CompareTag("Sword"))
        {
            
            durability --;
            Debug.Log("Shield durability:" + durability);
        }
    }

}
