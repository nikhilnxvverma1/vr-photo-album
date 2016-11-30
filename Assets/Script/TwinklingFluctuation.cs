using UnityEngine;
using System.Collections;

public class TwinklingFluctuation : MonoBehaviour {

	private SpriteRenderer spriteRenderer;
	private float alpha=0f;
	private static float stepValue=0.02f;
	private float step=stepValue;
	private bool fluctuationActive=false;
	// Use this for initialization
	void Start () {
		spriteRenderer=gameObject.GetComponent(typeof(SpriteRenderer)) as SpriteRenderer;
		spriteRenderer.color=new Color (1f, 1f, 1f, 0f);
		fluctuationActive=false;
	}
	
	// Update is called once per frame
	void Update () {
		if(fluctuationActive){
			alpha+=step;
			if(alpha>1){
				alpha=1f;
				step=-stepValue/2;
			}else if(alpha<0){
				alpha=0f;
				step=stepValue;
				fluctuationActive=false;
			}
			spriteRenderer.color=new Color (1f, 1f, 1f, alpha);
		}else{
			//fluctuation should happen sparodically
			//each update, pick a number between some range, and check if it is exactly some given value
			//since update happens 60 fps, it is very likely that this will happen once in a while
			fluctuationActive=50==Random.Range(1,300);
		}
	}
}
