using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgorithmLab2
{
    class HashMap
    {
        public HashNode[] table;
        private int capacity;
        private int size;

        public HashMap(int capacity)
        {
            this.capacity = capacity;
            table = new HashNode[capacity];
            size = 0;

        }
        private int HashCode(int key) { return key % capacity; }
        public void InsertNode(int key, int value)
        {
            int hashIndex = HashCode(key);
            HashNode newNode = new HashNode(key, value);
            if (table[hashIndex] != null)
            {
                HashNode current = table[hashIndex];

                while (current != null)
                {
                    if (current.key == key)
                    {
                        current.value = value;
                        return;
                    }
                    current = current.next;
                }
            }
            newNode.next = table[hashIndex];
            table[hashIndex] = newNode;
            size++;
        }
        public int? DeleteNode(int key)
        {
            int hashIndex = HashCode(key);

            if (table[hashIndex] != null)
            {
                HashNode current = table[hashIndex];
                HashNode previous = null;

                while (current != null)
                {
                    if (current.key == key)
                    {
                        if (previous == null)
                        {
                            table[hashIndex] = current.next;
                        }
                        else
                        {
                            previous.next = current.next;
                        }
                        size--;
                        return current.value;
                    }
                    previous = current;
                    current = current.next;
                }
            }

            return null;
        }
        public int? Get(int key)
        {
            int hashIndex = HashCode(key);

            if (table[hashIndex] != null)
            {
                HashNode current = table[hashIndex];

                while (current != null)
                {
                    if (current.key == key)
                    {
                        return current.value;
                    }
                    current = current.next;
                }
            }

            return 0;
        }
        public int Size() { return size; }
        public bool IsEmpty() { return size == 0; }

        public void Display(int key)
        {
            int? i = Get(key);
            if (table[(int)i!] != null)
            {
                Console.Write("По заданному ключу " + i + " обнаружено следующее значение ");
                Console.Write(table[key].key + ":" + table[key].value);
                Console.Write("\n");
            }
            else
                Console.Write($"По заданному ключу {key} значений не обнаружено ");
        }
        public void Display()
        {
            for (int i = 0; i < capacity; i++)
            {
                if (table[i] != null)
                {
                    HashNode current = table[i];

                    while (current != null)
                    {
                        Console.WriteLine("key = " + current.key
                                          + " value = "
                                          + current.value);
                        current = current.next;
                    }
                }
            }
        }
    }
}