using System.Collections;
using System.Collections.Generic;

namespace InstrumentProcessorKata
{
    public class InstrumentProcessor
    {
        private readonly IInstrument _instrumentObject;
        private readonly ITaskDispatcher _taskDispatcherObject;
        private readonly IConsole _consoleObject;
        private Queue<string> _taskQueue = new Queue<string>();

        public InstrumentProcessor(IInstrument instrumentObject, ITaskDispatcher taskDispatcherObject, IConsole consoleObject)
        {
            _instrumentObject = instrumentObject;
            _taskDispatcherObject = taskDispatcherObject;
            _consoleObject = consoleObject;

            _instrumentObject.Finished += (sender, args) =>
            {
                _taskDispatcherObject.FinishedTask(_taskQueue.Dequeue());
            };

            _instrumentObject.Error += (sender, args) => { _consoleObject.WriteLine("Error occured"); };
        }

        public void Process()
        {
            string task = _taskDispatcherObject.GetTask();
            _taskQueue.Enqueue(task);
            _instrumentObject.Execute(task);
        }
    }
}