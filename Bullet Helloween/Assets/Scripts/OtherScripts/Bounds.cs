using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Bounds : MonoBehaviour
{
	//VARIABLES                                 //VARIABLES
	[Header("General Variables")]               //GENERAL VARIABLES
	public GameObject bossObject;               //Boss
	//UDPATE FUNCTION
	void Update()
	{
		if (bossObject.GetComponent<BossHealth>().bounds == false)
			GetComponent<EdgeCollider2D>().enabled = false;
	}
}
///END OF SCRIPT!