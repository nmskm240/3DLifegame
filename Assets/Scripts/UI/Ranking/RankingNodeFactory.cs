using UnityEngine;

namespace UI.Ranking
{
    public class RankingNodeFactory : Object, IFactory<GameObject>
    {
        public GameObject Create()
        {
            return Instantiate(Resources.Load("Prefabs/RankingNode") as GameObject);
        }
    }
}