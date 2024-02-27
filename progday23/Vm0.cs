using System.Collections;
using lib;

namespace progday23
{
    class Vm0
    {
        private readonly int[] Screen = new int[10];
        private int caret = 0;
        private int selectionStart = -1;

        public IEnumerable<string> Run(string program)
        {
            foreach (var c in program)
            {
                if (c == 'R') caret = (caret + 1) % Screen.Length;
                if (c == 'L') caret = (caret + Screen.Length - 1) % Screen.Length;
                if (c == '+')
                    foreach (var pos in GetSelection())
                        Screen[pos]++;
                if (c == '-')
                    foreach (var pos in GetSelection())
                        Screen[pos]--;
                if (c == '*')
                    foreach (var pos in GetSelection())
                        Screen[pos]*=2;
                if (c == 'S') selectionStart = caret;
                if (c == 'C') selectionStart = -1;
                yield return c + " → " + ToString();
            }

        }

        private IEnumerable<int> GetSelection()
        {
            if (selectionStart == -1)
                yield return caret;
            else
                for (int i = selectionStart; i <= caret; i++)
                    yield return i;
        }

        public override string ToString()
        {
            return Screen.Select(GetSymbol).StrJoin("");
        }

        private char GetSymbol(int value)
        {
            if (value == 0) return ' ';
            if (value < 23) return (char)('A' + value - 1);
            return '#';
        }
    }
}