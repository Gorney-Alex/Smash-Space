using UnityEngine;

namespace UI.EmptyClasses
{
    public class MainMenu : MonoBehaviour
    {
        public void OnHide() => gameObject.SetActive(false);
        public void OnReveal() => gameObject.SetActive(true);
    }
}