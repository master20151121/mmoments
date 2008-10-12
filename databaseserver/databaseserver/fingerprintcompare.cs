#define VERBOSE
#define TRAILCHECK // recomended.

using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace databaseserver
{
    class fingerprintcompare
    {
        static int TIMESECTION_WEIGHT = 5; //how many mis-matching bands counts as a matching frame.
        static int BANDS = 24;
        static int NOTEWORTHY = 3; // for finding start. less is more strict.

        static public int files(string fileA, string fileB)
        {
            return 0;
        }

        static public int strings(string fingerA, string fingerB)
        {
            return 0;
        }

        static public int arrays(string[] f1, string[] f2)
        {
            return 0;
        }
    }
}
