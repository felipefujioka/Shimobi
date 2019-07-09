using System;
using UnityEngine;

namespace Game.Field
{
    [CreateAssetMenu(menuName = "Shimobi/Field Config")]
    public class FieldConfig : ScriptableObject
    {
        public GameObject WallPrefab;
        [SerializeField]
        public bool[] WallMatrix;

        public int initialX;
        public int initialY;

        public bool GetMatrixValue(int x, int y)
        {
            if (ValidatePosition(x, y))
            {
                return WallMatrix[x + y * FieldScript.FIELD_WIDTH];
            }

            return true;
        }
        
        public void SetMatrixValue(int x, int y, bool value)
        {
            if (ValidatePosition(x, y))
            {
                WallMatrix[x + y * FieldScript.FIELD_WIDTH] = value;    
            }
        }

        private bool ValidatePosition(int x, int y)
        {
            return ValidateRange(x, 0, FieldScript.FIELD_WIDTH) &&
                   ValidateRange(y, 0, FieldScript.FIELD_HEIGHT);
        }
        
        private bool ValidateRange(int value, int min, int max)
        {
            return value < max && value >= min;
        }
    }
}