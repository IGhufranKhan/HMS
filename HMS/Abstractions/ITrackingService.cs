namespace HMS.Abstractions
{
    public interface ITrackingService
    {
        void IncrementVisitCount();
        int GetTotalVisits();
    }
}
