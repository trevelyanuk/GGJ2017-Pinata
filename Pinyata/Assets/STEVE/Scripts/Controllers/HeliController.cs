using UnityEngine;
using System.Collections;

public class HeliController : MonoBehaviour
{
    NavMeshAgent myAgent;
    //Vector2 targetPosition
	// Use this for initialization
	void Start ()
    {
        myAgent = GetComponent<NavMeshAgent>();
    }
	
	// Update is called once per frame
	void Update ()
    {

        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 200))
            {
                //myAgent.SetDestination(hit.point);
            }
        }
    }
}
