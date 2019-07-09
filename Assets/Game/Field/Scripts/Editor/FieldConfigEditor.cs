using UnityEditor;
using UnityEngine;

namespace Game.Field
{
    [CustomEditor(typeof(FieldConfig))]
    public class FieldConfigEditor : Editor
    {
        public override void OnInspectorGUI()
        {   
            serializedObject.Update();
            
            FieldConfig myTarget = (FieldConfig)target;

            myTarget.WallPrefab =
                (GameObject) EditorGUILayout.ObjectField("Wall prefab", myTarget.WallPrefab, 
                    typeof(GameObject), false);

            EditorGUILayout.BeginHorizontal();
            myTarget.initialX = EditorGUILayout.IntField("initial x:", myTarget.initialX);
            myTarget.initialY = EditorGUILayout.IntField("initial y:", myTarget.initialY);
            EditorGUILayout.EndHorizontal();

            var matrixSize = FieldScript.FIELD_WIDTH * FieldScript.FIELD_HEIGHT;            
            if (myTarget.WallMatrix == null || 
                myTarget.WallMatrix.Length != matrixSize)
            {
                myTarget.WallMatrix = new bool[matrixSize];
            }

            for (int i = FieldScript.FIELD_HEIGHT - 1; i >= 0; i--)
            {
                EditorGUILayout.BeginHorizontal();
                for (int j = 0; j < FieldScript.FIELD_WIDTH; j++)
                {
                    var newValue = EditorGUILayout.Toggle(myTarget.GetMatrixValue(j, i));
                    myTarget.SetMatrixValue(j ,i, newValue);
                }
                EditorGUILayout.EndHorizontal();
            }
            
            EditorUtility.SetDirty(myTarget);
        }
    }
}