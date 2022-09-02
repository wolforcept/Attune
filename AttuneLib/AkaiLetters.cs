using System;
using System.Collections.Generic;
using System.Text;

namespace AttuneLib;

static class AkaiLetters
{

    public const int LETTER_WIDTH = 5, LETTER_HEIGHT = 5;

    public static bool[][] GetLetter(char c)
    {
        if (!dictionary.ContainsKey(c))
            return new bool[][] { };
        return dictionary[c];
    }

    public static void UseLetters(string text, Action<int, int> action)
    {
        int prevLetterW = 0;
        int x = 0, y = 0;
        foreach (char c in text)
        {
            bool[][] letterMatrix = GetLetter(c);
            for (int dy = 0; dy < letterMatrix.Length; dy++)
            {
                for (int dx = 0; dx < letterMatrix[dy].Length; dx++)
                {
                    if (letterMatrix[dy][dx] && x + dx >= 0 && y + dy >= 0 && x + dx < 128 && y + dy < 64)
                        action.Invoke(prevLetterW + dx, dy);
                }
            }
            prevLetterW += LETTER_WIDTH + 1;
        }
    }

    private static Dictionary<char, bool[][]> dictionary = new Dictionary<char, bool[][]>();

    static AkaiLetters()
    {

        addLetter(@"_xxx.
                    x___x.
                    xxxxx.
                    x___x.
                    x___x.", 'a', 'A');

        addLetter(@"xxxx.
                    x___x.
                    xxxx.
                    x___x.
                    xxxx.", 'b', 'B');

        addLetter(@"_xxxx.
                    x.
                    x.
                    x.
                    _xxxx.", 'c', 'C');

        addLetter(@"xxxx.
                    x___x.
                    x___x.
                    x___x.
                    xxxx.", 'd', 'D');

        addLetter(@"xxxxx.
                    x.
                    xxx.
                    x.
                    xxxxx.", 'e', 'E');

        addLetter(@"xxxxx.
                    x.
                    xxx.
                    x.
                    x.", 'f', 'F');

        addLetter(@"_xxx.
                    x.
                    x__xx.
                    x___x.
                    _xxx.", 'g', 'G');

        addLetter(@"x___x.
                    x___x.
                    xxxxx.
                    x___x.
                    x___x.", 'h', 'H');

        addLetter(@"xxx.
                    _x.
                    _x.
                    _x.
                    xxx.", 'i', 'I');

        addLetter(@"_xxxx.
                    ___x.
                    ___x.
                    x__x.
                    _xx.", 'j', 'J');

        addLetter(@"x___x.
                    x__x.
                    xxx.
                    x__x.
                    x___x.", 'k', 'K');

        addLetter(@"x.
                    x.
                    x.
                    x.
                    _xxx.", 'l', 'L');

        addLetter(@"xx_xx.
                    x_x_x.
                    x___x.
                    x___x.
                    x___x.", 'm', 'M');

        addLetter(@"x___x.
                    xx__x.
                    x_x_x.
                    x__xx.
                    x___x.", 'n', 'N');

        addLetter(@"_xxx.
                    x___x.
                    x___x.
                    x___x.
                    _xxx.", 'o', 'O');

        addLetter(@"xxxx.
                    x___x.
                    xxxx.
                    x.
                    x.", 'p', 'P');

        addLetter(@"_xxx.
                    x___x.
                    x___x.
                    x__xx.
                    _xxxxx.", 'q', 'Q');

        addLetter(@"xxxx.
                    x___x.
                    xxxx.
                    x_x.
                    x__x.", 'r', 'R');

        addLetter(@"_xxxx.
                    x.
                    _xxx.
                    ____x.
                    xxxx.", 's', 'S');

        addLetter(@"xxxxx.
                    __x.
                    __x.
                    __x.
                    __x.", 't', 'T');

        addLetter(@"x___x.
                    x___x.
                    x___x.
                    x___x.
                    _xxx.", 'u', 'U');

        addLetter(@"x___x.
                    x___x.
                    _x_x.
                    _x_x.
                    __x.", 'v', 'V');

        addLetter(@"x___x.
                    x___x.
                    x_x_x.
                    x_x_x.
                    _x_x.", 'w', 'W');

        addLetter(@"x___x.
                    _x_x.
                    __x.
                    _x_x.
                    x___x.", 'x', 'X');

        addLetter(@"x___x.
                    x___x.
                    _x_x.
                    __x.
                    __x.", 'y', 'Y');

        addLetter(@"xxxxx.
                    ___x.
                    __x.
                    _x.
                    xxxxx.", 'z', 'Z');

        //
        // NUMBERS

        addLetter(@"xx.
                    _x.
                    _x.
                    _x.
                    _x.", '1');

        addLetter(@"_xxx.
                    ____x.
                    __xx.
                    _x.
                    xxxxx.", '2');

        addLetter(@"_xxx.
                    x___x.
                    ___x.
                    x___x.
                    _xxx.", '3');

        addLetter(@"x___x.
                    x___x.
                    _xxxx.
                    ____x.
                    ____x.", '4');

        addLetter(@"xxxxx.
                    x.
                    xxxx.
                    ____x.
                    _xxx.", '5');

        addLetter(@"_xxx.
                    x.
                    xxxx.
                    x___x.
                    _xxx.", '6');

        addLetter(@"xxxx.
                    ___x.
                    __x.
                    _x.
                    x.", '7');

        addLetter(@"_xxx.
                    x___x.
                    _xxx.
                    x___x.
                    _xxx.", '8');

        addLetter(@"_xxx.
                    x___x.
                    _xxxx.
                    ____x.
                    ____x.", '9');

        addLetter(@"_xxx.
                    xx__x.
                    x_x_x.
                    x__xx.
                    _xxx.", '0');

        //
        // PUNCTUATION

        addLetter(@".
                    .
                    xxx.", '-');

        addLetter(@".
                    .
                    .
                    .
                    x.", '.');

        addLetter(@"x.
                    x.
                    x.
                    .
                    x.", '!');

        addLetter(@"xx.
                    __x.
                    _x.
                    .
                    _x.", '?');

        addLetter(@".
                    .
                    .
                    .
                    _x.
                    x.", ',');

        addLetter(@".
                    x.
                    .
                    x.
                    .", ':');

        addLetter(@".
                    _x.
                    .
                    .
                    _x.
                    x.", ';');

        addLetter(@"__x.
                    __x.
                    _x.
                    _x.
                    x.
                    x.", '/');

        addLetter(@"x.
                    x.
                    _x.
                    _x.
                    __x.
                    __x.", '\\');

        addLetter(@"xxx.
                    __x.
                    __x.
                    __x.
                    __x.
                    xxx.", ']');

        addLetter(@"xxx.
                    x.
                    x.
                    x.
                    x.
                    xxx.", '[');

    }

    private static void addLetter(string letter, params char[] chars)
    {
        List<bool[]> matrix = new List<bool[]>();
        List<bool> currLine = new List<bool>();
        foreach (char c in letter)
        {
            if (c == '.')
            {
                matrix.Add(currLine.ToArray());
                currLine.Clear();
            }
            else if (c == 'x' || c == '_')
                currLine.Add(c == 'x');
        }
        bool[][] finalMatrix = matrix.ToArray();
        foreach (char c in chars)
        {
            dictionary[c] = finalMatrix;
        }
    }
}
