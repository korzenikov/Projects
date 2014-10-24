using System;

using CourseraTasks.CSharp;

using FluentAssertions;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CourseraTasks.Test
{
    [TestClass]
    public class CyclicQueueTest
    {
        [TestMethod]
        public void QueueOperationsTest()
        {
            var queue = new CyclicQueue(4);
            queue.Enqueue(0);
            queue.Enqueue(1);
            queue.Enqueue(2);
            queue.Enqueue(3);

            Action enqueueAction = () => queue.Enqueue(4);
            enqueueAction.ShouldThrow<InvalidOperationException>();

            queue.Dequeue().Should().Be(0);
            queue.Dequeue().Should().Be(1);
            queue.Dequeue().Should().Be(2);
            queue.Enqueue(4);
            queue.Dequeue().Should().Be(3);
            queue.Dequeue().Should().Be(4);

            Action dequeueAction = () => queue.Dequeue();
            dequeueAction.ShouldThrow<InvalidOperationException>();
        }
    }
}
