using System.Data;

namespace Population.IO
{
    public interface IInputReader
    {
        void BeginRead();
        void EndRead();

        ICommandReader<Estimate> GetEstimateReader();
        ICommandReader<Actual> GetActualReader();
    }
}