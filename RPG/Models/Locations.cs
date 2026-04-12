using System.Data.Common;
using System.Dynamic;

namespace RPG.Models.Locations
{
    internal abstract class Location<I> : IInfoAboutWorld
    {
        private string _str = "";
        internal I? id { get; set; }
        internal string name
        {
            get => FirstUpSymbolToUpper.Apply(_str);
            set => _str = value;
        }
        internal I? difficulty { get; set; }
        internal string? type { get; set;}
    }

    internal class Type
    {
        internal Dictionary<int, string> Types = ListOfTypes;
        internal static readonly Dictionary<int, string> ListOfTypes = new()
        {
            {1, "Forest"},
            {2, "Ruins"},
            {3, "Spider Cave"}
        };
    }
}