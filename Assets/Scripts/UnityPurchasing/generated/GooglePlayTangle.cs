// WARNING: Do not modify! Generated file.

namespace UnityEngine.Purchasing.Security {
    public class GooglePlayTangle
    {
        private static byte[] data = System.Convert.FromBase64String("MxsoYFO/DNDVMtNFOjKdbzC7tKHDnMTRYuIBWDrTL2/rz4JlqegsZEYec7Zy1XgTj08ivq/I9Diku2jic5xM/43mXgO1x+XLWbsV9QJjT4IwvbwBxOSpW31n43ikZekdKM8EUsZ099TG+/D/3HC+cAH79/f38/b1cVGRNJxIcFyMekZzxj7CNAAPj8dl6+6DqOwMuX2ytFtmBpmXV+cMRnNlGfni/AQyEcCjrvPrTMFv2a6GdPf59sZ09/z0dPf39gErhYCLuZCM+ViztQVseWjZCy34XjrEAUNJ4phUaaYxvf2md9w8C3+cbOxpW+6UJxwkQMbuGvCMKxa+AMq5jZIN79sIYtPD9pjaOxi+GOBz2vpF6Puesgxq2/OPJk/j8fT19/b3");
        private static int[] order = new int[] { 13,11,8,6,9,13,13,12,8,11,12,12,12,13,14 };
        private static int key = 246;

        public static readonly bool IsPopulated = true;

        public static byte[] Data() {
        	if (IsPopulated == false)
        		return null;
            return Obfuscator.DeObfuscate(data, order, key);
        }
    }
}
