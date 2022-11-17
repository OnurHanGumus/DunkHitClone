using UnityEngine;
using Signals;

namespace Controllers
{
    public class PlayerPhysicsController : MonoBehaviour
    {
        #region Self Variables

        #region Serialized Variables
        [SerializeField] private bool isPlayerUp = false;
        #endregion
        #region Private Variables

        #endregion
        #endregion

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("TopPota"))
            {
                isPlayerUp = true;
            }
            else if (other.CompareTag("BottomPota"))
            {
                if (isPlayerUp.Equals(true))
                {
                    LevelSignals.Instance.onBasket?.Invoke();
                }
            }
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.CompareTag("TopPota"))
            {
                isPlayerUp = false;
            }
        }
    }
}