using MVVM;
using TMPro;
using UnityEngine;

namespace UI.Views
{
    public class TextView : MonoBehaviour
    {
        [Data("ViewText")]
        public TextMeshProUGUI viewText;
    }
}