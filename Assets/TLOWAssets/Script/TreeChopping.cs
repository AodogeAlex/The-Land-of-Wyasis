using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeChopping : MonoBehaviour
{
    public GameObject customTreePrefab;
    public LayerMask treeLayer;
    public float interactDistance = 2f;

    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            RaycastHit hit;
            if (Physics.Raycast(transform.position, transform.forward, out hit, interactDistance, treeLayer))
            {
                ReplaceTree(hit.collider.gameObject);
            }
            
        }
    }
    void ReplaceTree(GameObject terrainTree)
    {
        //Vector3 position = terrainTree.transform.position;
        //Quaternion rotation = terrainTree.transform.rotation;

        Destroy(terrainTree);

        //Instantiate(customTreePrefab, position, rotation);
    }
}
