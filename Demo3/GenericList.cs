// ***********************************************************************
// 
// 文件名：         GenericList.cs
// 
// 创建日期：       someday
//  
// 功能说明：       简单泛型链接列表类
//                  （大多数情况下，应使用 .NET Framework 类库提供的 List<T> 类，而不是自行创建类。）
//  
// 作者：           高亚斌
// 
// ***********************************************************************

using System.Collections.Generic;

namespace Demo3
{
    public class GenericList<T>
    {
        // The nested class is also generic on T.内部泛型类
        private class Node
        {
            // T used in non-generic constructor.
            public Node(T t)
            {
                next = null;
                data = t;
            }

            private Node next;
            public Node Next
            {
                get { return next; }
                set { next = value; }
            }

            // T as private member data type.
            private T data;

            // T as return type of property.
            public T Data
            {
                get { return data; }
                set { data = value; }
            }
        }

        private Node head;

        // constructor
        public GenericList()
        {
            head = null;
        }

        // T as method parameter type:
        public void AddHead(T t)
        {
            Node n = new Node(t);
            n.Next = head;
            head = n;
        }

        public IEnumerator<T> GetEnumerator()
        {
            Node current = head;

            while (current != null)
            {
                yield return current.Data;
                current = current.Next;
            }
        }
    }
}