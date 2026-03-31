using RPG.UI.ConsoleInfo;

namespace RPG.Core.Entity
{
    internal partial class Player<I, L>
        where I: notnull
        where L: notnull
    {
        private string str = "";
        internal string Name { get => FirstUpSymbolToUpper.Apply(str); set => str = value; }
        internal I HP { get; set; }
        internal L Experience { get; set; }
        internal string Class { get => FirstUpSymbolToUpper.Apply(str); set => str = value; }

    }
    
    internal struct Levels
    {
        internal List<int> level = new List<int>;
        
    }
}