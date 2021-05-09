using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using TMPro;
using Network;
using Network.RequestDto;
using Network.ResponseDto;

namespace UI
{    
    public class LifemodelNode : MonoBehaviour, IPointerClickHandler 
    {
        [SerializeField]
        private TextMeshProUGUI _rank;
        [SerializeField]
        private TextMeshProUGUI _name;
        [SerializeField]
        private TextMeshProUGUI _creatorInfo;
        [SerializeField]
        private TextMeshProUGUI _favoriteInfo;
        [SerializeField]
        private Button _favorite;
        [SerializeField]
        private LoadStageData _out;

        private LifeModel _model;

        private void Awake() 
        {
            _favorite.onClick.AddListener(() => 
            {
                var url = NetworkManager.Instance.GetMethod(MethodType.PostModelFavorite);
                var request = new FavoriteLifeModelRequestDto()
                {
                    id = _model.id,
                };
                StartCoroutine(NetworkManager.Instance.WebRequest.Post<FavoriteLifeModelRequestDto, NoneResponseDto>(url, request, x => {  }, error =>
                {
                    var factory = new DialogFactory();
                    var dialog = factory.Create().GetComponent<Dialog>();
                    dialog.Show(DialogType.Error, error.Contains("500") ? "Already have favorited" : error);
                }, true));
            });
        }

        public void Init(int rank, LifeModel model)
        {
            _model = model;
            _rank.text = rank.ToString();
            _name.text = model.name;
            _creatorInfo.text = model.user.name;
            _favoriteInfo.text = model.favorite.ToString();
        }

        public void Init(LifeModel model)
        {
            _model = model;
            _rank.text = "";
            _name.text = model.name;
            _creatorInfo.text = model.user.name;
            _favoriteInfo.text = model.favorite.ToString();
        }

        public void OnPointerClick(PointerEventData e)
        {
            var factory = new DialogFactory();
            var dialog = factory.Create().GetComponent<Dialog>();
            dialog.Show(DialogType.Switch, _model.name + " play?", agree => 
            {
                _out.Copy(_model);
                SceneManager.LoadScene("Game");
            });
        }
    }
}