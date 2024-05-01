using System;
using System;
using System.Collections.Generic;
using System.Linq;
class HashNode
{
    public int key;
    public int value;
    public HashNode next;

    public HashNode(int key, int value)
    {
        this.key = key;
        this.value = value;
        next = null;
    }
}

class HashMap
{
    private HashNode[] table;
    private int capacity;
    private int size;

    public HashMap(int capacity)
    {
        this.capacity = capacity;
        table = new HashNode[capacity];
        size = 0;
    }

    // hash function to find index for a given key
    private int HashCode(int key) { return key % capacity; }

    // function to add key value pair
    public void InsertNode(int key, int value)
    {
        int hashIndex = HashCode(key);
        HashNode newNode = new HashNode(key, value);

        // if the key already exists, update the value
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

        // if the key is new, add a new node to the table
        newNode.next = table[hashIndex];
        table[hashIndex] = newNode;
        size++;
    }

    // function to delete a key value pair
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

    // function to get the value for a given key
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

    // function to get the number of key value pairs in the
    // hashmap
    public int Size() { return size; }

    // function to check if the hashmap is empty
    public bool IsEmpty() { return size == 0; }

    // function to display the key value pairs in the
    // hashmap
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

class Program
{
    static void Main(string[] args)
    {
        HashMap h = new HashMap(20);

        h.InsertNode(1, 1);
        h.InsertNode(2, 2);
        h.InsertNode(2, 3);

        h.Display();

        Console.WriteLine(h.Size());
        Console.WriteLine(h.DeleteNode(2));
        Console.WriteLine(h.Size());
        Console.WriteLine(h.IsEmpty());
        Console.WriteLine(h.Get(2));
    }

    static void Main2()
    {
        doubleHash myHash = new doubleHash(
            13); // creates an empty hash table of size 13 

        /* Inserts random element in the hash table */

        int[] insertions = { 115, 12, 87, 66, 123 };
        int n1 = insertions.Length;

        for (int i = 0; i < n1; i++)
            myHash.insert(insertions[i]);

        Console.Write(
            "Status of hash table after initial insertions : ");
        myHash.print();

        /* 
		** Searches for random element in the hash table, 
		** and prints them if found. 
		*/

        int[] queries = { 1, 12, 2, 3, 69, 88, 115 };
        int n2 = queries.Length;

        Console.Write(
            "\n"
            + "Search operation after insertion : \n");

        for (int i = 0; i < n2; i++)
            if (myHash.search(queries[i]))
                Console.Write(queries[i] + " present\n");

        /* Deletes random element from the hash table. */

        int[] deletions = { 123, 87, 66 };
        int n3 = deletions.Length;

        for (int i = 0; i < n3; i++)
            myHash.erase(deletions[i]);

        Console.Write(
            "Status of hash table after deleting elements : ");
        myHash.print();
    }
}




class doubleHash
{

    int TABLE_SIZE, keysPresent, PRIME, MAX_SIZE = 10000001;
    List<int> hashTable;
    bool[] isPrime;

    /* Function to set sieve of Eratosthenes. */
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

    int hash1(int value) { return value % TABLE_SIZE; }

    int hash2(int value) { return PRIME - (value % PRIME); }

    bool isFull() { return (TABLE_SIZE == keysPresent); }

    public doubleHash(int n)
    {
        isPrime = new bool[MAX_SIZE + 1];
        __setSieve();
        TABLE_SIZE = n;

        /* Find the largest prime number smaller than hash 
		* table's size. */
        PRIME = TABLE_SIZE - 1;
        while (isPrime[PRIME] == true)
            PRIME--;

        keysPresent = 0;
        hashTable = new List<int>();
        /* Fill the hash table with -1 (empty entries). */
        for (int i = 0; i < TABLE_SIZE; i++)
            hashTable.Add(-1);
    }

    public void __printPrime(long n)
    {
        for (long i = 0; i <= n; i++)
            if (isPrime[i] == false)
                Console.Write(i + ", ");
        Console.WriteLine();
    }

    /* Function to insert value in hash table */
    public void insert(int value)
    {

        if (value == -1 || value == -2)
        {
            Console.Write(
                "ERROR : -1 and -2 can't be inserted in the table\n");
        }

        if (isFull())
        {
            Console.Write("ERROR : Hash Table Full\n");
            return;
        }

        int probe = hash1(value),
            offset
            = hash2(value); // in linear probing offset = 1; 

        while (hashTable[probe] != -1)
        {
            if (-2 == hashTable[probe])
                break; // insert at deleted element's 
                       // location 
            probe = (probe + offset) % TABLE_SIZE;
        }

        hashTable[probe] = value;
        keysPresent += 1;
    }

    public void erase(int value)
    {
        /* Return if element is not present */
        if (!search(value))
            return;

        int probe = hash1(value), offset = hash2(value);

        while (hashTable[probe] != -1)
            if (hashTable[probe] == value)
            {
                hashTable[probe]
                    = -2; // mark element as deleted (rather 
                          // than unvisited(-1)). 
                keysPresent--;
                return;
            }
            else
                probe = (probe + offset) % TABLE_SIZE;
    }

    public bool search(int value)
    {
        int probe = hash1(value), offset = hash2(value),
            initialPos = probe;
        bool firstItr = true;

        while (true)
        {
            if (hashTable[probe]
                == -1) // Stop search if -1 is encountered. 
                break;
            else if (hashTable[probe]
                    == value) // Stop search after finding 
                              // the element. 
                return true;
            else if (probe == initialPos
                    && !firstItr) // Stop search if one 
                                  // complete traversal of 
                                  // hash table is 
                                  // completed. 
                return false;
            else
                probe = ((probe + offset)
                        % TABLE_SIZE); // if none of the 
                                       // above cases occur 
                                       // then update the 
                                       // index and check 
                                       // at it. 

            firstItr = false;
        }
        return false;
    }

    /* Function to display the hash table. */
    public void print()
    {
        for (int i = 0; i < TABLE_SIZE; i++)
            Console.Write(hashTable[i] + ", ");
        Console.Write("\n");
    }
}

