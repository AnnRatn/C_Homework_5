using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace C_Homework_5
{
    public class MyLinkedList<T> : ICollection<T>, IEnumerable<T>
    {
        //Создаем класс, в котором храним элемент и ссылки на последующий и предыдущий объект
        class Object
        {
            public Object previous;
            public T current;
            public Object next;
        }

        public event EventHandler<T> Added;
        public event EventHandler<T> Removed;
        public event EventHandler Cleared;


        private int count;
        private Object first, last;
        private bool isteadonly;
        private List<T> objects = new List<T>();

        int ICollection<T>.Count
        {
            get { return count; }
        }

        T First
        {
            get { return first.current; }
        }

        T Last
        {
            get { return last.current; }
        }

        bool ICollection<T>.IsReadOnly
        {
            get { return isteadonly; }
        }

        //Добавление элемента
        public void Add(T item)
        {
            Object added = new Object();
            added.current = item;
            if (count == 0)
            {
                first = added;
                last = added;
                first.next = last;
                last.previous = first;
                count++;
            }
            else
            {
                added.previous = last;
                last.next = added;
                last = added;
                count++;
            }
            Added.Invoke(this, item);
        }

        //Отчищение списка
        public void Clear()
        {
            first = null;
            last = null;
            count = 0;
            Cleared.Invoke(this, EventArgs.Empty);
        }

        //Проверка на нахождение элемента в списке
        public bool Contains(T item)
        {
            Object node = new Object();
            node = first;
            for (int i = 0; i < count; i++)
            {
                if (node.current.Equals(item)) return true;
                node = node.next;
            }
            return false;
        }

        //Копирование части элементов в массив
        public void CopyTo(T[] array, int arrayIndex)
        {
            Object node = new Object();
            int num = 0;
            node = first;
            for (int i = 0; i < count; i++)
            {
                if (i < arrayIndex) node = node.next;
                else
                {
                    try {
                        array[num] = node.current;
                        node = node.next;
                        num++;
                    }
                    catch (IndexOutOfRangeException e)
                    {
                        Console.WriteLine(e.Message);
                    }
                }
            }
        }

        //Перечисление
        public IEnumerator GetEnumerator()
        {
            Object node = new Object();
            node = first;
            for (int i = 0; i < count; i++)
            {
                yield return node.current;
                node = node.next;
            }
        }

        IEnumerator<T> IEnumerable<T>.GetEnumerator()
        {
            Object node = new Object();
            node = first;
            for (int i = 0; i < count; i++)
            {
                yield return node.current;
                node = node.next;
            }
        }

        //Удаление элемента
        public bool Remove(T item)
        {
            Object node = new Object();
            node = first;
            for (int i = 0; i < count; i++)
            {
                if (node.current.Equals(item))
                {
                    if (i == 0)
                    {
                        if (count == 1)
                        {
                            first = null;
                            last = null;
                        }
                        else
                        {
                            first = node.next;
                            node.next.previous = null;
                        }
                    }
                    else
                    {
                        if (i == count - 1)
                        {
                            last = node.previous;
                            node.previous.next = null;
                        }
                        else
                        {
                            node.previous.next = node.next.previous;
                            node.next.previous = node.previous.next;
                        }
                    }
                    count--;
                    Removed?.Invoke(this, item);
                    return true;
                }
                node = node.next;
            }
            return false;
        }

        //Вставка элемента перед элементом листа
        public bool Insert(T newitem, T item)
        {
            Object newnode = new Object();
            newnode.current = newitem;
            Object node = new Object();
            node = first;
            for (int i = 0; i < count; i++)
            {
                if (node.current.Equals(item))
                {
                    if (i == 0)
                    {
                        first = newnode;
                        newnode.next = node;
                        node.previous = newnode;
                    }
                    else
                    {
                        node.previous.next = newnode;
                        newnode.previous = node.previous;
                        node.previous = newnode;
                        newnode.next = node;
                    }
                    count++;
                    return true;
                }
                node = node.next;
            }
            return false;
        }
    }
    class Programm
    {
        static void Main(string[] args)
        {
            MyLinkedList<int> list;
            list = new MyLinkedList<int>();
            EventHandler<int> eventHandlerA = (sender, item) => Console.WriteLine("New item is added");
            EventHandler<int> eventHandlerR = (sender, item) => Console.WriteLine("Item is removed");
            EventHandler eventHandlerC = (sender, item) => Console.WriteLine("List was cleared");
            list.Added += eventHandlerA;
            list.Removed += eventHandlerR;
            list.Cleared += eventHandlerC;
            list.Add(3);
            list.Add(5);
            list.Add(6);
            Console.WriteLine(list.Count().ToString());
            //list.Remove(6);
            //list.Remove(3);
            //list.Remove(5);
            Console.WriteLine(list.Count().ToString());
            Console.WriteLine(list.Contains(6).ToString());

            int[] array = new int[2];
            list.CopyTo(array, 1);
            foreach(int i in array)
            {
                Console.WriteLine(i.ToString());
            }
            list.Insert(2, 5);
            list.Insert(1, 3);
            array = new int[3];
            list.CopyTo(array, 0);
            foreach (int i in array)
            {
                Console.WriteLine(i.ToString());
            }
            Console.ReadKey();
        }
    }
    }