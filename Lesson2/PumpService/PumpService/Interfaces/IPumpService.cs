using System.ServiceModel;

namespace PumpService
{
    /// <summary>
    /// Интерфейс IPumpnService
    /// </summary>
    [ServiceContract(Namespace = "http://Microsoft.ServiceModel.Samples", SessionMode = SessionMode.Required, CallbackContract = typeof(IPumpServiceCallback))]
    public interface IPumpService
    {
        [OperationContract]
        void RunScript();

        [OperationContract]
        void UpdateAndCompileScript(string fileName);
    }
}
