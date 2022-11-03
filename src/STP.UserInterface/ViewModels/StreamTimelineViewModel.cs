using Caliburn.Micro;
using Microsoft.Win32;
using STP.DataLayer.API;
using STP.DataLayer.Interfaces;
using STP.DataLayer.Models;
using System;

namespace STP.UserInterface.ViewModels
{
    internal class StreamTimelineViewModel : Screen
    {
        private string? _stampLabel;
        private DateTime? LastStreamBad = null;
        private TimeSpan _timeAdjustment;
        private ConnectionStatus _status;

        private readonly ITimeStampsSaver _timeStampsSaver;

        public string StreamId => StreamInfo!.Value.Id;

        public string KeyName => $"{StreamInfo!.Value.Name}";

        public string Status => _status.ToString();

        public string StampLabel
        {
            get => _stampLabel ?? string.Empty;
            set
            {
                _stampLabel = value;
                NotifyOfPropertyChange(nameof(CanAdd));
            }
        }
        public bool CanAdd => !string.IsNullOrEmpty(StampLabel);

        public StreamInfo? StreamInfo { get; set; }

        public BindableCollection<TimeStamp> TimeStamps { get; set; } = new();

        public StreamTimelineViewModel(ITimeStampsSaver timeStampsSaver)
        {
            _timeStampsSaver = timeStampsSaver;
        }

        public void Add()
        {
            TimeStamps.Add(new TimeStamp
            {
                Name = _stampLabel!,
                Timestamp = StreamInfo!.Value.PublishedAt - DateTime.Now,
                TimeAdjustment = _timeAdjustment,
            });

            StampLabel = string.Empty;
        }

        public void End()
        {
            var saveFileDialog = new SaveFileDialog();

            if (saveFileDialog.ShowDialog() == true)
            {
                _timeStampsSaver.Save(saveFileDialog.FileName);
            }

            throw new NotImplementedException();
        }

        public void StreamChanged(object? sender, StatusEventArgs e)
        {
            _status = e.Status;
            if (e.Status != ConnectionStatus.Error
                && e.Status != ConnectionStatus.BadConnection
                && e.Status != ConnectionStatus.NoData)
            {
                LastStreamBad = LastStreamBad !?? DateTime.Now;
            }
            else
            {
                _timeAdjustment += LastStreamBad.HasValue
                    ? DateTime.Now - LastStreamBad.Value
                    : TimeSpan.Zero;
                LastStreamBad = null;
            }
        }
    }
}
