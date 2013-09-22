using UnityEngine;
using System.Collections;

[RequireComponent(typeof(OTSprite))]
public class VisibleToggleSpritesThingy : MonoBehaviour
{
	public int visibleOffset;
	
	public void SetVisibleMode (bool visible)
	{
		var sprite = GetComponent<OTSprite> ();
		/*
		if (visible && sprite.frameIndex < visibleOffset) {
			sprite.frameIndex += visibleOffset;
		}else if(!visible && sprite.frameIndex >= visibleOffset){
			sprite.frameIndex -= visibleOffset;
		}*/
		sprite.visible = !visible;
	}
}
