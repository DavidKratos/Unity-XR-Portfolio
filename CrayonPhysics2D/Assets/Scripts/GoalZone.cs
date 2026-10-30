using UnityEngine;
using UnityEngine.Events;

namespace CrayonPhysics
{
    [RequireComponent(typeof(Collider2D))]
    public class GoalZone : MonoBehaviour
    {
        [SerializeField] private UnityEvent onReached;
        private bool reached;
        private void Reset() { GetComponent<Collider2D>().isTrigger = true; }
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (reached || other.GetComponent<BallMarker>() == null) return;
            reached = true;
            onReached?.Invoke();
            Debug.Log("Crayon Physics: Goal reached!");
        }
    }
    public class BallMarker : MonoBehaviour { }
}
