using PumpClient.PumpServiceReference;
using System;
using System.ServiceModel;

namespace PumpClient
{
    public class Program
    {
        static void Main(string[] args)
        {
            var instanceContext = new InstanceContext(new CallbackHandler());

            var client = new PumpServiceClient(instanceContext);

            client.UpdateAndCompileScript(@"D:\Repos\C#_learning\SOAP_REST_gRPC\Lesson2\PumpService\PumpService\Scripts\Sample.script");
            client.RunScript();

            Console.WriteLine("Please, Enter to exit ...");
            Console.ReadKey(true);

            client.Close();

        }
    }
}
