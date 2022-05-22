using System;
using System.Collections;
using System.Collections.Generic;

namespace Arbitrary_Array.Library
{
    public class ArbitraryArray<T> : ICollection<T>
    {
        public int  Count => _elements.Length;
        public bool IsReadOnly
        {
            get => false;
            set => throw new NotImplementedException();
        }

        public event Action<T> ItemAddedSuccessfully;
        public event Action<T> ItemRemovedSuccessfully;
        
        private T[] _elements;
        
        private readonly int _lowIndex;

        public ArbitraryArray(int lowIndex, int highIndex)
        {
            if (highIndex <= lowIndex)
                throw new ArgumentException("High index is smaller than low.");
            
            _elements = new T[highIndex - lowIndex];
            
            _lowIndex = lowIndex;
        }
        
        /*
         * Public.
         */
        
        public void Add(T item)
        {
            if (item == null)
                throw new ArgumentNullException();
            
            var newArray = new T[_elements.Length + 1];
            _elements.CopyTo(newArray, 0);
            
            newArray[newArray.Length - 1] = item;
            _elements = newArray;
            
            ItemAddedSuccessfully?.Invoke(item);
        }

        public bool Remove(T item)
        {
            if (!Contains(item)) return false;
            
            RemoveAtIndex(GetIndexOfItem(item));
            
            ItemRemovedSuccessfully?.Invoke(item);
            
            return true;
        }

        public void Clear()
        {
            _elements = Array.Empty<T>();
        }

        public bool Contains(T item)
        {
            foreach (var element in _elements)
            {
                if (element.Equals(item))
                {
                    return true;
                }
            }

            return false;
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            _elements.CopyTo(array, arrayIndex);
        }
        
        public IEnumerator<T> GetEnumerator()
        {
            return ((IEnumerable<T>)_elements).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return (this as IEnumerable<T>).GetEnumerator();
        }

        /*
         * Private.
         */

        private int GetIndexOfItem(T item)
        {
            for (var i = 0; i < Count; i++)
            {
                if (_elements[i]!.Equals(item))
                    return i;
            }

            return -1;
        }
        
        private void RemoveAtIndex(int index)
        {
            if (index <= 0 || index >= Count) 
                return;
            
            var newArray = new T[Count - 1];

            for (var i = 0; i < index; i++)
                newArray[i] = _elements[i];
            
            for (var i = index; i < Count - 1; i++)
                newArray[i] = _elements[i + 1];

            _elements = newArray;
        }
        
        /*
         * Indexer.
         */
        
        public T this[int index]
        {
            get
            {
                try
                {
                    return _elements[index - _lowIndex];
                }
                catch
                {
                    throw new IndexOutOfRangeException();
                }
            }
            set
            {
                if (IsReadOnly == false)
                {
                    try
                    {
                        _elements[index - _lowIndex] = value;
                    }
                    catch
                    {
                        throw new IndexOutOfRangeException();
                    }
                }

                else
                    throw new InvalidOperationException("Array is readonly.");
            }
        }
    }
}
