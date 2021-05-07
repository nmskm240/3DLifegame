using System;

namespace Network.RequestDto
{
    [Serializable]
    public class CreateLifeModelRequestDto : DtoBase
    {
        // モデルid
        public string user_id;

        public string name;

        public int[] map;
    }
}