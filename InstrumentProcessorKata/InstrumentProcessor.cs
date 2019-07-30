namespace InstrumentProcessorKata
{
    public class InstrumentProcessor
    {
        private readonly IInstrument _instrumentObject;
        private readonly ITaskDispatcher _taskDispatcherObject;
        private readonly IConsole _consoleObject;

        public InstrumentProcessor(IInstrument instrumentObject, ITaskDispatcher taskDispatcherObject, IConsole consoleObject)
        {
            _instrumentObject = instrumentObject;
            _taskDispatcherObject = taskDispatcherObject;
            _consoleObject = consoleObject;
        }

        public void Process()
        {
            string task = _taskDispatcherObject.GetTask();
            _instrumentObject.Execute(task);
        }
    }
}