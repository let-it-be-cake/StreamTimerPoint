using AutoMapper;
using Google.Apis.YouTube.v3.Data;
using STP.DataLayer.Models;

namespace STP.DataLayer.Mapper
{
    internal class MapperProfile : Profile
    {
        //Enum.Parse<ConnectionStatus>(o.Status.HealthStatus.Status, ignoreCase: true)
        public MapperProfile()
        {
            CreateMap<LiveStream, StreamInfo>()
                .ForMember(d => d.ConnectionStatus, s => s.MapFrom((o, _)
                    => Enum.TryParse<ConnectionStatus>(o.Status?.HealthStatus?.Status, ignoreCase: true, out var result) ? result : default))
                .ForMember(d => d.Name, s => s.MapFrom(o => o.Snippet.Title))
                .ForMember(d => d.StreamStatus, s => s.MapFrom((o, _)
                    => Enum.TryParse<StreamStatus>(o.Status?.StreamStatus, ignoreCase: true, out var result) ? result : default));
        }
    }
}
