namespace Common
{
    public static class Utils
    {
        // value๐ออ[from1, to1]ฉ็ออ[from2, to2]ษฯทท้ึ
        public static float Remap(this float value, float from1, float to1, float from2, float to2)
        {
            return (value - from1) / (to1 - from1) * (to2 - from2) + from2;
        }
    }
}