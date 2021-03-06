using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace UI
{    
    public class Dialog : MonoBehaviour 
    {
        [SerializeField]
        private TextMeshProUGUI _title;
        [SerializeField]
        private TextMeshProUGUI _body;
        [SerializeField]
        private GameObject _agree;
        [SerializeField]
        private GameObject _disAgree;

        private void Awake() 
        {
            gameObject.SetActive(false);
        }

        public void Show(DialogType type, string text, Action<string> onAgree = null, Action<string> onDisAgree = null)
        {
            var agree = _agree.GetComponent<Button>();
            var disAgree = _disAgree.GetComponent<Button>();
            _title.text = (type == DialogType.Error) ? "ERROR" : "";
            _body.text = text;
            agree.onClick.AddListener(() => onAgree?.Invoke(null));
            agree.onClick.AddListener(() => { Destroy(gameObject); });
            disAgree.onClick.AddListener(() => onDisAgree?.Invoke(null));
            disAgree.onClick.AddListener(() => { Destroy(gameObject); });
            _disAgree.SetActive(type == DialogType.Switch);
            gameObject.SetActive(true);
        }
    }
}