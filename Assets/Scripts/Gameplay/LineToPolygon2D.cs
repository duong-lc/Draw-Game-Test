using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Assertions;

namespace Gameplay
{
    public class PolygonPoint
    {
        public Vector3 center;
        public Vector3 left;
        public Vector3 right;
    }
    
    public class LineToPolygon2D
    {
        private float _circleRadius = .1f;
        private float _edgeRadius = .1f;

        private LineRenderer _line;
        private EdgeCollider2D _edgeCol;
        
        public void Initialize(float circleRad, float edgeRad, LineRenderer line, EdgeCollider2D edgeCol)
        {
            _circleRadius = circleRad;
            _edgeRadius   = edgeRad;

            _line = line;
            _edgeCol = edgeCol;
        }

        public void DeleteRef()
        {
            _line = null;
            _edgeCol = null;
        }

        public void Bake()
        {
            Assert.IsNotNull(_line);
            Assert.IsNotNull(_edgeCol);
            
            if (_line.positionCount < 2) return;
            var arr = new Vector2[_line.positionCount];
            for (int i = 0; i < _line.positionCount; i++)
            {
                arr[i] = _line.GetPosition(i);
                AddCircleCol(i, arr);
            }
            
            _edgeCol.points     = arr;
            _edgeCol.edgeRadius = _edgeRadius;
        }

        private void AddCircleCol(int i, Vector2[] points)
        {
            var circleCol = _edgeCol.AddComponent<CircleCollider2D>();
            circleCol.radius = _circleRadius;
            circleCol.offset = points[i];
        }
    }
}