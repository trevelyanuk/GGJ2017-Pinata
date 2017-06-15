using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{

    public FloorSpawner FloorNavMesh;
    public HelicopterController Heli;


    public AudioSource Trumpington;
    public AudioClip TakingBigly;
    public AudioClip Ayayaya;
    public AudioClip PresidentHasntClue;
    public AudioClip[] Lose;
    public AudioClip Rich;
    public AudioClip[] Win;
    public AudioClip[] Idle;
    public AudioClip[] Taunt;

    public GameObject LoseScreen;
    public GameObject WinScreen;
    public GameObject MainScreen;
    public static GameController controller;
    public NPC_Spawner npcSpawner;
    public bool building = true;
    public static int WIRE_WALL = 1;
    public static int WOOD_WALL = 2;
    public static int CONCRETE_WALL = 3;
    public static int METAL_WALL = 4;
    public int buildingType = 1;
    public Image cultureBar;
    public Text cultureText;
    public Image wallButton1, wallButton2;
    private int wallBlockCount = 0;
    private int moneyForWall = 0;

    float deltaTimer = 0, deltaTrigger = 20;

    private int width = 8, height = 8;
    public int Funds = 0;
    public int Wallcost = 200;

    public int ImmigrantCount = 0;
    public int MaxImmigrants = 20;

    public bool WarningFifty = false;
    public bool WarningSeventyFive = false;
    public bool WarningNinetyFive = false;

    Text dollah;
    Text peeps;

    public const int GS_START = 0;
    public const int GS_PAUSE = 1;
    public const int GS_PLAY = 2;
    public const int GS_WIN = 3;
    public const int GS_LOSE = 4;
    float YapTimer = 6;

    public int GameState = GS_START;

    public void addMoney()
    {
        moneyForWall++;
    }
    public void SetGameState(int gameState)
    {
        GameState = gameState;
    }
    public void IllegalEntry()
    {
        peeps.text = "Total illegal Mexicants: " + ImmigrantCount.ToString();
        ImmigrantCount++;

        float scale = (float)ImmigrantCount / (float)MaxImmigrants;
        float percentage = (float)ImmigrantCount / (float)MaxImmigrants;
        if (scale > 1) {  scale = 1; }
        cultureText.text = "Culture level: " + (int)(scale * 100) + " %";
        cultureBar.transform.localScale = new Vector2(scale, 1);
        Color barColour = cultureBar.color;
        barColour.r = scale;
        barColour.g = 1 - scale;
        cultureBar.color = barColour;

        if (!WarningFifty)
        {
            if (percentage > 0.5f)
            {
                WarningFifty = true;
                Trumpington.clip = Ayayaya;
                Trumpington.Play();
                 
            }
        }

        if (!WarningSeventyFive)
        {
            if (percentage > 0.75f)
            {
                WarningSeventyFive = true;
                Trumpington.clip = TakingBigly;
                Trumpington.Play();
            }
        }

        if (!WarningNinetyFive)
        {
            if (percentage > 0.95f)
            {
                WarningNinetyFive = true;
                Trumpington.clip = PresidentHasntClue;
                Trumpington.Play();
            }
        }
        if (MaxImmigrants < ImmigrantCount)
        {
            Trumpington.clip = Lose[Random.Range(0, Lose.Length)];
            Trumpington.Play();


            GameState = GS_LOSE;
            LoseScreen.SetActive(true);
            resetAll();
        }

    }
    void resetAll()
    {
        GameObject[] NPCReset = GameObject.FindGameObjectsWithTag("Mexican");
        Heli.transform.position = new Vector3(0, 3, 6);

        foreach (GameObject x in NPCReset)
        {
            DestroyObject(x);
        }
        
        cultureBar.transform.localScale = new Vector2(0.0f, 1);
        cultureText.text = "Culture level!";

        dollah.text = "Funds: $" + Funds.ToString();//moneyForWall
        //dollah.text = "Funds: $" + moneyForWall;//moneyForWall
        peeps.text = "Total illegal Mexicants: " + ImmigrantCount.ToString();

        building = true;
        buildingType = 1;

        deltaTimer = 0;
        deltaTrigger = 20;

        width = 8;
        height = 8;
        Funds = 1000;
        Wallcost = 200;

        ImmigrantCount = 0;
        MaxImmigrants = 20;

        WarningFifty = false;
        WarningSeventyFive = false;
        WarningNinetyFive = false;


        MainScreen.SetActive(false);
    }
    public bool BuyWallBlock()
    {
        if (wallBlockCount >= 8)
        {

            Trumpington.clip = Win[Random.Range(0, Lose.Length)];
            Trumpington.Play();

            GameState = GS_WIN;
            WinScreen.SetActive(true);
            resetAll();

        }
        if (Funds > Wallcost)
        {
            Funds -= Wallcost;
            dollah.text = "Funds: $" + Funds.ToString();//moneyForWall
            wallBlockCount++;
            
            Debug.Log("Bought wall block " + wallBlockCount);
            return true;
        }
        return false;
    }

    public void ChangeFunds(int amount)
    {
        if ((YapTimer <= 2) && (!Trumpington.isPlaying))
        {
            Trumpington.clip = Taunt[(int)Random.Range(0, Taunt.Length)];
            Trumpington.Play();
            YapTimer = Random.Range(Trumpington.clip.length, Trumpington.clip.length + 10);
        }

        Funds += amount;
        if (Funds < 0)
        {
            Funds = 0;
        }
        dollah.text = "Funds: $" + Funds.ToString();
    }




    void Awake() // Enforcing a single instance of this class.
    {
        if (controller == null)
        {
            DontDestroyOnLoad(gameObject);
            controller = this;
        }
        else if (controller != this) { Destroy(gameObject); }
    }

    void Start()
    {
        dollah = GameObject.Find("Funds").GetComponent<Text>();
        peeps = GameObject.Find("Immigrants").GetComponent<Text>();
        cultureBar.transform.localScale = new Vector2(0.0f, 1);
        cultureText.text = "Culture level!";

        dollah.text = "Funds: $" + Funds.ToString();//moneyForWall
        //dollah.text = "Funds: $" + moneyForWall;//moneyForWall
        peeps.text = "Total illegal Mexicants: " + ImmigrantCount.ToString();

        npcSpawner.initialiseNpcWave(20);
    }
    public Vector3 getFloorSize()
    {
        return new Vector2(width, height);
    }
    public static GameController getInstance()
    {
        return controller;
    }
     void Update()
    {
        deltaTimer += Time.deltaTime;
        if (deltaTimer >= deltaTrigger)
        {
            deltaTimer = 0;
            npcSpawner.initialiseNpcWave(15);
        }
        YapTimer -= 1 * Time.deltaTime;
        if (YapTimer <= 0)
        {
            Trumpington.clip = Idle[Random.Range(0, Idle.Length)];
            Trumpington.Play();
            YapTimer = Random.Range(Trumpington.clip.length, Trumpington.clip.length + 10);

        }
    }
}
