using UnityEngine;
using TMPro;
using Network;

namespace UI.Ranking
{    
    public class RankingNode : MonoBehaviour 
    {
        [SerializeField]
        private TextMeshProUGUI _rank;
        [SerializeField]
        private TextMeshProUGUI _name;

        private LifeModel _model;

        public void Init(int rank, LifeModel model)
        {
            _model = model;
            _rank.text = rank.ToString();
            _name.text = model.name;
        }
    }
}