using UnityEngine;
using UnityEngine.UI;
using Network;
using Network.RequestDto;
using Network.ResponseDto;

namespace UI.Ranking
{
    public class RankingViewer : MonoBehaviour
    {
        [SerializeField]
        private Button _close;
        [SerializeField]
        private Transform _contents;

        private void Awake()
        {
            gameObject.SetActive(false);
            _close.onClick.AddListener(() =>
            {
                gameObject.SetActive(false);
            });
        }

        public void Show()
        {
            foreach (Transform tf in _contents)
            {
                Destroy(tf.gameObject);
            }
            gameObject.SetActive(true);
            var url = NetworkManager.Instance.GetMethod(MethodType.GetRanking);
            StartCoroutine(NetworkManager.Instance.WebRequest.Get<RankingLifeModelResponseDto>(url, response =>
            {
                var rank = 1;
                var factory = new RankingNodeFactory();
                foreach (var model in response.life_model_list)
                {
                    var obj = factory.Create();
                    var node = obj.GetComponent<RankingNode>();
                    node.Init(rank++, model);
                    obj.transform.SetParent(_contents);
                    obj.transform.localScale = Vector3.one;
                }
            }));
        }
    }
}