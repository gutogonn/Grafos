using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Djikstra : MonoBehaviour
{
    private GameObject[] nodes;

    public List<Transform> shortestPath(Transform start, Transform end)
    {
        nodes = GameObject.FindGameObjectsWithTag("Aeroport");
        List<Transform> result = new List<Transform>();
        Transform node = calculate(start, end);

        while (node != null)
        {
            result.Add(node);
            Node currentNode = node.GetComponent<Node>();
            node = currentNode.parent;
        }
        result.Reverse();
        return result;
    }

    Transform calculate(Transform start, Transform end)
    {
        List<Transform> unexplored = new List<Transform>();
               
        foreach (GameObject v in nodes)
        {
            Node n = v.GetComponent<Node>();
            n.resetNode();
            unexplored.Add(v.transform);
        }

        start.GetComponent<Node>().distance = 0;

        while (unexplored.Count > 0)
        {
            unexplored.Sort((x, y) => x.GetComponent<Node>().distance.CompareTo(y.GetComponent<Node>().distance));
            Transform current = unexplored[0];

            if (current == end)
            {
                return end;
            }

            unexplored.Remove(current);

            Node currentNode = current.GetComponent<Node>();
            List<Transform> neighbours = currentNode.neighbours;
            foreach (Transform n in neighbours)
            {
                Node node = n.GetComponent<Node>();

                if (unexplored.Contains(n))
                {
                    float dist = Vector3.Distance(n.position, current.position);
                    dist = currentNode.distance + dist;

                    if (dist < node.distance)
                    {
                        node.distance = dist;
                        node.parent = current;
                    }
                }

            }

        }

        return end;
    }

}
