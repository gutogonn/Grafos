using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour
{
    public float distance;
    public Transform parent;
    public List<Transform> neighbours;
    public bool showConnections;

    void Start()
    {
        this.resetNode();
    }

    private void Update()
    {
        if (showConnections)
        {
            foreach (Transform n in neighbours)
            {
                Debug.DrawLine(transform.position, n.position, Color.white);
            }
        }
    }

    public void resetNode()
    {
        distance = float.MaxValue;
        parent = null;
    }

}
