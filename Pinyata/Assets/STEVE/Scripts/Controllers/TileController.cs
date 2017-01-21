using UnityEngine;
using System.Collections;

public class TileController : MonoBehaviour
{
	public GameObject Cube;
    public Vector3 tilePos;
    public bool occupied = false;
    public int wallType = 0;
    public bool isWallCapable = false;
    public int health = 0;
    private bool building = true;

    public void intitialised()
    {
        //Debug.Log("Init");
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
					occupied = true;
					// Build the wall
					GameObject tempTile = (Instantiate(Cube, tilePos, Quaternion.identity) as GameObject);
					tempTile.transform.Translate(0,(Cube.transform.localScale.y/2),0);
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
