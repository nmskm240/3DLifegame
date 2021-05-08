using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace UI
{    
    public class MenuManager : MonoBehaviour 
    {
        [SerializeField]
        private Button _createModel;
        [SerializeField]
        private Button _ranking;
        [SerializeField]
        private Ranking.RankingViewer _rankingViewer;

        private void Awake()
        {
            _createModel.onClick.AddListener(() =>
            {
                SceneManager.LoadScene("Game");
            });
            _ranking.onClick.AddListener(() =>
            {
                _rankingViewer.Show();
            });
        }
    }
}