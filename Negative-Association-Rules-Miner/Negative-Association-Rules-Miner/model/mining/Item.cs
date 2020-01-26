using System;

namespace Negative_Association_Rules_Miner.model
{
    /// <summary>
    /// Single item model 
    /// </summary>
    public class Item : IComparable
    {
        public string Name { get; set; }

        public int CompareTo(object obj)
        {
            Item item = (Item)obj;
            return String.Compare(Name, item.Name);
        }

    }
}
