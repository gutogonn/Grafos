using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GriffonController : MonoBehaviour
{
    public Griffon griffon;
    public List<Transform> routes = new List<Transform>();
    private GameObject[] aeroports;
    public Transform start, end, target, current;
    Routes myLastRoute;

    // Start is called before the first frame update
    void Start()
    {
        aeroports = GameObject.FindGameObjectsWithTag("Aeroport");
        griffon = new Griffon();
        griffon.speed = 2;
        routes = new List<Transform>();
        current = start;
        routes.AddRange(GetComponent<Djikstra>().shortestPath(current, end));
    }

    // Update is called once per frame
    void Update()
    {
        foreach(Transform r in routes)
        {
            if (target != null)
            {
                Debug.DrawLine(current.position, target.position, Color.green);
            }
        }

        StartCoroutine(fly());

        if (routes.Count <= 0)
        {
            int ran = Random.Range(0, aeroports.Length - 1);
            start = current;
            end = aeroports[ran].transform;
            routes.AddRange(GetComponent<Djikstra>().shortestPath(start, end));
        }
    }

    IEnumerator fly()
    {
        if (routes.Count > 0)
        {
            target = target == null ? routes[0] : target;
            transform.LookAt(target);

            if (checkTransitedEdge(current, target))
            {
                transform.position = Vector3.MoveTowards(transform.position, target.position, griffon.speed * Time.deltaTime);
            }
            yield return new WaitForSeconds(15);
            if (Vector3.Distance(transform.position, target.position) <= 1)
            {
                routes.Remove(target);
                current = target;

                if (routes.Count > 0)
                    target = routes[0];

                GameController.Instance().transit.Remove(myLastRoute);
                
                if (checkTransitedEdge(current, target))
                {
                    myLastRoute = new Routes();
                    myLastRoute.griffon = gameObject.transform;
                    myLastRoute.current = current;
                    myLastRoute.target = target;
                    GameController.Instance().transit.Add(myLastRoute);
                }
                // Debug.Log(gameObject.name + " [" + current.name + " to " + target.name + "]");
            }
        }

    }

    bool checkTransitedEdge(Transform current, Transform target)
    {
        foreach (Routes r in GameController.Instance().transit)
        {
            if (current.Equals(r.target) && target.Equals(r.current) && !r.griffon.Equals(griffon))
            {
                Debug.Log(r.griffon.name + " [" + r.target.position + " - " + r.current.position + "] - " + current.name + " [" + current.position + "]");
                return false;
            }
        }
        return true;
    }
}
