/// <summary>
/// 연결 리스트 클래스
/// </summary>
/// <typeparam name="T"></typeparam>
namespace DataStructure
{
    public class LinkedNode<T>
    {
        public T? Data = default(T);
        public LinkedNode<T>? Next;
        public LinkedNode<T>? Prev;
    }

    public class MyLinkedList<T>
    {
        public LinkedNode<T>? First { get; private set; } //첫부분
        public LinkedNode<T>? Last { get; private set; } //마지막
        public int Count { get; private set; }

        public LinkedNode<T> AddLast(T data)
        {
            LinkedNode<T> newLinkedNode = new LinkedNode<T>();
            newLinkedNode.Data = data;

            // 만약 노드가 존재하지 않았다면, 추가한 노드가 첫 번째 노드가 됨.
            if (First == null)
                First = newLinkedNode;

            // 기존의 마지막 노드와 새로 추가되는 노드를 연결
            if (Last != null)
            {
                Last.Next = newLinkedNode;
                newLinkedNode.Prev = Last;
            }

            // 새로운 노드를 마지막 노드로 만든다.
            Last = newLinkedNode;
            Count++;
            return newLinkedNode;
        }
        public LinkedNode<T> AddFirst(T data)
        {
            LinkedNode<T> newLinkedNode = new LinkedNode<T>();
            newLinkedNode.Data = data;

            // 만약 마지막 노드가 존재하지 않았다면, 추가한 노드가 마지막 노드가 됨.
            if (Last == null)
                Last = newLinkedNode;

            // 기존의 첫 노드와 새로 추가되는 노드를 연결
            if (First != null)
            {
                First.Prev = newLinkedNode;
                newLinkedNode.Next = First;
            }

            // 새로운 노드를 첫 노드로 만든다.
            First = newLinkedNode;
            Count++;
            return newLinkedNode;
        }
        public void Remove(LinkedNode<T> node)
        {
            // 기존의 첫 노드의 다음 노드를 첫 노드로 설정함.
            if (First == node && First.Next != null)
            {
                First.Next.Prev = null;
                First = First.Next;
            }
            else if (First == node && First.Next == null) First = null;

            // 기존의 마지막 노드의 이전 노드를 마지막 노드로 설정함.
            if (Last == node && Last.Prev != null)
            {
                Last.Prev.Next = null;
                Last = Last.Prev;               
            }
            else if (Last == node && Last.Prev == null) Last = null;

            // 만약 이전 노드가 존재 한다면 이전 노드의 다음 노드를 현재 노드의 다음 노드로 설정함.
            if (node.Prev != null && node.Next != null)
                node.Prev.Next = node.Next;

            // 만약 다음 노드가 존재 한다면 다음 노드의 이전 노드를 현재 노드의 이전 노드로 설정함.
            if (node.Next != null && node.Prev != null)
                node.Next.Prev = node.Prev;

            Count--;
        }
    }
}