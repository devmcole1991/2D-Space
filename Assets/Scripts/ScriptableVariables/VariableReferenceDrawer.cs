#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;

namespace Assets.SOVariables
{
	[CustomPropertyDrawer(typeof(VariableReferenceBase), true)]
	public class VariableReferenceDrawer : PropertyDrawer
	{
		private readonly string[] Content = new string[] { "Use Variable", "Use Scriptable Variable" };
		private const float PopupWidth = 17f;

		public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
		{
			SerializedProperty useVariableProperty = property.FindPropertyRelative("useVariable");
			SerializedProperty variableProperty = property.FindPropertyRelative("variable");
			SerializedProperty scriptableVariableProperty = property.FindPropertyRelative("scriptableVariable");

			EditorGUI.BeginProperty(position, label, property);

			position = EditorGUI.PrefixLabel(position, GUIUtility.GetControlID(FocusType.Passive), label);

			var popupRect = new Rect(position.x, position.y, PopupWidth, position.height);
			int index = useVariableProperty.boolValue ? 0 : 1;

			switch (EditorGUI.Popup(popupRect, index, Content))
			{
			case 0:
				useVariableProperty.boolValue = true;
				EditorGUI.PropertyField(new Rect(position.x + PopupWidth, position.y, position.width - PopupWidth, position.height),
						variableProperty, GUIContent.none);
				break;
			case 1:
				useVariableProperty.boolValue = false;
				EditorGUI.PropertyField(new Rect(position.x + PopupWidth, position.y, position.width - PopupWidth, position.height),
						scriptableVariableProperty, GUIContent.none);
				break;
			}

			EditorGUI.EndProperty();
		}
	}
}
#endif
