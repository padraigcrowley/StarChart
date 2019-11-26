using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card : MonoBehaviour
{
	public GameObject cardGameObject;
	public Sprite cardFace;
  public int cardFaceIndex = -1;
	public Sprite cardBack;
	public bool hidden = true;


  public void TouchCard()
  {
    if (hidden)
      RevealCard();
    else
      HideCard();
  }

  public void RevealCard()
  {
    SpriteRenderer sr = cardGameObject.GetComponent<SpriteRenderer>();
    sr.sprite = cardFace;
    hidden = false;
  }


  public void HideCard()
  {
    SpriteRenderer sr = cardGameObject.GetComponent<SpriteRenderer>();
    sr.sprite = GameplayManager.Instance.cardBack;
    hidden = true;
  }


  public IEnumerator ScaleOverTime(float time, Vector3 destScale)
  {
    Vector3 originalScale = cardGameObject.transform.localScale;
    Vector3 destinationScale = destScale;

    float currentTime = 0.0f;

    do
    {
      cardGameObject.transform.localScale = Vector3.Lerp(originalScale, destinationScale, currentTime / time);
      currentTime += Time.deltaTime;
      yield return null;
    } while (currentTime <= time);
    print("Got Here in coroutine!");
    //GameplayManager.Instance.cardShrinkingCompleted = true;
  }

}


