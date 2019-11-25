using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameplayManager : Singleton<GameplayManager>
{
	public Sprite[] AllCardFaces;
	public GameObject[] cardsGameObjects = new GameObject[6];
	public Card[] cards = new Card[6];
	public GameObject lastTouchedGameObject;
	public bool cardTouchProcessed = true;
	List<int> dealtCardIndexes = new List<int>();

	void Start()
  {
		lastTouchedGameObject = null;
		dealtCardIndexes.Clear();
		//reshuffle(AllCardFaces);
		for(int i = 0;i<=5;i++)
		{
			//Card card[i] = new Card();
			cards[i].cardGameObject = cardsGameObjects[i];
		}
	}

  // Update is called once peer frame
  void Update()
  {
		int num;
		if (lastTouchedGameObject != null && cardTouchProcessed == false)
		{
			Debug.Log($"Object Name {lastTouchedGameObject.name}");
			int i = FindTouchedCardIndex(lastTouchedGameObject);
			Debug.Log($"Object Name from cards[{i}] : {cards[i].cardGameObject.name}");
			SpriteRenderer sr = lastTouchedGameObject.GetComponent<SpriteRenderer>();
			do
			{
				num = Random.Range(1, AllCardFaces.Length);
			} while (dealtCardIndexes.Contains(num) );
			dealtCardIndexes.Add(num);
			sr.sprite = AllCardFaces[num];
			cardTouchProcessed = true;
		}
	}

	int FindTouchedCardIndex(GameObject cardObject)
	{
		for (int i = 0; i <= 5; i++)
		{
			if (cards[i].cardGameObject == cardObject)
				return i;
		}
		return -1;
	}

	void reshuffle(Sprite[] sprites)
    {
			// Knuth shuffle algorithm :: courtesy of Wikipedia :)
        for (int t = 0; t < sprites.Length; t++ )
        {
						Sprite tmp = sprites[t];
            int r = Random.Range(t, sprites.Length);
						sprites[t] = sprites[r];
						sprites[r] = tmp;
        }
    }

}
