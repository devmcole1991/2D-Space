#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;

namespace Assets.GameLogic.Core
{
	[CustomEditor(typeof(ProbabilitySpawner))]
	public class ProbabilitySpawnerEditor : Editor
	{
		private SerializedProperty pairListProp;
		private int scoreSum = 0;

		private void OnEnable()
		{
			pairListProp = serializedObject.FindProperty("scoreGameObjectPairs");
		}

		public override void OnInspectorGUI()
		{
			serializedObject.Update();
			
			var divider = new GUIStyle(EditorStyles.toolbarButton);
			divider.border.top = divider.border.bottom = 1;
			divider.margin.top = divider.margin.bottom = 3;
			divider.fixedHeight = 2f;

			float labelWidth = GUI.skin.label.CalcSize(new GUIContent("Prefab ")).x;
			EditorGUIUtility.labelWidth = labelWidth;

			SerializedProperty pairProp;
			SerializedProperty scoreProp;
			SerializedProperty gameObjectProp;

			int removeIndex = -1;
			int count = pairListProp.arraySize;

			for (int i = 0; i < count; ++i)
			{
				EditorGUILayout.Space();

				pairProp = pairListProp.GetArrayElementAtIndex(i);
				scoreProp = pairProp.FindPropertyRelative("score");
				gameObjectProp = pairProp.FindPropertyRelative("gameObject");

				// Prefab
				EditorGUILayout.PropertyField(gameObjectProp, new GUIContent("Prefab"));

				// Score
				scoreSum -= scoreProp.intValue;

				EditorGUILayout.BeginHorizontal();
				EditorGUILayout.LabelField("Score", GUILayout.Width(labelWidth));
				EditorGUILayout.IntSlider(scoreProp, ProbabilitySpawner.MinScore, ProbabilitySpawner.MaxScore, GUIContent.none);
				EditorGUILayout.EndHorizontal();
				
				if (scoreSum + scoreProp.intValue > ProbabilitySpawner.MaxScore)
				{
					scoreProp.intValue = ProbabilitySpawner.MaxScore - scoreSum;
				}

				scoreSum += scoreProp.intValue;

				// Percent chance to spawn
				EditorGUILayout.BeginHorizontal();
				EditorGUILayout.HelpBox(((float)scoreProp.intValue / ProbabilitySpawner.MaxScore * 100f).ToString() + "% chance to spawn", MessageType.None);
				EditorGUILayout.EndHorizontal();

				// Right align remove button
				EditorGUILayout.BeginHorizontal();
				GUILayout.FlexibleSpace();

				if (GUILayout.Button("Remove", GUILayout.Width(70f)))
				{
					removeIndex = i;
					scoreSum -= scoreProp.intValue;
				}

				EditorGUILayout.EndHorizontal();

				// Insert divider
				GUILayout.Box("", divider, GUILayout.ExpandWidth(true));
			}

			EditorGUIUtility.labelWidth = 0f;
			EditorGUILayout.Space();

			// Remove item
			if (removeIndex >= 0)
			{
				pairListProp.DeleteArrayElementAtIndex(removeIndex);
			}

			// Total percent info
			EditorGUILayout.HelpBox(((float)scoreSum / ProbabilitySpawner.MaxScore * 100f).ToString() + "% chance to spawn an instance", MessageType.Info);

			// Add new item
			if (scoreSum + ProbabilitySpawner.MinScore <= ProbabilitySpawner.MaxScore && GUILayout.Button("Add Spawn"))
			{
				pairListProp.InsertArrayElementAtIndex(count);
				pairListProp.GetArrayElementAtIndex(count).FindPropertyRelative("score").intValue = ProbabilitySpawner.MinScore;
				pairListProp.GetArrayElementAtIndex(count).FindPropertyRelative("gameObject").objectReferenceValue = null;
				scoreSum += ProbabilitySpawner.MinScore;
			}

			serializedObject.ApplyModifiedProperties();
		}
	}
}
#endif