using UnityEngine;
using System.Collections;

public class NpcController : MonoBehaviour
{

    public GameObject Npc_Mesh;
    public Animation Npc_Animation;
    public Animator Npc_animator;
    private NavMeshAgent navAgent;
    private float navBoundsXmax, navBoundsXmin, navBoundsZmax, navBoundsZmin;
    private float deltaCounter = 0, deltaTrigger = 1, eligibleToPay = 0.5f;
    private int behaviourState = 1;
    private float wallTripwire, finishTripwire;
    private float lifeTimer = 20;
    private GameObject trump;
    public GameObject moneyPrefab;
    private bool isColiding = false;
    public GameObject stars;
    private bool alive = true;

    private int money;
    private float speed;

    public const int NPC_SEEK_OPPERTUNITY = 1;
    public const int NPC_FLEE_TRUMP = 2;
    public const int NPC_ATTACK_WALL = 3;
    public const int NPC_RETURN_HOME = 4;

    public int WorstCase = -20;
    public int BestCase = 10;
    public int EconomicImpact;
    public GameController America;

    private Vector3 spawnPoint, targetPoint;
    // Use this for initialization
    void Start()
    {
        America = GameObject.FindObjectOfType<GameController>();
        EconomicImpact = (int)Random.Range(WorstCase, BestCase);

        trump = GameObject.FindWithTag("Trump");
        navAgent = gameObject.GetComponent<NavMeshAgent>();
        navBoundsXmax = ((GameController.getInstance().getFloorSize().x * 0.5f) + 0.5f);
        navBoundsXmin = -((GameController.getInstance().getFloorSize().x * 0.5f) + 0.5f);
        navBoundsZmax = ((GameController.getInstance().getFloorSize().y * 0.5f) + 0.5f);
        navBoundsZmin = -((GameController.getInstance().getFloorSize().y * 0.5f) + 0.5f);
        deltaTrigger = Random.Range(1.0f, 5.0f);
        deltaTrigger = 3;
        generateSpawn();
        transform.position = spawnPoint;
        navAgent.speed = speed;
    }
    public void payForWall()
    {
        //Debug.Log("Paying for wall :(");
        // Loose money
        if (eligibleToPay > 0.5 && money > 0)
        {
            America.SendMessage("ChangeFunds", 1);
            eligibleToPay = 0;
            money--;
            GameController.getInstance().addMoney();
            GameObject tempTile = (Instantiate(moneyPrefab, transform.position, Quaternion.identity) as GameObject);
            //tempTile.transform.parent = this.transform;
        }

    }
    private void generateSpawn()
    {
        float zSpawn = -(navBoundsZmax + 5);
        float xSpawn = Random.Range(navBoundsXmin, navBoundsXmax);
        spawnPoint = new Vector3(xSpawn, 0, zSpawn);
        float zTarget = (navBoundsZmax);
        float xTarget = Random.Range(navBoundsXmin, navBoundsXmax);
        targetPoint = new Vector3(xTarget, 0, zTarget);
        money = Random.Range(1, 3);
        speed = Random.Range(0.6f, 1.5f);
        lifeTimer = Random.Range(20.0f, 30.0f);
        wallTripwire = 0;
        finishTripwire = (GameController.getInstance().getFloorSize().y * 0.5f);
    }
    private void npcBehaviour(int state)
    {
        switch (state)
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
                StartCoroutine(Die());
                break;
        }
    }
    private IEnumerator Die()
    {
        //PlayAnimation(GlobalSettings.animDeath1, WrapeMode.ClampForever);
        yield return new WaitForSeconds(10);
        Destroy(gameObject);
    }

    private void randomMovement()
    {
        float newX = Random.Range(navBoundsXmin, navBoundsXmax);
        float newY = Random.Range(navBoundsZmin, navBoundsZmax);
        navAgent.SetDestination(new Vector3(newX, 0, newY));
    }

    public void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;
        if (navAgent)
            Gizmos.DrawWireCube(navAgent.destination, Vector3.one);
    }

    // Update is called once per frame

    void StartPaying()
    {
        isColiding = true;
        Debug.Log("Test");
    }
    void StopPaying()
    {
        isColiding = false;
        Debug.Log("Test");
    }
    void Update()
    {
        lifeTimer -= Time.deltaTime;
        deltaCounter += Time.deltaTime;
        eligibleToPay += Time.deltaTime;
        //getRangeToTrump()
        if (isColiding)
        {
            payForWall();
        }
        if (lifeTimer > 0)
        {
            //Update behaviours
            if (deltaCounter >= deltaTrigger)
            {
                deltaCounter = 0;
                npcBehaviour(behaviourState);
                //if(getRangeToTrump() < 5)
                //{

                //}
                //randomMovement();
            }
            if (money <= 0)
            {
                // Detect Freedom 
                //if (transform.position.z > wallTripwire)
                //{
                // past the wall, keep going
                //}
                //else
                //{
                behaviourState = NPC_RETURN_HOME;
                //}
            }
            // Detect Success
            if (transform.position.z > finishTripwire)
            {
                if(alive)
                {
                    // Made it to the end!
                    //behaviourState = NPC_RETURN_HOME;
                    America.SendMessage("ChangeFunds", EconomicImpact);
                    America.SendMessage("IllegalEntry");
                    alive = false;
                    Vector3 newStarPos = transform.position;
                    newStarPos.y = 1;
                    GameObject tempStar = (Instantiate(stars, newStarPos, Quaternion.identity) as GameObject);
                    Destroy(gameObject);
                    // Effects and score mods
                }

            }

            float velocity = ((navAgent.velocity.x + navAgent.velocity.z) * 2);


            float newSpeed = navAgent.velocity.magnitude / speed;
            //Debug.Log("Velocity = " + newSpeed);
            //Vector3.Normalize( navAgent.velocity.normalized);
            Npc_animator.SetFloat("velocity", newSpeed);


        }
        else
        {
            // Dead
            //payForWall();
            Destroy(gameObject);
        }





    }
}
