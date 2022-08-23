using AutoMapper;
using Google.Apis.YouTube.v3.Data;
using STP.DataLayer.Models;

namespace STP.DataLayer.Mapper
{
    internal class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<LiveStream, StreamInfo>()
                .ForMember(d => d.ConnectionStatus,
                    s => s.MapFrom(o => o.Status.HealthStatus.Status))
                .ForMember(d => d.Name, s => s.MapFrom(o => o.Snippet.Title))
                .ForMember(d => d.StreamStatus, s => s.MapFrom(o => o.Status.StreamStatus));
        }
    }
}
