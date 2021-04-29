using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Lifegame.Rules;

namespace Lifegame
{    
    public class GameManager : MonoBehaviour 
    {
        [SerializeField]
        private Stage _stage;
        [SerializeField]
        private float _frameDuration = 1.0f;

        private bool _isPause = true;
        private IRule _rule = new Default2D();
        private Coroutine _process;

        private void OnGUI()
        {
            if(GUI.Button(new Rect(0,0,100,20), (_isPause) ? "▶" : "■"))
            {
                if(_isPause)
                {
                    _isPause = false;
                    Setup();
                    _process = StartCoroutine("Play");
                }
                else
                {
                    _isPause = true;
                    StopCoroutine(_process);
                }
            }
        }

        private IEnumerator Play()
        {
            while(true)
            {
                foreach(var cell in _stage.Map)
                {
                    cell.SetNextAlive(_rule);
                }
                yield return new WaitForSeconds(_frameDuration);
                foreach(var cell in _stage.Map)
                {
                    cell.Next();
                }
            }
        }

        public void Setup()
        {
            _stage.Create(10,10,10,500);
        }
    }
}