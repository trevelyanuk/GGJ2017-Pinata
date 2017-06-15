using UnityEngine;
using UnityEngine.UI;

public class HelicopterController : MonoBehaviour
{
    //   public AudioSource HelicopterSound;
    public GameController GodBlessAmerica;
    public Rigidbody HelicopterModel;
    public GameObject target;
    public Camera maincamera;

    public float Height = 3;
    public float TurnForce = 150f;
    public float ForwardForce = 10f;


    public float HeliAccelerate = 0.6f;
    public float HeliSlowdown = 2f;
    public float HeliAmbientChange = 0.2f;



    public const int GS_START = 0;
    public const int GS_PAUSE = 1;
    public const int GS_PLAY = 2;
    public const int GS_WIN = 3;
    public const int GS_LOSE = 4;

    public float turnForcePercent = 1.3f;

    private float _engineForce;

    public float EngineForce
    {
        get { return _engineForce; }
        set
        {
            _engineForce = value;
        }
    }

    private Vector2 hMove = Vector2.zero;
    private Vector2 hTilt = Vector2.zero;
    private float hTurn = 0f;
    Vector3 heliPosition;
    // Use this for initialization

    void Start()
    {
        GodBlessAmerica = GameObject.FindObjectOfType<GameController>();
    }

    void Update()
    {
        if (GodBlessAmerica.GameState == GS_PLAY)
        {
            if (Input.GetMouseButtonDown(0))
            {
                RaycastHit hit;
                Ray ray = maincamera.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, out hit))
                {
                    if (hit.transform.position.y < 1)
                    {
                        target.transform.position = hit.point;
                    }
                }
            }
        }
    }


void FixedUpdate()
{
    heliPosition = HelicopterModel.transform.position;

    if (GodBlessAmerica.GameState == GS_PLAY)
    {

        Vector3 newDirection = target.transform.position - HelicopterModel.transform.position;

        Quaternion rotationToTarget = Quaternion.LookRotation(newDirection);
        HelicopterModel.transform.localRotation = Quaternion.RotateTowards(HelicopterModel.transform.localRotation, rotationToTarget, TurnForce);


        newDirection.y = 0;
        // UIGameController.runtime.EngineForceView.text = string.Format("Distance to target = {0}", (int)newDirection.magnitude);

        if (newDirection.magnitude > 0.2f)
        {
            hMove.y += HeliAccelerate * Time.fixedDeltaTime;
        }
        else
        {
            hMove.y -= HeliSlowdown * Time.fixedDeltaTime;
        }
        hMove.y = Mathf.Clamp(hMove.y, -1, 0.5f);


        if (hMove.y > 0)
        {
            hMove.y -= HeliAmbientChange * Time.fixedDeltaTime;
        }
        if (hMove.y < 0)
        {
            hMove.y += HeliAmbientChange * Time.fixedDeltaTime;
        }
        MoveProcess();
        TiltProcess();
    }
}

    private void MoveProcess()
    {
        var turn = TurnForce * Mathf.Lerp(hMove.x, hMove.x * Mathf.Abs(hMove.y), Mathf.Max(0f, hMove.y));
        hTurn = Mathf.Lerp(hTurn, turn, Time.fixedDeltaTime * TurnForce);
        HelicopterModel.AddRelativeTorque(0f, hTurn * HelicopterModel.mass, 0f);
        HelicopterModel.AddRelativeForce(Vector3.forward * Mathf.Max(0f, hMove.y * ForwardForce * HelicopterModel.mass));

    }

    private void TiltProcess()
    {
        hTilt.x = Mathf.Lerp(hTilt.x, hMove.x, Time.deltaTime);
        HelicopterModel.transform.localRotation = Quaternion.Euler(hTilt.y, HelicopterModel.transform.localEulerAngles.y, -hTilt.x);

        heliPosition.y = Height;
        HelicopterModel.transform.position = heliPosition;
    }

    void OnTriggerEnter(Collider collider)

    {
        Debug.Log(collider);


        if (collider.gameObject.tag == "Money")
        {
                
                Destroy(collider.gameObject,0.7f);
        }

        else


            if (collider.gameObject.tag == "Mexican")
        {
            Debug.Log("Getting mexico to pay for it");
            collider.gameObject.SendMessage("StartPaying");
        }

    }
    void OnTriggerExit(Collider collider)
    {

        if (collider.gameObject.tag == "Mexican")
        {
            Debug.Log("Stop mexico to pay for it");
            collider.gameObject.SendMessage("StopPaying");
        }

    }
}