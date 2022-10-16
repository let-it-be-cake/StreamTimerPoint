using STP.DataLayer.Models;

namespace STP.DataLayer.Interfaces
{
    public interface ITimeStampsSaver
    {
        public List<TimeStamp> TimeStamps { get; }

        public void Save(string pathToSave);
    }
}
