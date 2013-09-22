using UnityEngine;
using System.Collections;
[System.Serializable]

public class ActorControl : MonoBehaviour {

	public
		Vector3 position, velocity;
	public
		float viewDistance = 0;
	public
		int aware, health, speed, behavior, damage;	
		GameObject Actor, Player;
		
	
	 void Start(){
		//Get player object
		
	 	this.position = transform.position;
		Player = GameObject.FindGameObjectWithTag( "Player" );
		
	}

	void detect(){

		RaycastHit hitInfo;
		bool isHit;
		Ray test = new Ray( collider.bounds.center,  Player.collider.bounds.center - collider.bounds.center );
		
		isHit = Physics.Raycast ( test, out hitInfo, viewDistance );
		
		if( isHit && hitInfo.collider.tag == "Player" ) setAware( 1 );
		else setAware( 0 );
	}

	void move(){
		switch( aware )
		{
			case 0: //some passive stuff 
				break;
			case 1: gameObject.SendMessage ( "AIPathCall", SendMessageOptions.DontRequireReceiver );
				break;
			
		}
		// gameObject.SendMessage ( "AIPathCall", SendMessageOptions.DontRequireReceiver );
	}

	void attack(){
		// get proximity to player
		// if( proximity <= range )
		// damagePlayer();
	}

	void  coolOff(){
		// detect();
		// if( !getState(); )
		// start countdown...
		// if( countdown >= maxTime )
		// setBehavior( 0 );
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
		//return this;
	}

	void  setDamage( int attack ){
		//this.attack = attack;
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
		
		detect();
		move();
		attack();
		coolOff();
	}
}