using IsoMetrix.BL.LinkedList;

namespace IsoMetrix.Tests.LinkedList
{
    [TestClass]
    public class LinkedListDeleteTests
    {
        [TestMethod]
        public void Delete_LinkedNodeAtTheStart()
        {
            var customLinkedList = new CustomLinkedList<string>(
                new("Value 1"),
                new("Value 2")
            );

            customLinkedList.Delete(0);

            Assert.AreEqual("Value 2", customLinkedList.TryGetNode(0)?.NodeValue);
        }

        [TestMethod]
        public void Delete_LinkedNodeAtTheEnd()
        {
            var customLinkedList = new CustomLinkedList<string>(
                new("Value 1"),
                new("Value 2")
            );

            customLinkedList.Delete(1);

            Assert.AreEqual("Value 1", customLinkedList.TryGetNode(0)?.NodeValue);
            Assert.IsNull(customLinkedList.TryGetNode(1));
        }

        [TestMethod]
        public void Delete_ToLinkedListWithMultipleItems()
        {
            var customLinkedList = new CustomLinkedList<string>(
                new("Value 1"),
                new("Value 2"),
                new("Value 3")
            );

            customLinkedList.Delete(1);

            Assert.AreEqual("Value 1", customLinkedList.TryGetNode(0)?.NodeValue);
            Assert.AreEqual("Value 3", customLinkedList.TryGetNode(1)?.NodeValue);
        }

        [TestMethod]
        public void Delete_AtInvalidPosition()
        {
            //Arrange
            var customLinkedList = new CustomLinkedList<string>([
                new("Value 1")
            ]);

            //Act
            var ex = Assert.ThrowsException<ArgumentOutOfRangeException>(
                () => customLinkedList.Delete(3)
            );

            //Assert
            Assert.AreEqual($"Specified argument was out of the range of valid values. (Parameter 'position')", ex.Message);
        }
    }
}