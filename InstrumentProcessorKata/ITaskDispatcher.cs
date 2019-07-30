namespace InstrumentProcessorKata
{
    public interface ITaskDispatcher
    {
        string GetTask();
        void FinishedTask(string task);
    }
}