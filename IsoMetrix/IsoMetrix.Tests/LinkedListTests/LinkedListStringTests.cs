using IsoMetrix.BL.LinkedList;

namespace IsoMetrix.Tests.LinkedList
{
    [TestClass]
    public class LinkedListStringTests
    {
        [TestMethod]
        public void PrintList_EmptyList()
        {
            var list = new CustomLinkedList<string>();

            var result = list.PrintList();

            Assert.AreEqual("", result.Trim());
        }

        [TestMethod]
        public void PrintList_NonEmptyList()
        {
            var customLinkedList = new CustomLinkedList<string>(
                new("Value 1"),
                new("Value 2"),
                new("Value 3")
            );

            var result = customLinkedList.PrintList();

            Assert.AreEqual("Value 1\nValue 2\nValue 3", result.Trim());
        }
    }
}