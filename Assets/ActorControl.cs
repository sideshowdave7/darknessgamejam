﻿using UnityEngine;
using System.Collections;
[System.Serializable]

public class ActorControl : MonoBehaviour {

	public
		Vector3 position, velocity;
	public
		float viewDistance = 0, countDown;
	public
		int aware, health, speed, behavior, damage; 
	
	public bool justPatrol = false, isActive = false, prevIsActive = false;
		
	public
		GameObject[] navPoints;
	
	private GameObject[] globalNavPoints;
	
	protected
		GameObject[] tempNavPoints;
	protected 
		float timeKeeper;
	protected
		GameObject Actor, Player;
		
	
	 void Start(){	
	 	this.position = transform.position;
		Player = GameObject.FindGameObjectWithTag( "Player" );
		
		globalNavPoints = new GameObject[navPoints.Length];
		
	}

	void detect(){
		if( Player == null )
			return;
		RaycastHit hitInfo;
		bool isHit;
		Ray test = new Ray( collider.bounds.center,  Player.collider.bounds.center - collider.bounds.center );
		
		isHit = Physics.Raycast ( test, out hitInfo, viewDistance );
		
		if( isHit && hitInfo.collider.tag == "Player" )
		{
			setAware( 1 );
			timeKeeper = 0;
		}
		else setAware( 0 );
		
		if( aware == 1 )
			behavior = 1;
	}

	void move(){
		if (behavior == 0 && !justPatrol){
			gameObject.SendMessage( "AIPathCall", navPoints , SendMessageOptions.DontRequireReceiver );
		} else if (justPatrol) {
			gameObject.SendMessage( "AIPathCall", globalNavPoints , SendMessageOptions.DontRequireReceiver );
		}
		else if (behavior == 1){
		
			tempNavPoints = new GameObject[1];
			tempNavPoints[0] = Player;
			if (!justPatrol){
				gameObject.SendMessage( "AIPathCall", tempNavPoints , SendMessageOptions.DontRequireReceiver );
			}
		}
	}
	
	void attack(){
		// get proximity to player
		// if( proximity <= range )
		// damagePlayer();
	}

	void  coolOff(){
		if( aware == 0 && behavior == 1 ){
			timeKeeper += Time.deltaTime;
			if(timeKeeper > countDown )
			{
				behavior = 0;
				timeKeeper = 0;
			}
		}
		else
			timeKeeper = 0;
	}

	int  getAware(){
		return this.aware;
	}

	void  setAware( int aware ){
		this.aware = aware;
	}

	int  getHealth(){
		return this.health;
	}

	void  setHealth( int health ){
		this.health = health;
	}

	int  getDamage(){
		return this.damage;
	}

	void  setDamage( int damage ){
		this.damage = damage;
	}

	int  getSpeed(){
		return this.speed;
	}

	void  setSpeed( int speed ){
		this.speed = speed;
	}

	int  getBehavior(){
		return this.behavior;
	}

	void  setBehavior( int behavior ){
		this.behavior = behavior;
	}


	void Update () {
		if( Player == null )
			Player = GameObject.FindGameObjectWithTag( "Player" );
		
		
		if (!prevIsActive && isActive) {
			for (int i = 0; i < navPoints.Length; i++){
					globalNavPoints[i] = new GameObject("navPoint"+i.ToString());
					globalNavPoints[i].transform.position += transform.position + navPoints[i].transform.localPosition;
			}
		}
		
		if (isActive){
		
			detect();
			move();
			attack();
			coolOff();
			
		}
		
		prevIsActive = isActive;

	}
}