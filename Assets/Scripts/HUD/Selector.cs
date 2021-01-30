using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

[RequireComponent(typeof(TMP_Text))]
public class Selector : MonoBehaviour
{
	private TMP_Text text;
	private RectTransform rectTransform;

	private void Start()
	{
		text = GetComponent<TMP_Text>();
		rectTransform = GetComponent<RectTransform>();
		text.color = new Color(text.color.r, text.color.g, text.color.b, 0f);
	}

	public void OnEnter(BaseEventData data)
	{
		PointerEventData pointerEvent = (PointerEventData)data;

		RectTransform rect = pointerEvent.pointerCurrentRaycast.gameObject.transform.parent.GetComponent<RectTransform>();
		rectTransform.localPosition = rect.offsetMin;

		text.color = new Color(text.color.r, text.color.g, text.color.b, 1f);
	}

	public void OnLeave(BaseEventData data)
	{
		text.color = new Color(text.color.r, text.color.g, text.color.b, 0f);
	}
}
