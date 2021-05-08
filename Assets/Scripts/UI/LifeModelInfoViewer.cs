using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using Network;

namespace UI
{    
    public class LifeModelInfoViewer : MonoBehaviour 
    {
        [SerializeField]
        private TextMeshProUGUI _main;
        [SerializeField]
        private Button _close;
        [SerializeField]
        private Button _play;
        [SerializeField]
        private LoadStageData _loadData;

        private void Awake() 
        {
            var lifemodel = _loadData.LifeModel;
            _main.text = "id:" + lifemodel.id + "\n"
                + "name:" + lifemodel.name + "\n"
                + "user:" + lifemodel.user + "\n"
                + "favorite" + lifemodel.favorite;
            _close.onClick.AddListener(() => 
            {
                _loadData.Reset();
                SceneManager.LoadScene("Menu");
            });
            _play.onClick.AddListener(() => 
            {
                SceneManager.LoadScene("Game");
            });
        }
    }
}