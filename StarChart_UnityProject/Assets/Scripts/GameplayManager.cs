using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Linq;

public class GameplayManager : Singleton<GameplayManager>
{
	public Sprite[] AllCardFaces;
  public Sprite cardBack;
	public GameObject[] cardsGameObjects = new GameObject[6];
	public Card[] cards = new Card[6];
	public GameObject lastTouchedGameObject;
	public bool cardTouchProcessed = true;
	List<int> dealtCardIndexes = new List<int>();
  List<int> tempDealtCardIndexes = new List<int>();
  int num = 0;
  public TextMeshProUGUI StarCountText;
  public int starCount = 0;
  int firstTimePlay = 1;
  int card0Hidden=1, card1Hidden=1, card2Hidden = 1, card3Hidden = 1, card4Hidden = 1, card5Hidden = 1;


  void Start()
  {
    //PlayerPrefs.DeleteAll(); // test code - delete me
    
    firstTimePlay = PlayerPrefs.GetInt("firstTimePlay",1);
    print($"firstTimePlay = {firstTimePlay}");

    if (firstTimePlay==1)
    {
      print("First time playing!");
      RandomizeAndAssignCards();
      SaveAssignedCards();
            
      for (int i = 0; i <= 5; i++)
      {
        Card c = new Card();
        cards[i] = c;
        cards[i].cardGameObject = cardsGameObjects[i];
        cards[i].cardFace = AllCardFaces[dealtCardIndexes[i]];
        cards[i].cardFaceIndex = dealtCardIndexes[i];
        cards[i].cardNumber = i;
        cards[i].cardBack = cardBack;
        //cards[i].HideCard();
      }
      
      firstTimePlay = 0;
      PlayerPrefs.SetInt("firstTimePlay", 0);
      PlayerPrefs.Save();
    }
    else
    {
      print("NOT first time playing!");
      LoadAssignedCards();
    }
        
    lastTouchedGameObject = null;
		
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
    }
	}

  public void Reset()
  {
    RandomizeAndAssignCards();
    SaveAssignedCards();

    for (int i = 0; i <= 5; i++)
    {
      cards[i].cardGameObject = cardsGameObjects[i];
      cards[i].cardFace = AllCardFaces[dealtCardIndexes[i]];
      cards[i].cardFaceIndex = dealtCardIndexes[i];
      cards[i].cardNumber = i;
      cards[i].cardBack = cardBack;
      if(!cards[i].hidden)  
        cards[i].HideCard();
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

  private void RandomizeAndAssignCards() 
  {
    //put the numbers 0 to [AllCardFaces.Length] into the list
    for (int i = 0; i < AllCardFaces.Length; i++) { tempDealtCardIndexes.Add(i); }

    // shuffle the list https://forum.unity.com/threads/clever-way-to-shuffle-a-list-t-in-one-line-of-c-code.241052/
    dealtCardIndexes = tempDealtCardIndexes.OrderBy(x => Random.value).ToList();
  }

  void SaveAssignedCards()
  {
    //save that shuffled list to PlayerPrefs
    PlayerPrefs.SetInt("card0", dealtCardIndexes[0]);
    PlayerPrefs.SetInt("card1", dealtCardIndexes[1]);
    PlayerPrefs.SetInt("card2", dealtCardIndexes[2]);
    PlayerPrefs.SetInt("card3", dealtCardIndexes[3]);
    PlayerPrefs.SetInt("card4", dealtCardIndexes[4]);
    PlayerPrefs.SetInt("card5", dealtCardIndexes[5]);
    PlayerPrefs.Save();
  }

  void LoadAssignedCards()
  {

    for (int i = 0; i <= 5; i++)
    {
      dealtCardIndexes.Add(PlayerPrefs.GetInt("card"+i));
      Card c = new Card();
      cards[i] = c;
      cards[i].cardGameObject = cardsGameObjects[i];
      cards[i].cardFace = AllCardFaces[dealtCardIndexes[i]];
      cards[i].cardFaceIndex = dealtCardIndexes[i];
      cards[i].cardNumber = i;
      cards[i].cardBack = cardBack;
    }
    
    card0Hidden = PlayerPrefs.GetInt("card0Hidden",1);
    if (card0Hidden == 0) cards[0].RevealCard();
    card1Hidden = PlayerPrefs.GetInt("card1Hidden",1);
    if (card1Hidden == 0) cards[1].RevealCard();
    card2Hidden = PlayerPrefs.GetInt("card2Hidden",1);
    if (card2Hidden == 0) cards[2].RevealCard();
    card3Hidden = PlayerPrefs.GetInt("card3Hidden",1);
    if (card3Hidden == 0) cards[3].RevealCard();
    card4Hidden = PlayerPrefs.GetInt("card4Hidden",1);
    if (card4Hidden == 0) cards[4].RevealCard();
    card5Hidden = PlayerPrefs.GetInt("card5Hidden",1);
    if (card5Hidden == 0) cards[5].RevealCard();
  }

  /*void shuffle(Sprite[] sprites)
    {
			// Knuth shuffle algorithm :: courtesy of Wikipedia :)
        for (int t = 0; t < sprites.Length; t++ )
        {
						Sprite tmp = sprites[t];
            int r = Random.Range(t, sprites.Length);
						sprites[t] = sprites[r];
						sprites[r] = tmp;
        }
    }*/

}
