namespace Common
{
    public static class Utils
    {
        // valueを範囲[from1, to1]から範囲[from2, to2]に変換する関数
        public static float Remap(this float value, float from1, float to1, float from2, float to2)
        {
            return (value - from1) / (to1 - from1) * (to2 - from2) + from2;
        }
    }
}