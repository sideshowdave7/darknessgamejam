﻿using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{
	public float move_speed;
	
	// Use this for initialization
	void Start ()
	{
		
	}
	
	// Update is called once per frame
	void Update ()
	{
		var input = new Vector3(Input.GetAxis ("Horizontal"),Input.GetAxis ("Vertical"), 0f);
		
		this.transform.position += (input * move_speed * Time.deltaTime);
	}
}
