using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TileController : MonoBehaviour
{
    public GameObject wall;
    public Vector3 tilePos;
    public bool occupied = false;
    public int wallType = 0;
    public bool isWallCapable = false;
    //public int health = 0;
    private bool building = true;
    public Button Wall1;

    public GameController GodBlessAmerica;

    public const int GS_START = 0;
    public const int GS_PAUSE = 1;
    public const int GS_PLAY = 2;
    public const int GS_WIN = 3;
    public const int GS_LOSE = 4;

    public void intitialised()
    {
        //Debug.Log("Init");
    }

    public void Reset()
    {

    }
    void Start()
    {
        GodBlessAmerica = GameObject.FindObjectOfType<GameController>();
    }

    public void OnMouseUp()
    {
        if (GodBlessAmerica.GameState == GS_PLAY)
        {

        if (building)
        {
            if (isWallCapable)
            {
                if (!occupied)
                {
                        if (GodBlessAmerica.BuyWallBlock())
                        {

                            Debug.Log("Build new wall");
                            occupied = true;
                            // Build the wall
                            GameObject tempWall = (Instantiate(wall, tilePos, Quaternion.identity) as GameObject);
                            tempWall.transform.Translate(0, (wall.transform.localScale.y / 2), wall.transform.localScale.z / -5);
                            tempWall.transform.Rotate(new Vector3(0, 1, 0), 270);
                            //Debug.Log("Try set parent?");
                            tempWall.GetComponent<Wall>().setParent(this);
                        }

                }
            }
            else
            {
                // just floor
             //   Debug.Log("Plain old floor");
            }

            }
        }


    }


}
