
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class LongClickButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
  private bool pointerDown;
  private float pointerDownTimer;
  public float requiredHoldTime;
  public UnityEvent onLongClick;

  [SerializeField]
  private Image fillImage;

  public void OnPointerDown(PointerEventData eventData)
  {
    pointerDown = true;
    print("OnPointerDown");
  }

  public void OnPointerUp(PointerEventData eventData)
  {
    Reset();
    print("OnPointerUp");

  }

  private void Update()
  {
    if(pointerDown)
    {
      pointerDownTimer += Time.deltaTime;
      if (pointerDownTimer >= requiredHoldTime)
      {
        if (onLongClick != null)
          onLongClick.Invoke();
        Reset();
      }
      fillImage.fillAmount = pointerDownTimer / requiredHoldTime;
    }
  }

  private void Reset()
  {
    pointerDown = false;
    pointerDownTimer = 0;
    fillImage.fillAmount = pointerDownTimer / requiredHoldTime;
  }
}
