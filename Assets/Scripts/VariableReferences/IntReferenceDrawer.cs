using UnityEngine;
using UnityEditor;

namespace Assets.VariableReferences
{
	[CustomPropertyDrawer(typeof(IntReference))]
	public class IntReferenceDrawer : PropertyDrawer
	{
		private readonly string[] Content = new string[] { "Use Primitive", "Use Reference" };
		private const float PopupWidth = 17f;

		public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
		{
			SerializedProperty usePrimitiveProperty = property.FindPropertyRelative("usePrimitive");
			SerializedProperty primitiveProperty = property.FindPropertyRelative("primitive");
			SerializedProperty referenceProperty = property.FindPropertyRelative("reference");

			EditorGUI.BeginProperty(position, label, property);

			position = EditorGUI.PrefixLabel(position, GUIUtility.GetControlID(FocusType.Passive), label);

			var popupRect = new Rect(position.x, position.y, PopupWidth, position.height);
			int index = usePrimitiveProperty.boolValue ? 0 : 1;

			switch(EditorGUI.Popup(popupRect, index, Content))
			{
			case 0:
				usePrimitiveProperty.boolValue = true;
				EditorGUI.PropertyField(new Rect(position.x + PopupWidth, position.y, position.width - PopupWidth, position.height),
						primitiveProperty, GUIContent.none);
				break;
			case 1:
				usePrimitiveProperty.boolValue = false;
				EditorGUI.PropertyField(new Rect(position.x + PopupWidth, position.y, position.width - PopupWidth, position.height),
						referenceProperty, GUIContent.none);
				break;
			}

			EditorGUI.EndProperty();
		}
	}
}
