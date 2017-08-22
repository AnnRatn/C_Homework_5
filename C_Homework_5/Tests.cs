using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace C_Homework_5
{
    [TestFixture]
    class LinkedListTest
    {
        MyLinkedList<int> list;

        [SetUp]
        public void NewList()
        {
            list = new MyLinkedList<int>();
            EventHandler<int> eventHandlerA = (sender, item) => Console.WriteLine("New item is sdded");
            EventHandler<int> eventHandlerR = (sender, item) => Console.WriteLine("New item is sdded");
            EventHandler eventHandlerC = (sender, item) => Console.WriteLine("List was cleared");
            list.Added += eventHandlerA;
            list.Removed += eventHandlerR;
            list.Cleared += eventHandlerC;
        }

        [Test]
        public void AddingTest()
        {
            list.Add(3);
            list.Add(5);
            Assert.AreEqual(list.Count(), 2);
        }

        [Test]
        public void RemovedTest()
        {
            list.Add(3);
            list.Add(5);
            list.Remove(3);
            Assert.AreEqual(list.Count(), 1);
        }

        [Test]
        public void ClearTest()
        {
            list.Add(3);
            list.Add(5);
            list.Clear();
            Assert.AreEqual(list.Count(), 0);
        }

        [Test]
        public void ContainsTest()
        {
            list.Add(3);
            list.Add(5);
            Assert.AreEqual(list.Contains(5), true);
        }

        [Test]
        public void CopyTest()
        {
            list.Add(3);
            list.Add(5);
            list.Add(6);
            int[] array = new int[2];
            list.CopyTo(array, 1);
            Assert.AreEqual(array[0], 5);
            Assert.AreEqual(array[1], 6);
        }

        [Test]
        public void GetIEnumerableTest()
        {
            list.Add(3);
            list.Add(5);
            list.Add(6);
            foreach (int i in list)
            {
                Console.WriteLine(i);
            }
        }

        [Test]
        public void InsertTest()
        {
            list.Add(3);
            list.Add(5);
            list.Add(6);
            list.Insert(2, 5);
            list.Insert(1, 3);
            int[] array = new int[5];
            list.CopyTo(array, 0);
            Assert.AreEqual(array[0], 1);
            Assert.AreEqual(array[2], 2);
        }
    }
}
