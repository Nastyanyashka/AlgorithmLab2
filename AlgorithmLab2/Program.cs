using AlgorithmLab2;
using System;
class Program
{
    static void Main(string[] args)
    {

        HashMap h = new HashMap(20);

        h.InsertNode(1, 1);
        h.InsertNode(2, 2);
        h.InsertNode(12, 3);

        h.Display();

        Console.WriteLine(h.Size());
        Console.WriteLine(h.DeleteNode(2));
        Console.WriteLine(h.Size());
        Console.WriteLine(h.IsEmpty());
        Console.WriteLine(h.Get(2));


        //////
        ///

        doubleHash myHash = new doubleHash(200);
        int[] insertionsKey = { 115, 12, 87, 66, 123 };
        int[] insertionsValue = { 115, 122, 871, 123, 123 };

        for (int i = 0; i < insertionsKey.Length; i++)
            myHash.insert(insertionsKey[i], insertionsValue[i]);

        Console.Write(
            "Хэш таблица после заполнения: ");
        myHash.print();
        int[] queries = { 1, 12, 2, 3, 69, 88, 115 };
        int n2 = queries.Length;

        Console.Write(
            "\n"
            + "Выполним поиск после : \n");

        for (int i = 0; i < n2; i++)
        {
            int searchResult = myHash.search(queries[i]);
            if (searchResult != -1)
                myHash.print(searchResult);
        }


        int[] deletions = { 123, 87, 66 };
        int n3 = deletions.Length;

        for (int i = 0; i < n3; i++)
            myHash.erase(deletions[i]);

        Console.Write(
            "Хэш таблица после удаления : ");
        myHash.print();
    }

}