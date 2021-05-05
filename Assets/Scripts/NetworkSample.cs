using System.Collections;
using ProjectConnect.Network;
using ProjectConnect.Network.RequestDto;
using ProjectConnect.Network.ResponseDto;
using UnityEngine;

public class NetworkSample : MonoBehaviour
{
    /// <summary>
    /// サーバの接続先アドレスを設定
    /// TODO: ここに指定したアドレスを入力してください
    ///       インターンでの開発では、バックエンドメンバーが用意したアドレスを設定します。
    ///       このサンプル内でもやっているWebRequest.SetServerAddress()への設定を忘れないようにしてください。
    /// </summary>
    /// private const string ServerAddress = "http://35.72.193.240/";
    private const string ServerAddress = "http://lifegame-3d.herokuapp.com"; //ここが変わる
    
    /// <summary>
    /// メソッドタイプ
    /// </summary>
    public enum MethodType
    {
        // Postメソッド系
//        PostUserUpdate,
        PostUserCreate,

        PostModelCreate,
//        PostModelFavorite,
        
        // Getメソッド系

        GetUser,

        GetModel,
        GetRanking,
        GetModelUser
    }
    
    void Start()
    {
        StartCoroutine(NetworkCoroutine());
    }

    /// <summary>
    /// 通信サンプル用のCoroutine
    /// </summary>
    private IEnumerator NetworkCoroutine()
    {
        // WebRequestクラスをインスタンス化
        var webRequest = new WebRequest();

        // サーバーアドレスを設定する
        webRequest.SetServerAddress(ServerAddress);

        // ユーザデータ作成　コルーチン
        yield return CreateUser(webRequest);

        // ユーザー情報取得　・・
        yield return GetUserInfo(webRequest);

        // モデル作成
        yield return CreateModel(webRequest);

        // モデル取得
        yield return GetModel(webRequest);

//        // お気に入り
//        yield return FavoriteModel(webRequest);

        // ランキング
        yield return RankingModel(webRequest);

        //ユーザー
        yield return UserModel(webRequest);
    }

    /// <summary>
    /// ユーザデータの作成
    /// </summary>
    /// <param name="webRequest">Webリクエスト</param>
    /// <returns></returns>
    private IEnumerator CreateUser(WebRequest webRequest)
    {
        // TODO: 【事前課題】ユーザ作成用Dtoクラスを作成し、トークンを取得してください。
        //       レギュレーション①: HTTPメソッドは "/user/create"
        //       レギュレーション②: リクエストデータは "name" の１つのみ
        //       レギュレーション③: レスポンスデータは "token" の１つのみ
        
        // トークンを受け取る変数
        // レスポンスで受け取ったトークンはここに設定してください。
        string token = null;
        
        // TODO: ユーザ作成の通信
        //       更新や情報取得など他の通信を参考に、ユーザ作成の通信処理を記載してください。
        var userCreateRequestDto = new UserCreateRequestDto()
        {
            name = "test"
        };
        yield return webRequest.Post<UserCreateRequestDto, UserCreateResponseDto>(GetMethod(MethodType.PostUserCreate), userCreateRequestDto, userCreateResponseDto =>
        {
            Debug.Log("ユーザー作成完了");
            token = userCreateResponseDto.token;
        }, Debug.LogError);
        
        // トークンの設定
        webRequest.SetToken(token);
    }

    /// <summary>
    /// ユーザ情報取得
    /// </summary>
    /// <param name="webRequest">Webリクエスト</param>
    /// <returns></returns>
    private IEnumerator GetUserInfo(WebRequest webRequest)
    {
        // ユーザー情報取得リクエストを投げる
        // 成功時: name,id,coin,highScoreの情報を出力する
        // 失敗時: エラーの内容をDebug.LogErrorで出力する
        yield return webRequest.Get<UserGetResponseDto>(GetMethod(MethodType.GetUser), userGetResponseDto =>
        {
            Debug.Log("id:" + userGetResponseDto.id);
            Debug.Log("name:" + userGetResponseDto.name);
        }, Debug.LogError);
    }

    /// <summary>
    /// モデル作成
    /// </summary>
    /// <param name="webRequest">Webリクエスト</param>
    /// <returns></returns>
    private IEnumerator CreateModel(WebRequest webRequest)
    {
        var createLifeModelRequestDto = new CreateLifeModelRequestDto()
        {
            user_id = "1205a65f-7898-4686-be75-edef2a430ead",
            name = "",
            map = new int [3] //
        };
        yield return webRequest.Post<CreateLifeModelRequestDto,NoneResponseDto>(GetMethod(MethodType.PostModelCreate), createLifeModelRequestDto, noneResponseDto =>
        {
            Debug.Log("モデル作成完了");
        }, Debug.LogError);

    }

    /// <summary>
    /// モデル取得
    /// </summary>
    /// <param name="webRequest">Webリクエスト</param>
    /// <returns></returns>
    private IEnumerator GetModel(WebRequest webRequest)
    {
        var getLifeModelRequestDto = new GetLifeModelRequestDto()
        {
            id = "1205a65f-7898-4686-be75-edef2a430ead"
        };

        yield return webRequest.Get<GetLifeModelResponseDto>(GetMethod(MethodType.GetModel),getLifeModelResponseDto =>
        {
            Debug.Log("life_model"+getLifeModelResponseDto.life_model);

        }, Debug.LogError);


    }

    /// <summary>
    /// モデルランキング
    /// </summary>
    /// <param name="webRequest">Webリクエスト</param>
    /// <returns></returns>
     private IEnumerator RankingModel(WebRequest webRequest)
    {
        yield return webRequest.Get<RankingLifeModelResponseDto>(GetMethod(MethodType.GetRanking),rankingLifeModelResponseDto =>
        {
            Debug.Log("life_model_list"+rankingLifeModelResponseDto.life_model_list);

        }, Debug.LogError);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="webRequest">Webリクエスト</param>
    /// <returns></returns>
     private IEnumerator UserModel(WebRequest webRequest)
    {
        yield return webRequest.Get<GetLifeModelResponseDto>(GetMethod(MethodType.GetRanking),getLifeModelResponseDto =>
        {
            Debug.Log("life_model_list"+getLifeModelResponseDto.life_model);

        }, Debug.LogError);
    }

    /// <summary>
    /// メソッドの取得
    /// </summary>
    /// <param name="type">メソッドタイプ</param>
    /// <returns></returns>
    private string GetMethod(MethodType type)
    {
        switch (type)
        {
            case MethodType.PostUserCreate:
                return "/user/create";

            case MethodType.GetUser:
                return "/user/get"; 

            case MethodType.PostModelCreate:
                return "/model/create";
            
            case MethodType.GetModel:
                return "/model/get";
            
//            case MethodType.PostModelFavorite:
//                return "/model/favorite";
            
            case MethodType.GetRanking:
                return "/model/ranking";

            case MethodType.GetModelUser:
                return "/model/user";
        }

        return null;
    }
}
