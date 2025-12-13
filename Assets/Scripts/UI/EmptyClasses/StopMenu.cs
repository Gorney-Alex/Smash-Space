using UnityEngine;

namespace UI.EmptyClasses
{
    public class StopMenu : MonoBehaviour
    {
        public void OnHide() => gameObject.SetActive(false);
        public void OnReveal() => gameObject.SetActive(true);
    }
}