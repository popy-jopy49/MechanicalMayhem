using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections.Generic;
using System;
using System.Threading.Tasks;

namespace SWAssets.Utils
{
	public class UIUtils
	{
		private static LayerMask UILayer;

		static UIUtils()
		{
			UILayer = LayerMask.NameToLayer("UI");
		}

		#region UIPointer
		// Returns 'true' if we touched or hovering on Unity UI element.
		public static bool IsPointerOverUIElement()
		{
			return EventSystem.current ? IsPointerOverUIElement(GetEventSystemRaycastResults()) : false;
		}

		// Returns 'true' if we touched or hovering on Unity UI element.
		private static bool IsPointerOverUIElement(List<RaycastResult> eventSystemRaysastResults)
		{
			for (int index = 0; index < eventSystemRaysastResults.Count; index++)
			{
				RaycastResult curRaysastResult = eventSystemRaysastResults[index];
				if (curRaysastResult.gameObject.layer == UILayer)
					return true;
			}
			return false;
		}

		//Gets all event system raycast results of current mouse or touch position.
		static List<RaycastResult> GetEventSystemRaycastResults()
		{
			PointerEventData eventData = new PointerEventData(EventSystem.current);
			eventData.position = Input.mousePosition;
			List<RaycastResult> raysastResults = new List<RaycastResult>();
			EventSystem.current.RaycastAll(eventData, raysastResults);
			return raysastResults;
		}
#endregion

		#region Image
		// Draw a UI Sprite
		public static RectTransform CreateUIImage(Color color, Transform parent, Vector2 pos, Vector2 size, string name = null)
		{
			RectTransform rectTransform = CreateUIImage(null, color, parent, pos, size, name);
			return rectTransform;
		}

		// Draw a UI Sprite
		public static RectTransform CreateUIImage(Sprite sprite, Transform parent, Vector2 pos, Vector2 size, string name = null)
		{
			RectTransform rectTransform = CreateUIImage(sprite, Color.white, parent, pos, size, name);
			return rectTransform;
		}

		// Draw a UI Sprite
		public static RectTransform CreateUIImage(Sprite sprite, Color color, Transform parent, Vector2 pos, Vector2 size, string name = null)
		{
			// Setup icon
			if (name == null || name == "") name = "Sprite";
			GameObject go = new GameObject(name, typeof(RectTransform), typeof(Image));
			RectTransform goRectTransform = go.GetComponent<RectTransform>();
			goRectTransform.SetParent(parent, false);
			goRectTransform.sizeDelta = size;
			goRectTransform.anchoredPosition = pos;

			Image image = go.GetComponent<Image>();
			image.sprite = sprite;
			image.color = color;

			return goRectTransform;
		}
		#endregion

		#region Text
		public static Font GetDefaultFont() => Resources.GetBuiltinResource<Font>("Arial.ttf");

		public static Text DrawUIText(string textString, Vector2 anchoredPosition, int fontSize, Font font)
		{
			return DrawUIText(textString, GetCanvasTransform(), anchoredPosition, fontSize, font);
		}

		public static Text DrawUIText(string textString, Transform parent, Vector2 anchoredPosition, int fontSize, Font font)
		{
			GameObject textGo = new GameObject("Text", typeof(RectTransform), typeof(Text));
			textGo.transform.SetParent(parent, false);
			Transform textGoTrans = textGo.transform;
			textGoTrans.SetParent(parent, false);
			textGoTrans.localPosition = Vector3.zero;
			textGoTrans.localScale = Vector3.one;

			RectTransform textGoRectTransform = textGo.GetComponent<RectTransform>();
			textGoRectTransform.sizeDelta = new Vector2(0, 0);
			textGoRectTransform.anchoredPosition = anchoredPosition;

			Text text = textGo.GetComponent<Text>();
			text.text = textString;
			text.verticalOverflow = VerticalWrapMode.Overflow;
			text.horizontalOverflow = HorizontalWrapMode.Overflow;
			text.alignment = TextAnchor.MiddleLeft;
			if (font == null) font = GetDefaultFont();
			text.font = font;
			text.fontSize = fontSize;

			return text;
		}

		// Get Main Canvas Transform
		private static Transform cachedCanvasTransform;
		public static Transform GetCanvasTransform()
		{
			if (cachedCanvasTransform == null)
			{
				Canvas canvas = UnityEngine.Object.FindObjectOfType<Canvas>();
				if (canvas != null)
				{
					cachedCanvasTransform = canvas.transform;
				}
			}
			return cachedCanvasTransform;
		}

		public static class World
		{
			// Creates a Text Mesh in the World and constantly updates it
			public static async void CreateWorldTextUpdater(Func<string> GetTextFunc, Vector3 localPosition, Transform parent = null, int fontSize = 40, Color? color = null, TextAnchor textAnchor = TextAnchor.UpperLeft, TextAlignment textAlignment = TextAlignment.Left, int sortingOrder = 5000)
			{
				TextMesh textMesh = CreateWorldText(GetTextFunc(), parent, localPosition, fontSize, color, textAnchor, textAlignment, sortingOrder);

				while (true)
				{
					textMesh.text = GetTextFunc();
					await Task.Yield();
				}
			}

			// Create Text in the World
			public static TextMesh CreateWorldText(string text, Transform parent = null, Vector3 localPosition = default, int fontSize = 40, Color? color = null, TextAnchor textAnchor = TextAnchor.UpperLeft, TextAlignment textAlignment = TextAlignment.Left, int sortingOrder = 5000)
			{
				if (color == null) color = Color.white;
				return CreateWorldText(parent, text, localPosition, fontSize, (Color)color, textAnchor, textAlignment, sortingOrder);
			}

			// Create Text in the World
			public static TextMesh CreateWorldText(Transform parent, string text, Vector3 localPosition, int fontSize, Color color, TextAnchor textAnchor, TextAlignment textAlignment, int sortingOrder)
			{
				GameObject gameObject = new GameObject("World_Text", typeof(TextMesh));
				Transform transform = gameObject.transform;
				transform.SetParent(parent, false);
				transform.localPosition = localPosition;
				TextMesh textMesh = gameObject.GetComponent<TextMesh>();
				textMesh.anchor = textAnchor;
				textMesh.alignment = textAlignment;
				textMesh.text = text;
				textMesh.fontSize = fontSize;
				textMesh.color = color;
				textMesh.GetComponent<MeshRenderer>().sortingOrder = sortingOrder;
				return textMesh;
			}


			// Create a Text Popup in the World, no parent
			public static void CreateWorldTextPopup(string text, Vector3 localPosition, float popupTime = 1f)
			{
				CreateWorldTextPopup(null, text, localPosition, 40, Color.white, localPosition + new Vector3(0, 20), popupTime);
			}

			// Create a Text Popup in the World
			public static async void CreateWorldTextPopup(Transform parent, string text, Vector3 localPosition, int fontSize, Color color, Vector3 finalPopupPosition, float popupTime)
			{
				TextMesh textMesh = CreateWorldText(parent, text, localPosition, fontSize, color, TextAnchor.LowerLeft, TextAlignment.Left, 5000);
				Transform transform = textMesh.transform;
				Vector3 moveAmount = (finalPopupPosition - localPosition) / popupTime;

				while (popupTime > 0f)
				{
					transform.position += moveAmount * Time.deltaTime;
					popupTime -= Time.deltaTime;
					await Task.Yield();
				}
				UnityEngine.Object.Destroy(transform.gameObject);
			}
		}
		#endregion
	}
}
