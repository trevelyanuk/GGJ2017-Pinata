using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour
{
    public static GameController controller;

    public bool building = true;
    public static int WIRE_WALL = 1;
    public static int WOOD_WALL = 2;
    public static int CONCRETE_WALL = 3;
    public static int METAL_WALL = 4;
    public int buildingType = 1;


    void Awake() // Enforcing a single instance of this class.
    {
        if (controller == null)
        {
            DontDestroyOnLoad(gameObject);
            controller = this;
        }
        else if (controller != this) { Destroy(gameObject); }
    }
    public static GameController getInstance()
    {
        return controller;
    }
}
