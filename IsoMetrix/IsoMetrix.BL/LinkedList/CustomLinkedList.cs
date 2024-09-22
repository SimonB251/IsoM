using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IsoMetrix.BL.LinkedList
{
    //NOTE: The task was to create a LinkedList without built in classes, but even 'object' is a built in class.
    // My assumption was the requirement means to not use a built in LinkedList class.
    // I also assumed a zero based index and that a forward-only linked list was acceptable, as it wasn't explicitly mentioned

    public class CustomLinkedList<T>
    {
        public LinkedNode<T>? StartNode { get; set; }

        public CustomLinkedList(params LinkedNode<T>[] linkedNodes)
        {
            if (linkedNodes.Length <= 0) return;

            var currentNode = StartNode = linkedNodes[0];
            for (int i = 1; i < linkedNodes.Length; i++)
            {
                currentNode.NextNode = linkedNodes[i];
                currentNode = linkedNodes[i];
            }
        }

        public void Insert(LinkedNode<T> newNode, int position)
        {
            if (position == 0)
            {
                newNode.NextNode = StartNode;
                StartNode = newNode;
                return;
            }

            if (TryGetNode(position - 1) is LinkedNode<T> nodeAtPosition)
            {
                // I.E. Inserting position 2, in LL [0][1][2][3]
                // Node 1's next node is now new node, old node 2 is now new nodes next node.
                newNode.NextNode = nodeAtPosition.NextNode;
                nodeAtPosition.NextNode = newNode;
            }
            else
            {
                throw new ArgumentOutOfRangeException(nameof(position));
            }
        }

        public void Delete(int position)
        {
            if (StartNode == null)
                return;

            if (position == 0)
            {
                StartNode = StartNode.NextNode;
                return;
            }

            if (TryGetNode(position - 1) is LinkedNode<T> nodeAtPosition)
            {
                // I.E. Removing position 2, in LL [0][1][2][3]
                // Get node 1, next node is node 2's next node (3)
                nodeAtPosition.NextNode = nodeAtPosition.NextNode?.NextNode;
            }
            else
            {
                throw new ArgumentOutOfRangeException(nameof(position));
            }
        }

        public LinkedNode<T>? TryGetNode(int position)
        {
            if (StartNode == null || position == 0)
                return StartNode;

            var currentNode = StartNode;
            for (int i = 0; i < position; i++)
            {
                currentNode = currentNode?.NextNode;
            }

            return currentNode;
        }

        public string PrintList()
        {
            StringBuilder sb = new StringBuilder();

            var currentNode = StartNode;
            while (currentNode != null)
            {
                sb.Append($"{currentNode}\n");
                currentNode = currentNode.NextNode;
            }

            return sb.ToString();
        }
    }

    public class LinkedNode<T>
    {
        public LinkedNode(T value)
        {
            NodeValue = value;
        }

        public LinkedNode<T>? NextNode { get; set; }
        public T? NodeValue { get; set; }

        public override string ToString()
        {
            return $"{NodeValue}";
        }
    }
}