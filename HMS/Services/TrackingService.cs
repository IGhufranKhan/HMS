using HMS.Abstractions;

namespace HMS.Services
{
    public class TrackingService : ITrackingService
    {
        private static int _visitCount = 0;

        public void IncrementVisitCount()
        {
            
            _visitCount++;
        }

        public int GetTotalVisits()
        {
            
            return _visitCount;
        }
    }
}
