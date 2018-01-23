using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;// Required when using Event data.

public class MenuButtonController : MonoBehaviour, IPointerEnterHandler// required interface when using the OnPointerEnter method.
{
	//Do this when the cursor enters the rect area of this selectable UI object.
	public void OnPointerEnter(PointerEventData eventData)
	{
		MenuController.Instance.SetSelected (this.gameObject);
	}
}