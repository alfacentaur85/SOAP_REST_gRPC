namespace PumpService
{
    /// <summary>
    /// Интерфейс IScriptService
    /// </summary>
    public interface IScriptService
    {
        bool Compile();
        void Run(int count);
    }
}
