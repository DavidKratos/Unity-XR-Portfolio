using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace CrayonPhysics
{
    public class DrawPhysics : MonoBehaviour
    {
        [SerializeField] private Camera worldCamera;
        [SerializeField] private Material lineMaterial;
        [SerializeField] private float lineWidth = 0.12f;
        [SerializeField] private float minSpacing = 0.12f;
        [SerializeField] private int maxPoints = 180;
        [SerializeField] private float inkBudget = 24f;
        private readonly List<Vector2> points = new List<Vector2>();
        private GameObject current;
        private LineRenderer rendererLine;
        private float usedInk;
        public float RemainingInk => Mathf.Max(0f, inkBudget - usedInk);

        private void Awake() { if (worldCamera == null) worldCamera = Camera.main; }
        private void Update()
        {
            if (worldCamera == null) return;
            if (Input.GetMouseButtonDown(0) && (EventSystem.current == null || !EventSystem.current.IsPointerOverGameObject())) Begin();
            if (Input.GetMouseButton(0) && current != null) Append();
            if (Input.GetMouseButtonUp(0) && current != null) Finish();
        }
        private Vector2 PointerWorld()
        {
            Vector3 p = worldCamera.ScreenToWorldPoint(Input.mousePosition);
            return new Vector2(p.x, p.y);
        }
        private void Begin()
        {
            if (RemainingInk <= 0f) return;
            current = new GameObject("DrawnCrayonStroke");
            current.transform.SetParent(transform, true);
            rendererLine = current.AddComponent<LineRenderer>();
            rendererLine.useWorldSpace = false;
            rendererLine.positionCount = 0;
            rendererLine.startWidth = rendererLine.endWidth = lineWidth;
            rendererLine.numCapVertices = 6;
            rendererLine.numCornerVertices = 4;
            rendererLine.material = lineMaterial != null ? lineMaterial : new Material(Shader.Find("Sprites/Default"));
            rendererLine.startColor = rendererLine.endColor = new Color(0.15f, 0.35f, 0.8f);
            points.Clear(); usedInk = 0f;
            Append();
        }
        private void Append()
        {
            if (points.Count >= maxPoints || RemainingInk <= 0f) return;
            Vector2 p = PointerWorld();
            if (points.Count > 0)
            {
                float delta = Vector2.Distance(points[points.Count - 1], p);
                if (delta < minSpacing) return;
                if (delta > RemainingInk) p = points[points.Count - 1] + (p - points[points.Count - 1]).normalized * RemainingInk;
                usedInk += Vector2.Distance(points[points.Count - 1], p);
            }
            points.Add(p);
            rendererLine.positionCount = points.Count;
            rendererLine.SetPosition(points.Count - 1, new Vector3(p.x, p.y, 0));
        }
        private void Finish()
        {
            if (points.Count < 2) { Destroy(current); current = null; return; }
            var edge = current.AddComponent<EdgeCollider2D>();
            edge.points = points.ToArray();
            edge.edgeRadius = lineWidth * 0.35f;
            var rb = current.AddComponent<Rigidbody2D>();
            rb.bodyType = RigidbodyType2D.Dynamic;
            rb.mass = Mathf.Max(.2f, usedInk * .1f);
            current = null;
        }
    }
}
