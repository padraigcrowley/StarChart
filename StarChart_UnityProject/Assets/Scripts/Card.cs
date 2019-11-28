using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card 
{
	public GameObject cardGameObject;
	public Sprite cardFace;
  public int cardFaceIndex = -1;
	public Sprite cardBack;
	public bool hidden = true;
  bool cardShrinkingCompleted = false;


  public void TouchCard()
  {
    if (hidden)
      RevealCard();
    else
      HideCard();
  }

  public void RevealCard()
  {
    cardShrinkingCompleted = false;    
    StaticCoroutine.StartCoroutine(FlipCard(.5f, cardFace));   
    hidden = false;
    GameplayManager.Instance.starCount++;
  }


  public void HideCard()
  {
    //SpriteRenderer sr = cardGameObject.GetComponent<SpriteRenderer>();
    //sr.sprite = GameplayManager.Instance.cardBack;
    StaticCoroutine.StartCoroutine(FlipCard(.15f, cardBack));
    hidden = true;
    GameplayManager.Instance.starCount--;
  }


  public IEnumerator FlipCard(float time, Sprite newSide)
  {
    Vector3 originalScale = cardGameObject.transform.localScale;
    Vector3 destinationScale = new Vector3(0f, 0.49728f, 0f);

    float currentTime = 0.0f;

    do
    {
      cardGameObject.transform.localScale = Vector3.Lerp(originalScale, destinationScale, currentTime / time);
      currentTime += Time.deltaTime;
      yield return null;
    } while (currentTime <= time);



    SpriteRenderer sr = cardGameObject.GetComponent<SpriteRenderer>();
    sr.sprite = newSide;

    originalScale = cardGameObject.transform.localScale;
     destinationScale = new Vector3(0.49728f, 0.49728f, 0f);

     currentTime = 0.0f;

    do
    {
      cardGameObject.transform.localScale = Vector3.Lerp(originalScale, destinationScale, currentTime / time);
      currentTime += Time.deltaTime;
      yield return null;
    } while (currentTime <= time);


  }

  IEnumerator WaitAction()
  {
    yield return new WaitForSeconds(3);
    Debug.Log("Done");
    
  }

}


