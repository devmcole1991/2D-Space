using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using Assets.SOVariables;

namespace Assets.UI
{
	[RequireComponent(typeof(RectTransform))]
	[RequireComponent(typeof(Image))]
	public class SimpleHealthBar : UpdatableUI
	{
		[SerializeField] private ScriptableInt health;
		[SerializeField] private ScriptableInt maxHealth;
		private RectTransform rectTransform;
		private int segmentSize;

		private void Awake()
		{
			rectTransform = GetComponent<RectTransform>();
			segmentSize = (int)GetComponent<Image>().sprite.bounds.size.x;
		}

		public override void OnUpdate()
		{
			if (rectTransform.rect.width / segmentSize != health.Value)
			{
				var sizeDelta = rectTransform.sizeDelta;
				sizeDelta.x = health.Value * segmentSize;
				rectTransform.sizeDelta = sizeDelta; 
			}
		}
	}
}
