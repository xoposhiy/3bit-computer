using lib;

namespace progday23
{
    class Vm
    {
        private readonly int[] Screen = new int[8];
        private int caret = 0;

        public IEnumerable<string> Run(string program)
        {
            foreach (var c in program)
            {
                if (c == '>') caret = (caret + 1) % Screen.Length;
                if (c == 'p') Screen[caret]++;
                if (c == 'm') Screen[caret]--;
                if (c == 'd') Screen[caret] *= 2;
                if (c == 'z') Screen[caret] = 0;
                if (c == 'P')
                    for (int x = caret; x < Screen.Length; x++)
                        Screen[x]++;
                if (c == 'D')
                    for (int x = caret; x < Screen.Length; x++)
                        Screen[x] *= 2;
                if (c == 'M')
                    for (int x = caret; x < Screen.Length; x++)
                        Screen[x]--;
                if (c == 'Z')
                    for (int x = caret; x < Screen.Length; x++)
                        Screen[x] = 0;
                yield return ToString();
            }

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