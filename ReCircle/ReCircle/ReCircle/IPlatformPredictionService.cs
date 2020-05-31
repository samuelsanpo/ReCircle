using System;
using System.IO;
using System.Threading.Tasks;

namespace ReCircle
{
    public interface IPlatformPredictionService
    {
        Task<ClassificationResult> Classify(Stream imageStream);
    }
}
