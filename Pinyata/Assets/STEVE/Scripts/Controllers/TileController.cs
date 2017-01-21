using UnityEngine;
using System.Collections;

public class TileController : MonoBehaviour
{

    public Vector3 tilePos;
    public bool occupied = false;
    public int wallType = 0;
    public bool isWallCapable = false;
    public int health = 0;
    private bool building = true;

    public void intitialised()
    {
        Debug.Log("Init");
    }

    public void tileClicked()
    {
        Debug.Log("TESTEDHDHHDDHBD");
    }
    void OnMouseUp()
    {
        if(building)
        {
            if(isWallCapable)
            {
                if(!occupied)
                {
                    Debug.Log("Build new wall");
                    // Build the wall

                    occupied = true;
                }
            }
            else
            {
                // just floor
                Debug.Log("Plain old floor");
            }
           
        }
        

    }


}
