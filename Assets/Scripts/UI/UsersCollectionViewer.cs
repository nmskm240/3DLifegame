using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Network;
using Network.RequestDto;
using Network.ResponseDto;

namespace UI
{    
    public class UsersCollectionViewer : MonoBehaviour 
    {
        [SerializeField]
        private Transform _contents;
        [SerializeField]
        private Button _close;

        private void Awake() 
        {
            var url = NetworkManager.Instance.GetMethod(MethodType.GetModelUser);
            StartCoroutine(NetworkManager.Instance.WebRequest.Get<ModelUsersResponseDto>(url, response =>
            {
                var factory = new LifemodelNodeFactory();
                foreach(var model in response.life_model_list)
                {
                    var obj = factory.Create();
                    var node = obj.GetComponent<LifemodelNode>();
                    node.Init(model);
                    obj.transform.SetParent(_contents);
                    obj.transform.localScale = Vector3.one;
                }
            }, error =>
            {
                var factory = new DialogFactory();
                var dialog = factory.Create().GetComponent<Dialog>();
                dialog.Show(DialogType.AgreeOnly, error);
            }));
            _close.onClick.AddListener(() => 
            {
                SceneManager.LoadScene("Menu");
            });
        }
    }
}