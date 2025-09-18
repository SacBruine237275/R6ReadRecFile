using R6ReadRecFile.Core.Models;

namespace R6ReadRecFile.Core.Interfaces
{
    public interface IRecParser
    {
        RecFile Parse(FileStream file);
    }
}
