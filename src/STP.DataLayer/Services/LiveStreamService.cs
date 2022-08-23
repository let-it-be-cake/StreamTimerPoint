using AutoMapper;
using Google.Apis.Util;
using Google.Apis.YouTube.v3;
using STP.DataLayer.API;
using STP.DataLayer.Models;

namespace STP.DataLayer.Services
{
    internal class LiveStreamService : YouTubeService
    {
        private readonly Repeatable<string> _statusPart =
            new Repeatable<string>(new[]
            {
                StreamPart.Status.ToString(),
            });
        private readonly Repeatable<string> _streamInfoPart =
            new Repeatable<string>(new[]
            {
                StreamPart.Status.ToString(),
                StreamPart.Snippet.ToString(),
            });

        private readonly IUserCredentional _userCredentional;
        private readonly IMapper _mapper;
        
        public LiveStreamService(IUserCredentional userCredentional, IMapper mapper, string applicationName)
            : base(new Initializer { ApplicationName = applicationName })
        {
            _userCredentional = userCredentional;
            _mapper = mapper;
        }

        public async Task<ConnectionStatus> GetConnectionStatusAsync(string streamId)
        {
            var streamInfo = await ExecuteAsync(_statusPart, streamId);

            return streamInfo.HasValue ?
                streamInfo.Value.ConnectionStatus :
                ConnectionStatus.Error;
        }

        public Task<StreamInfo?> GetStreamInfoAsync(string streamId)
        {
            return ExecuteAsync(_streamInfoPart, streamId);
        }

        private async Task<StreamInfo?> ExecuteAsync(Repeatable<string> part, string streamId)
        {
            var request = LiveStreams.List(part);
            request.Credential = await _userCredentional.GetUserCredentialAsync();
            request.Id = streamId;

            try
            {
                var response = await request.ExecuteAsync();
                return _mapper.Map<StreamInfo>(response.Items.Single());
            }
            catch
            {
                return null;
            }
        }
    }
}
