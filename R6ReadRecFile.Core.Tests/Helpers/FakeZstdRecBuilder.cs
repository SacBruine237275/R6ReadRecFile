using System.Text;
using ZstdSharp;

namespace R6ReadRecFile.Core.Tests.Helpers
{
    /// <summary>
    /// Builds a minimal fake .rec file: a header followed by one size-prefixed Zstd frame
    /// containing player blocks laid out like in real Siege X replays.
    /// </summary>
    internal class FakeZstdRecBuilder
    {
        public static readonly byte[] KillIndicator = { 0x1C, 0xD2, 0xB1, 0x9D };
        public static readonly byte[] DeathIndicator = { 0xCD, 0x9C, 0x5D, 0x72 };
        public static readonly byte[] AssistIndicator = { 0x4D, 0x73, 0x7F, 0x9E };
        public static readonly byte[] UnknownIndicator = { 0x31, 0xA3, 0xD4, 0xD2 };
        public static readonly byte[] ScoreIndicator = { 0xEC, 0xDA, 0x4F, 0x80 };
        private static readonly byte[] PlayerIndicator = { 0x22, 0x07, 0x94, 0x9B, 0xDC };
        private static readonly byte[] SpawnIndicator = { 0xAF, 0x98, 0x99, 0xCA };
        private const int NameIndicatorOffsetFromId = 148;

        private readonly MemoryStream _data = new();

        // Init block: "23 <id> 00000000 <stats...> ... 22 07 94 9B DC <name> ... AF 98 99 CA <spawn>"
        public FakeZstdRecBuilder AddPlayer(uint id, string name, string spawn, int kills = 0, int deaths = 0, int assists = 0, int score = 0)
        {
            WriteHeader(id);
            long idPosition = _data.Position - 8;
            WriteStat(KillIndicator, kills, withPrefix: false);
            WriteStat(DeathIndicator, deaths, withPrefix: true);
            WriteStat(AssistIndicator, assists, withPrefix: true);
            WriteStat(UnknownIndicator, 0, withPrefix: true);
            WriteStat(ScoreIndicator, score, withPrefix: true);
            while (_data.Position < idPosition + NameIndicatorOffsetFromId)
                _data.WriteByte(0x00);
            _data.Write(PlayerIndicator);
            WriteString(name);
            _data.Write(SpawnIndicator);
            WriteString(spawn);
            return this;
        }

        // Mid-match update: "23 <id> 00000000 <first stat> 22 <next stat> ..."
        public FakeZstdRecBuilder AddStatUpdate(uint id, params (byte[] indicator, int value)[] stats)
        {
            WriteHeader(id);
            for (int i = 0; i < stats.Length; i++)
                WriteStat(stats[i].indicator, stats[i].value, withPrefix: i > 0);
            return this;
        }

        public FakeZstdRecBuilder AddBytes(params byte[] bytes)
        {
            _data.Write(bytes);
            return this;
        }

        public FakeZstdRecBuilder AddPadding(int count)
        {
            for (int i = 0; i < count; i++)
                _data.WriteByte(0xFF);
            return this;
        }

        public byte[] Build()
        {
            using var compressor = new Compressor();
            var frame = compressor.Wrap(_data.ToArray()).ToArray();
            using var file = new MemoryStream();
            file.Write(Encoding.ASCII.GetBytes("dissect\0"));
            file.Write(BitConverter.GetBytes(frame.Length));
            file.Write(frame);
            return file.ToArray();
        }

        private void WriteHeader(uint id)
        {
            _data.WriteByte(0x23);
            _data.Write(BitConverter.GetBytes(id));
            _data.Write(BitConverter.GetBytes(0u));
        }

        private void WriteStat(byte[] indicator, int value, bool withPrefix)
        {
            if (withPrefix)
                _data.WriteByte(0x22);
            _data.Write(indicator);
            _data.WriteByte(0x04);
            _data.Write(BitConverter.GetBytes(value));
        }

        private void WriteString(string value)
        {
            var bytes = Encoding.UTF8.GetBytes(value);
            _data.WriteByte((byte)bytes.Length);
            _data.Write(bytes);
        }
    }
}
