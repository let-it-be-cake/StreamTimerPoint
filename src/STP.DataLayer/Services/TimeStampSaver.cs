using STP.DataLayer.Interfaces;
using STP.DataLayer.Models;
using System.Text;

namespace STP.DataLayer.Services
{
    internal class TimeStampSaver : ITimeStampsSaver
    {
        public readonly string _pattern;

        public List<TimeStamp> TimeStamps { get; } = new ();

        public TimeStampSaver(string pattern)
        {
            _pattern = pattern;
        }

        public void Save(string pathToSave)
        {
            using var stream = new StreamWriter(pathToSave);
            stream.Write(ToString());
        }

        public override string ToString()
        {
            var sb = new StringBuilder();

            foreach (var timeStamp in TimeStamps)
            {
                sb.Append(string.Format(_pattern, timeStamp.LiveTimeStamp, timeStamp.Name));
            }

            return sb.ToString();
        }
    }
}
