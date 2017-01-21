using UnityEngine;
using System.Collections;

public class NpcController : MonoBehaviour {

    public GameObject Npc_Mesh;
    public Animation Npc_Animation;
    private NavMeshAgent navAgent;
    public FloorSpawner floorSpawner;
    public float navBoundsXmax, navBoundsXmin, navBoundsZmax, navBoundsZmin;
    private float deltaCounter = 0, deltaTrigger = 3;
    private int behaviourState = 1;
    private float wallTripwire, finishTripwire;

    public const int NPC_SEEK_OPPERTUNITY = 1;
    public const int NPC_FLEE_TRUMP = 2;
    public const int NPC_ATTACK_WALL = 3;
    public const int NPC_RETURN_HOME = 4;

    private Vector3 spawnPoint, targetPoint;
    // Use this for initialization
    void Start ()
    {
        navAgent = gameObject.GetComponent<NavMeshAgent>();
        navBoundsXmax = ((floorSpawner.getFloorSize().x *0.5f) + 0.5f);
        navBoundsXmin = -((floorSpawner.getFloorSize().x * 0.5f) + 0.5f);
        navBoundsZmax = ((floorSpawner.getFloorSize().y * 0.5f) + 0.5f);
        navBoundsZmin = -((floorSpawner.getFloorSize().y * 0.5f) + 0.5f);
        deltaTrigger = Random.Range(1.0f, 5.0f);
        deltaTrigger = 1;
        generateSpawn();
        transform.position = spawnPoint;
    }
    private void generateSpawn()
    {
        //spawnPoint
        //targetPoint
        float zSpawn = -(navBoundsZmax);
        float xSpawn = Random.Range(navBoundsXmin, navBoundsXmax);
        spawnPoint = new Vector3(xSpawn, 0, zSpawn);
        float zTarget = (navBoundsZmax);
        float xTarget = Random.Range(navBoundsXmin, navBoundsXmax);
        targetPoint = new Vector3(xTarget, 0, zTarget);

        wallTripwire = 0;
        finishTripwire = (floorSpawner.getFloorSize().y * 0.5f)-1;
    }
    private void npcBehaviour(int state)
    {
        switch(state)
        {
            case NPC_SEEK_OPPERTUNITY:
                navAgent.SetDestination(targetPoint);
                break;
            case NPC_FLEE_TRUMP:

                break;
            case NPC_ATTACK_WALL:

                break;
            case NPC_RETURN_HOME:
                generateSpawn();
                navAgent.SetDestination(spawnPoint);
                break;
        }
    }

    private void randomMovement()
    {
        float newX = Random.Range(navBoundsXmin, navBoundsXmax);
        float newY = Random.Range(navBoundsZmin, navBoundsZmax);
        navAgent.SetDestination(new Vector3(newX, 0, newY));
    }

	
	// Update is called once per frame
	void Update ()
    {
        deltaCounter += Time.deltaTime;
        if(deltaCounter >= deltaTrigger)
        {
            deltaCounter = 0;
            npcBehaviour(behaviourState);
            //randomMovement();
        }

        // Detect Freedom 
        if (transform.position.z > wallTripwire)
        {
            // past the wall
        }
        // Detect Success
        if (transform.position.z > finishTripwire)
        {
            // Made it to the end!
            behaviourState = NPC_RETURN_HOME;
        }


    }
}
