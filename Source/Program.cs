using System;
using Arbitrary_Array.Library;

namespace Arbitrary_Array.Source
{
    internal static class Program
    {
        private static void Main()
        {
            const int lowIndex = -10;
            const int highIndex = 0;

            var arbitraryArray = new ArbitraryArray<int>(lowIndex, highIndex);

            arbitraryArray.ItemAddedSuccessfully += OnItemAdded;
            arbitraryArray.ItemRemovedSuccessfully += OnItemRemoved;

            for (var i = lowIndex; i < highIndex; i++)
                arbitraryArray.Add(i);

            Console.WriteLine("\n ARRAY: ");
            foreach (var item in arbitraryArray)
                Console.Write(item + " ");

            arbitraryArray.Remove(-5);

            Console.WriteLine("\n ARRAY: ");
            foreach (var item in arbitraryArray)
                Console.Write(item + " ");

            Console.WriteLine("\n Number at -5 index is " + arbitraryArray[-5]);
            
            arbitraryArray.ItemAddedSuccessfully -= OnItemAdded;
            arbitraryArray.ItemRemovedSuccessfully -= OnItemRemoved;
        }
        
        /*
         * Events handlers
         */

        private static void OnItemAdded<T>(T item)
        {
            Console.WriteLine($"\n Item({item}) was added");
        }
        
        private static void OnItemRemoved<T>(T item)
        {
            Console.WriteLine($"\n Item({item}) was removed");
        }
    }
}