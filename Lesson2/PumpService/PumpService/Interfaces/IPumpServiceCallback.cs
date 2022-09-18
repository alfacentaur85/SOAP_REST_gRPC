using System.ServiceModel;

namespace PumpService
{
    /// <summary>
    /// Интерфейс IPumpServiceCallback
    /// </summary>
    [ServiceContract]
    public interface IPumpServiceCallback
    {
        [OperationContract]
        void UpdateStatistics(StatisticsService statistics);
    }
}
