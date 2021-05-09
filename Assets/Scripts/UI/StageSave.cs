using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using Lifegame;
using Network;
using Network.RequestDto;
using Network.ResponseDto;

namespace UI
{    
    public class StageSave : MonoBehaviour 
    {
        [SerializeField]
        private Button _save;
        [SerializeField]
        private TMP_InputField _inputField;

        private void Awake() 
        {
            var stage = GameObject.Find("Stage").GetComponent<Stage>();
            _save.interactable = !string.IsNullOrEmpty(_inputField.text);
            _save.onClick.AddListener(() => 
            {
                var map = new int[stage.Width * stage.Height * stage.Length];
                var url = NetworkManager.Instance.GetMethod(MethodType.PostModelCreate);
                var tmp = 0;
                foreach(var cell in stage.Map)
                {
                    map[tmp] = Convert.ToInt32(cell.IsAlive);
                }
                var request = new CreateLifeModelRequestDto()
                {
                    name = _inputField.text,
                    map = map,
                };
                StartCoroutine(NetworkManager.Instance.WebRequest.Post<CreateLifeModelRequestDto, NoneResponseDto>(url, request, x =>
                {
                    SceneManager.LoadScene("Menu");
                }, error =>
                {
                    var factory = new DialogFactory();
                    var dialog = factory.Create().GetComponent<Dialog>();
                    dialog.Show(DialogType.Error, error);
                }, true));
            });    
        }
    }
}