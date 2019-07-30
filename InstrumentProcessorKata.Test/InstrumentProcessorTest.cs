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
    }
}
