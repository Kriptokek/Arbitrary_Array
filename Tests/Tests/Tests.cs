using System;
using Arbitrary_Array.Library;
using NUnit.Framework;

namespace Tests
{
    [TestFixture]
    public class Tests
    {
        [Test]
        [TestCase(3, 2)]
        public void CreatingArrayWithWrongArgs_ThrowsException(int a, int b)
        {
            Assert.Throws<ArgumentException>(() =>
            {
                var array = new ArbitraryArray<int>(a, b);
            });
        }

        [Test]
        public void Add_AddingNull_ExceptionThrown()
        {
            var array = new ArbitraryArray<string>(0, 4);

            Assert.Throws<ArgumentNullException>(() =>
            {
                array.Add(null);
            });
        }

        [Test]
        public void Remove_ItemContained_ReturnsTrue()
        {
            var array = new ArbitraryArray<int>(0, 4);
            array.Add(1);

            var result = array.Remove(1);

            Assert.IsTrue(result);
        }
        
        [Test]
        public void Remove_ItemIsNotContained_ReturnsFalse()
        {
            var array = new ArbitraryArray<int>(0, 4);
            array.Add(1);

            var result = array.Remove(2);

            Assert.IsFalse(result);
        }

        [Test]
        public void IndexerIsOurOfRange_ExceptionThrown()
        {
            var array = new ArbitraryArray<int>(0, 3);

            Assert.Throws<IndexOutOfRangeException>(() =>
            {
                Console.WriteLine(array[int.MaxValue]);
            });
        }
        
        [Test]
        public void ChangingItems_ArrayIsNotReadOnly_ItemChanged()
        {
            var array = new ArbitraryArray<int>(0, 3);
            var result = 1;
            
            array.Add(1);
            array[0] = result;
            
            Assert.AreEqual(result, array[0]);
        }
    }
}