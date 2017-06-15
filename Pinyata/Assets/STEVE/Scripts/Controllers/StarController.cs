using UnityEngine;
using System.Collections;

public class StarController : MonoBehaviour
{
    SpriteRenderer sRender;
    Animator anim;

	// Use this for initialization
	void Start () {
        sRender = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
    }
	
	// Update is called once per frame
	void Update ()
    {
        transform.LookAt(Camera.main.transform.position, Vector3.up);
        StartCoroutine(Die());
    }
    private IEnumerator Die()
    {
        //PlayAnimation(GlobalSettings.animDeath1, WrapeMode.ClampForever);
        yield return new WaitForSeconds(1);
        Destroy(gameObject);
    }
}
