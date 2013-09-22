using UnityEngine;
using System.Collections;
[System.Serializable]

public class ActorControl : MonoBehaviour {

	public
		Vector3 position, velocity;
	public
		float viewDistance = 0;
	public
		int aware, health, speed, behavior, damage, countDown;	
	public
		GameObject[] navPoints;
	
	protected 
		int timeKeeper;
	protected
		GameObject Actor, Player;
		
	
	 void Start(){	
	 	this.position = transform.position;
		Player = GameObject.FindGameObjectWithTag( "Player" );
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
		gameObject.SendMessage ( "AIPathCall", navPoints , SendMessageOptions.DontRequireReceiver );
		}
	
	void attack(){
		// get proximity to player
		// if( proximity <= range )
		// damagePlayer();
	}

	void  coolOff(){
		if( aware == 0 && behavior == 1 ){
			if(  timeKeeper * Time.deltaTime > countDown )
			{
				behavior = 0;
			}
		}
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
		
		detect();
		move();
		attack();
		coolOff();
	}
}