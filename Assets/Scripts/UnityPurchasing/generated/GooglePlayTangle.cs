// WARNING: Do not modify! Generated file.

namespace UnityEngine.Purchasing.Security {
    public class GooglePlayTangle
    {
        private static byte[] data = System.Convert.FromBase64String("TYaLfFSsDXJk525pQDQJDwkTKwqPpjz0fkGUrla+IvpisNVAu9FjsgB1ynvSrHL8YxGYNUGbvvL5gEtNjS5BYf3HQ9s/gXy+LYaS6azom0ZToeNa7dZhduWctpOb+94e8NGbspYuNPsiNhg8cttHWBKfFyZzUqYtSehXYxVYEfES5o/jdDWs7OHvLtW5OjQ7C7k6MTm5Ojo7t9ljCDWbi2x/ID9MsZtr77+pT8pwraSqprG0Pf1tY2trnjW2xUMjjCtelsGuiuVKTBonOzYPkiaHcBCC90Cr0tyeXaqJ6VrKobjNJrBUvnnncFqyDBC+JLMjawaQrw/U3v3/z9U+yx/Dt9wLuToZCzY9MhG9c73MNjo6Oj47OMafYz8XdVaZTjk4Ojs6");
        private static int[] order = new int[] { 5,13,6,4,4,9,10,13,10,12,13,13,13,13,14 };
        private static int key = 59;

        public static readonly bool IsPopulated = true;

        public static byte[] Data() {
        	if (IsPopulated == false)
        		return null;
            return Obfuscator.DeObfuscate(data, order, key);
        }
    }
}
