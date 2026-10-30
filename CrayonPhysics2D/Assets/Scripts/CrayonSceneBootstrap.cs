using UnityEngine;

namespace CrayonPhysics
{
    // Generates primitive geometry at runtime; no external sprites required.
    public class CrayonSceneBootstrap : MonoBehaviour
    {
        [Range(1,5)] public int level = 1;
        private void Start()
        {
            var cam = Camera.main;
            if (cam == null) { var go = new GameObject("Main Camera"); cam = go.AddComponent<Camera>(); go.tag = "MainCamera"; }
            cam.orthographic = true; cam.orthographicSize = 5.5f; cam.transform.position = new Vector3(0,0,-10); cam.backgroundColor = new Color(1f,.96f,.84f);
            CreateBox("Ground", new Vector2(0,-4.3f), new Vector2(18,.6f), new Color(.4f,.25f,.15f));
            CreateBox("LeftPlatform", new Vector2(-5,-2.8f), new Vector2(3,.25f), new Color(.55f,.37f,.2f));
            if (level >= 2) CreateBox("RightPlatform", new Vector2(4,-2.8f), new Vector2(3,.25f), new Color(.55f,.37f,.2f));
            if (level >= 3) CreateBox("Obstacle", new Vector2(0,-3.5f), new Vector2(.5f,1.5f), Color.gray);
            if (level >= 4) CreateBox("Obstacle2", new Vector2(2,-1.8f), new Vector2(1.2f,.25f), Color.gray);
            if (level >= 5) CreateBox("Obstacle3", new Vector2(-1,-1f), new Vector2(.3f,2f), Color.gray);
            var ball = CreateCircle("Ball", new Vector2(-5,-2.3f), .35f, new Color(.9f,.25f,.28f));
            ball.AddComponent<BallMarker>();
            var rb = ball.AddComponent<Rigidbody2D>(); rb.collisionDetectionMode = CollisionDetectionMode2D.Continuous;
            var goal = CreateCircle("Goal Star Placeholder", new Vector2(5,-3.5f), .52f, new Color(1f,.75f,.12f));
            goal.GetComponent<CircleCollider2D>().isTrigger = true;
            goal.AddComponent<GoalZone>();
            if (FindFirstObjectByType<DrawPhysics>() == null) new GameObject("Draw System").AddComponent<DrawPhysics>();
        }
        private GameObject CreateBox(string name, Vector2 p, Vector2 size, Color color)
        {
            var go = new GameObject(name); go.transform.position = p;
            var sprite = go.AddComponent<SpriteRenderer>(); sprite.sprite = Pixel(); sprite.color = color;
            go.transform.localScale = new Vector3(size.x,size.y,1);
            go.AddComponent<BoxCollider2D>(); return go;
        }
        private GameObject CreateCircle(string name, Vector2 p, float radius, Color color)
        {
            var go = new GameObject(name); go.transform.position = p;
            var sprite = go.AddComponent<SpriteRenderer>(); sprite.sprite = Pixel(); sprite.color = color;
            go.transform.localScale = new Vector3(radius*2,radius*2,1);
            var col = go.AddComponent<CircleCollider2D>(); col.radius = .5f; return go;
        }
        private static Sprite Pixel()
        {
            var t = Texture2D.whiteTexture;
            return Sprite.Create(t, new Rect(0,0,1,1), new Vector2(.5f,.5f), 1);
        }
    }
}
