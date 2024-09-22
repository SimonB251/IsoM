using IsoMetrix.BL.LinkedList;
using System.Xml.Linq;

namespace IsoMetrix.Tests.LinkedList
{
    [TestClass]
    public class LinkedListInsertTests
    {
        [TestMethod]
        public void Insert_ToBlankLinkedList()
        {
            //Arrange
            var customLinkedList = new CustomLinkedList<string>();
            var newLinkedNode    = new LinkedNode<string>("Value 1");

            //Act
            customLinkedList.Insert(newLinkedNode, 0);

            //Assert
            Assert.AreEqual("Value 1", customLinkedList.TryGetNode(0)?.NodeValue);
        }

        [TestMethod]
        public void Insert_ToLinkedListWithOneItemAtBeginningOfList()
        {
            //Arrange
            var newLinkedNode    = new LinkedNode<string>("Value 1");
            var customLinkedList = new CustomLinkedList<string>([
                new("Value 2")
            ]);

            //Act
            customLinkedList.Insert(newLinkedNode, 0);

            //Assert
            Assert.AreEqual("Value 1", customLinkedList.TryGetNode(0)?.NodeValue);
            Assert.AreEqual("Value 2", customLinkedList.TryGetNode(1)?.NodeValue);
        }

        [TestMethod]
        public void Insert_ToLinkedListWithOneItemAtEndOfList()
        {
            //Arrange
            var newLinkedNode    = new LinkedNode<string>("Value 2");
            var customLinkedList = new CustomLinkedList<string>([
                new("Value 1")
            ]);

            //Act
            customLinkedList.Insert(newLinkedNode, 1);

            //Assert
            Assert.AreEqual("Value 1", customLinkedList.TryGetNode(0)?.NodeValue);
            Assert.AreEqual("Value 2", customLinkedList.TryGetNode(1)?.NodeValue);
        }

        [TestMethod]
        public void Insert_ToLinkedListWithMultipleItems()
        {
            //Arrange
            var newLinkedNode    = new LinkedNode<string>("Value 3");
            var customLinkedList = new CustomLinkedList<string>(
                new("Value 1"),
                new("Value 2"),
                new("Value 4")
            );

            //Act
            customLinkedList.Insert(newLinkedNode, 2);

            //Assert
            Assert.AreEqual("Value 1", customLinkedList.TryGetNode(0)?.NodeValue);
            Assert.AreEqual("Value 2", customLinkedList.TryGetNode(1)?.NodeValue);
            Assert.AreEqual("Value 3", customLinkedList.TryGetNode(2)?.NodeValue);
            Assert.AreEqual("Value 4", customLinkedList.TryGetNode(3)?.NodeValue);
        }

        [TestMethod]
        public void Insert_AtInvalidPosition()
        {
            //Arrange
            var newLinkedNode    = new LinkedNode<string>("Value 1");
            var customLinkedList = new CustomLinkedList<string>([
                new("Value 2")
            ]);

            //Act
            var ex = Assert.ThrowsException<ArgumentOutOfRangeException>(
                () => customLinkedList.Insert(newLinkedNode, 3)
            );

            //Assert
            Assert.AreEqual($"Specified argument was out of the range of valid values. (Parameter 'position')", ex.Message);
        }
    }
}