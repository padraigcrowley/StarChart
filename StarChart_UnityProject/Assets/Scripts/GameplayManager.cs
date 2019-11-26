using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameplayManager : Singleton<GameplayManager>
{
	public Sprite[] AllCardFaces;
  public Sprite cardBack;
	public GameObject[] cardsGameObjects = new GameObject[6];
	public Card[] cards = new Card[6];
	public GameObject lastTouchedGameObject;
	public bool cardTouchProcessed = true;
	List<int> dealtCardIndexes = new List<int>();
  int num = 0;
  public TextMeshProUGUI StarCountText;
  public int starCount = 0;


  void Start()
  {
		lastTouchedGameObject = null;
		dealtCardIndexes.Clear();
		shuffle(AllCardFaces);
		for(int i = 0;i<=5;i++)
		{
			Card c = new Card();
			cards[i] = c;
			cards[i].cardGameObject = cardsGameObjects[i];
      cards[i].cardFace = AllCardFaces[i];
      cards[i].cardBack = cardBack;
      //cards[i].HideCard();
    }    
	}

  // Update is called once peer frame
  void Update()
  {
    StarCountText.text = starCount.ToString();

    if (lastTouchedGameObject != null && cardTouchProcessed == false)
		{
			Debug.Log($"Object Name {lastTouchedGameObject.name}");
			int TouchedCardIndex = FindTouchedCardIndex(lastTouchedGameObject);
			Debug.Log($"Object Name from cards[{TouchedCardIndex}] : {cards[TouchedCardIndex].cardGameObject.name}");

      cards[TouchedCardIndex].TouchCard();
      cardTouchProcessed = true;

      /*if (cards[TouchedCardIndex].hidden)
			{
        cards[TouchedCardIndex].RevealCard();
        cardTouchProcessed = true;
      }
			else //hide the card 
			{
        cards[TouchedCardIndex].HideCard();
        cardTouchProcessed = true;
      }*/
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

	void shuffle(Sprite[] sprites)
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
