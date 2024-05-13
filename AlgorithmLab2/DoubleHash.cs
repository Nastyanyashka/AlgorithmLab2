using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgorithmLab2
{
    class doubleHash
    {

        int TABLE_SIZE, keysPresent, PRIME, MAX_SIZE = 10000001;
        HashMap hashTable;
        bool[] isPrime;

        void __setSieve()
        {
            isPrime[0] = isPrime[1] = true;
            for (long i = 2; i * i <= MAX_SIZE; i++)
            {
                if (isPrime[i] == false)
                {
                    for (long j = i * i; j <= MAX_SIZE;
                         j += i)
                    {
                        isPrime[j] = true;
                    }
                }
            }
        }

        int hash1(int key) { return key % TABLE_SIZE; }

        int hash2(int key) { return PRIME - (key % PRIME); }

        bool isFull() { return (TABLE_SIZE == keysPresent); }

        public doubleHash(int n)
        {
            isPrime = new bool[MAX_SIZE + 1];
            __setSieve();
            TABLE_SIZE = n;

            PRIME = TABLE_SIZE - 1;
            while (isPrime[PRIME] == true)
                PRIME--;

            keysPresent = 0;
            hashTable = new HashMap(TABLE_SIZE);
        }

        public void __printPrime(long n)
        {
            for (long i = 0; i <= n; i++)
                if (isPrime[i] == false)
                    Console.Write(i + ", ");
            Console.WriteLine();
        }

        public void insert(int key, int value)
        {
            HashNode newNode = new HashNode(key, value);
            if (key == -1)
            {
                Console.Write(
                    "ERROR : -1 can't be inserted in the table\n");
            }

            if (isFull())
            {
                Console.Write("Ошибка: Хэш таблица заполнена\n");
                return;
            }

            int probe = hash1(key),
                offset
                = hash2(key);
            if (probe < 0)
                Console.Write("Error: probe ");
            else
            {
                while (hashTable.table[probe] != null)
                {
                    if (hashTable.table[probe].key == -1)
                        break;
                    probe = (probe + offset) % TABLE_SIZE;
                }

                newNode.next = hashTable.table[probe];
                hashTable.table[probe] = newNode;


                keysPresent += 1;
            }
        }

        public void erase(int key)
        {
            if (search(key) == -1)
                return;

            int probe = hash1(key), offset = hash2(key);

            while (hashTable.table[probe].key != -1)
                if (hashTable.table[probe].key == key)
                {
                    hashTable.table[probe].key = -1;
                    keysPresent--;
                    return;
                }
                else
                    probe = (probe + offset) % TABLE_SIZE;
        }

        public int search(int key)
        {
            int probe = hash1(key), offset = hash2(key),
                initialPos = probe;
            bool firstItr = true;

            while (true)
            {
                if (hashTable.table[probe] == null)
                    break;
                else if (hashTable.table[probe].key == key)
                    return probe;
                else if (probe == initialPos && !firstItr)
                    return -1;
                else
                    probe = ((probe + offset) % TABLE_SIZE);
                firstItr = false;
            }
            return -1;
        }

        public void print(int key)
        {
            int i = search(key);
            if (hashTable.table[i] != null)
            {
                Console.Write("По заданному ключу " + i + " обнаружено следующее значение ");
                if (hashTable.table[i].key != -1)
                    Console.Write(hashTable.table[i].key + ":" + hashTable.table[i].value + ", ");
                Console.Write("\n");
            }
            else
                Console.Write($"По заданному ключу {key} значений не обнаружено ");
        }

        public void print()
        {
            for (int i = 0; i < TABLE_SIZE; i++)
                if (hashTable.table[i] != null)
                    if (hashTable.table[i].key != -1)
                        Console.Write(hashTable.table[i].key + ":" + hashTable.table[i].value + ", ");
            Console.Write("\n");
        }
    }
}
