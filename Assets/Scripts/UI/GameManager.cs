using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Lifegame;
using Network;
using Network.RequestDto;
using Network.ResponseDto;

namespace UI
{    
    public class GameManager : MonoBehaviour 
    {
        [SerializeField]
        private Stage _stage;
        [SerializeField]
        private Button _play;
        [SerializeField]
        private Button _pause;
        [SerializeField]
        private Button _reset;

        private Coroutine _process = null;

        private void Awake() 
        {
            _play.onClick.AddListener(() =>
            {
                _play.interactable = false;
                _pause.interactable = true;
                if(!_stage.IsPause){ _process = StartCoroutine(_stage.Play()); }
                _stage.IsPause = false;
            });
            _pause.onClick.AddListener(() => 
            {
                _play.interactable = true;
                _pause.interactable = false;
                _stage.IsPause = true;
            });
            _reset.onClick.AddListener(() => 
            {
                if(_process != null){ StopCoroutine(_process); }
                _stage.Create(500);
                _play.interactable = true;
                _pause.interactable = true;
            });
        }
    }
}