using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using Network;
using Network.RequestDto;
using Network.ResponseDto;

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
        private Button _favorite;
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
            _favorite.onClick.AddListener(() => 
            {
                var url = NetworkManager.Instance.GetMethod(MethodType.PostModelFavorite);
                var request = new FavoriteLifeModelRequestDto()
                {
                    id = lifemodel.id,
                };
                StartCoroutine(NetworkManager.Instance.WebRequest.Post<FavoriteLifeModelRequestDto, NoneResponseDto>(url, request, response => { }, error =>
                {
                    var factory = new DialogFactory();
                    var dialog = factory.Create().GetComponent<Dialog>();
                    dialog.Show(DialogType.AgreeOnly, error);
                }, true));
            });
        }
    }
}