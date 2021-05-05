using System;

namespace ProjectConnect.Network.ResponseDto
{
    [Serializable]
    public class ModelUsersResponseDto : DtoBase
    {
        public LifeModel[] life_model;
    }
}
