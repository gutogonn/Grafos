using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AeroportCntrl : MonoBehaviour
{
    public Node node;

    // Start is called before the first frame update
    void Start()
    {
        node = GetComponent<Node>();
        node.name = name;
        showDistance();
    }

    // Update is called once per frame
    void Update()
    {
 
    }


    public void showDistance()
    {
        foreach(Transform n in node.neighbours)
        {
            Debug.Log(node.name + " - " + n.name + " [" + Vector3.Distance(transform.position, n.position) + "]");
        }
    }
}
