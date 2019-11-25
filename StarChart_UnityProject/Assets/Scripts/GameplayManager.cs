using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameplayManager : Singleton<GameplayManager>
{

	public Sprite[] CardFaces;
	public GameObject lastTouchedGameObject;

  void Start()
  {
		lastTouchedGameObject = null;

	}

  // Update is called once peer frame
  void Update()
  {
		if (lastTouchedGameObject != null)
			Debug.Log($"Object Name {lastTouchedGameObject.name}");

	}

  public void initializeMainGameplayLoop()
  {
    
    
  }

}
