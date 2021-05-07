using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Network.RequestDto;
using Network.ResponseDto;

namespace UI
{    
    public class TitleManager : MonoBehaviour 
    {
        [SerializeField]
        private Button _startButton;

        private void Awake() 
        {
            _startButton.onClick.AddListener(() => 
            {
                var request = new UserCreateRequestDto()
                {
                    name = "test240"
                };
                var url = NetworkManager.Instance.GetMethod(MethodType.PostUserCreate);
                StartCoroutine(NetworkManager.Instance.WebRequest.Post<UserCreateRequestDto, UserCreateResponseDto>(url, request, response =>
                {
                    NetworkManager.Instance.WebRequest.SetToken(response.token);
                    SceneManager.LoadScene("Menu");
                }, Debug.LogError));
            });
        }
    }
}