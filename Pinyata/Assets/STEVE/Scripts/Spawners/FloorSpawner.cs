using UnityEngine;
using System.Collections;

public class FloorSpawner : MonoBehaviour
{
    public GameObject floorTilePrefab;
    // Use this for initialization
    private int width = 8, height = 8;
    void Start()
    {
        genFloor();
    }
    public Vector3 getFloorSize()
    {
        return new Vector2(width, height);
    }
    public void genFloor()
    {
        for (int i = 0; i <= width; i++)
        {
            for (int j = 0; j <= height; j++)
            {
                float x = i - (width * 0.5f);
                float z = j - (height * 0.5f);
                Vector3 position = new Vector3(x, 0, z);

                GameObject tempTile = (Instantiate(floorTilePrefab, position, Quaternion.identity) as GameObject);
                tempTile.transform.parent = this.transform;
                TileController tempController = tempTile.GetComponent<TileController>();
                tempController.tilePos = position;

                if (j == height-2)
                {
                    Material yellowMat = new Material(Shader.Find("Diffuse"));
                    Color32 yellow = new Color32(255, 255, 0, 255);
                    Renderer rend = tempTile.GetComponent<Renderer>();
                    rend.material = yellowMat;
                    yellowMat.color = yellow;
                    tempController.isWallCapable = true;
                    tempController.intitialised();
                }
            }
        }
    }
}


