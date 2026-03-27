using System;
using System.Reflection;
using BfresLibrary;
using BfresLibrary.Core;

namespace Bfres.Structs
{
    // Reflection-based wrappers for BfresLibrary's internal ResFileLoader.ImportSection
    // and ResFileSaver.ExportSection, which are not exposed publicly on Bone/Skeleton.
    internal static class BfresWiiUImportExport
    {
        private static readonly MethodInfo _importSection;
        private static readonly MethodInfo _exportSection;

        static BfresWiiUImportExport()
        {
            _importSection = typeof(ResFileLoader).GetMethod(
                "ImportSection",
                BindingFlags.NonPublic | BindingFlags.Static,
                null,
                new[] { typeof(string), typeof(IResData), typeof(ResFile) },
                null);

            _exportSection = typeof(ResFileSaver).GetMethod(
                "ExportSection",
                BindingFlags.NonPublic | BindingFlags.Static,
                null,
                new[] { typeof(string), typeof(IResData), typeof(ResFile) },
                null);
        }

        public static void ImportSection(string fileName, IResData resData, ResFile resFile)
        {
            if (_importSection == null)
                throw new InvalidOperationException("BfresLibrary.Core.ResFileLoader.ImportSection not found via reflection.");
            _importSection.Invoke(null, new object[] { fileName, resData, resFile });
        }

        public static void ExportSection(string fileName, IResData resData, ResFile resFile)
        {
            if (_exportSection == null)
                throw new InvalidOperationException("BfresLibrary.Core.ResFileSaver.ExportSection not found via reflection.");
            _exportSection.Invoke(null, new object[] { fileName, resData, resFile });
        }
    }
}
