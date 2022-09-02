using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AttuneLib;

internal class Programs
{

    public static readonly List<Tuple<string, Func<Attune, AkaiFire, AttuneProgram>>> Values = new()
    {

        new("KEYBOARD", (attune, fire) =>
        {
            return new KeyboardProgram("KEYBOARD", attune, fire);
        }),

        new("ATTUNE", (attune, fire) =>
        {
            return new PlayerProgram("ATTUNE", attune, fire);
        }),

        new("DEBUG", (attune, fire) =>
        {
            return new DebugProgram("DEBUG", attune, fire);
        })
    };

}

