using UnityEngine;
using System.Collections;

public class Wall : MonoBehaviour 
{
	public int maxHealth = 100;
	public int curHealth = 100;

	public float healthBarLength;
	
	private TileController parentTile;
		
	// Use this for initialization
	void Start ()
	{
	
	}
	
	// Update is called once per frame
	void Update ()
	{
		AddjustCurrentHealth(0);
	}

	public void setParent(TileController tCon)
	{
		//Debug.Log("Set parent " + tCon);
		parentTile = tCon;
	}

	public void OnMouseUp()
	{

			if (parentTile.occupied)
		{
			Debug.Log("Upgrade");
		}
	}
	public void AddjustCurrentHealth(int adj)
	{
		curHealth +=adj;
		if (curHealth <= 0 )
		{
			Destroy(gameObject);
		}
	}
	public void OnGui()
	{
		GUI.Box(new Rect(10,40,healthBarLength, 20), curHealth + "/" + maxHealth);
	}
		

	void OnCollisionEnter (Collision other)
	{
		Debug.Log ("Hello");
		if (other.gameObject.CompareTag("Mexican")) 
		{
			if (parentTile != null) {
				parentTile.occupied = false;
			}
			Debug.Log("BANG BANG!");
			curHealth -= 1;
			Debug.Log ("health of wall is" + curHealth);
			//return (health);
		}
	}
}