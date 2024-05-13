using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgorithmLab2
{
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
}