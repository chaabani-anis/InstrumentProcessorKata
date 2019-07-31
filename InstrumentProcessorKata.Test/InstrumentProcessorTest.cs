using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace InstrumentProcessorKata.Test
{
    [TestClass]
    public class InstrumentProcessorTest
    {
        [TestMethod]
        public void should_object_not_null_when_create_new_instrument_processor()
        {
            var instrument = new Mock<IInstrument>();
            var taskDispatcher = new Mock<ITaskDispatcher>();
            var console = new Mock<IConsole>();
            var instrumentProcessor = new InstrumentProcessor(instrument.Object, taskDispatcher.Object, console.Object);
            Assert.IsNotNull(instrumentProcessor);
        }

        [TestMethod]
        public void should_execute_next_task_when_process_is_called()
        {
            //Arrange
            var instrument = new Mock<IInstrument>();
            var taskDispatcher = new Mock<ITaskDispatcher>();
            var console = new Mock<IConsole>();
            var instrumentProcessor = new InstrumentProcessor(instrument.Object, taskDispatcher.Object, console.Object);

            taskDispatcher.Setup(n => n.GetTask()).Returns("task");
            
            //Act
            instrumentProcessor.Process();

            //Assert
            instrument.Verify(n=>n.Execute("task"), Times.Once());
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void should_Process_throw_exception_when_Instrument_Execute_throw_exception()
        {
            var instrument = new Mock<IInstrument>();
            var taskDispatcher = new Mock<ITaskDispatcher>();
            var console = new Mock<IConsole>();
            var instrumentProcessor = new InstrumentProcessor(instrument.Object, taskDispatcher.Object, console.Object);
            taskDispatcher.Setup(n => n.GetTask()).Returns("task");
            instrument.Setup(n => n.Execute("task")).Throws(new Exception());

            //ACT
            instrumentProcessor.Process();

            //Assert
        }

        [TestMethod]
        public void should_Processor_calls_Dispatcher_FinishedTask_when_Instrument_fires_Finished_event()
        {
            //Arrange
            var instrument = new Mock<IInstrument>();
            var taskDispatcher = new Mock<ITaskDispatcher>();
            var console = new Mock<IConsole>();
            var instrumentProcessor = new InstrumentProcessor(instrument.Object, taskDispatcher.Object, console.Object);
            taskDispatcher.Setup(n => n.GetTask()).Returns("task");
            instrument.Setup(n => n.Execute("task")).Raises(n => n.Finished += null, EventArgs.Empty);
            //Act
            instrumentProcessor.Process();

            //Assert
            taskDispatcher.Verify(n=>n.FinishedTask("task"), Times.Once);
        }

        [TestMethod]
        public void should_Processor_writes_error_to_console_when_Instrument_fires_Error_event()
        {
            //Arrange
            var instrument = new Mock<IInstrument>();
            var taskDispatcher = new Mock<ITaskDispatcher>();
            var console = new Mock<IConsole>();
            var instrumentProcessor = new InstrumentProcessor(instrument.Object, taskDispatcher.Object, console.Object);
            taskDispatcher.Setup(n => n.GetTask()).Returns("task");
            instrument.Setup(n => n.Execute("task")).Raises(n => n.Error += null, EventArgs.Empty);
            //Act
            instrumentProcessor.Process();

            //Assert
            console.Verify(n => n.WriteLine("Error occured"));
        }
    }
}
