using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalTree : MonoBehaviour
{
    public int treeHealth = 10;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Axe"))
        {
            Debug.Log("PlayerTouched");
            //treeHealth -= 1;
            //if (treeHealth <= 0)
            //{
            //   
            //    // ���ŵ��¶�������Ч
            //    Destroy(gameObject);
            //}
        }
    }
}
