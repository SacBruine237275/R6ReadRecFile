namespace R6ReadRecFile.Core.Utils
{

    /// <summary>
    /// Helper used for development purposes only.
    /// Simplifies the creation of separate binary files 
    /// to allow easier analysis of decompressed data.
    /// </summary>
    public class ZSTDHelperForTest : ZSTDHelper
    {

        public ZSTDHelperForTest(byte[] data, string outputDirectory)
            : base(data, CreateBlockWriter(outputDirectory))
        {
        }

        private static Action<int, ReadOnlySpan<byte>> CreateBlockWriter(string outputDirectory)
        {
            return (blockIndex, block) =>
                File.WriteAllBytes(Path.Combine(outputDirectory, $"block_{blockIndex}.bin"), block);
        }

    }
}
