using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Fuzzlyn.ExecutionServer
{
    public class ProgramResult
    {
        public ProgramResult(
            string checksum,
            string exceptionType,
            string exceptionText,
            string exceptionStackTrace,
            List<ChecksumSite> checksumSites)
        {
            Checksum = checksum;
            ExceptionType = exceptionType;
            ExceptionText = exceptionText;
            ExceptionStackTrace = exceptionStackTrace;
            ChecksumSites = checksumSites;
        }

        public string Checksum { get; }
        public string ExceptionType { get; }
        public string ExceptionText { get; }
        public string ExceptionStackTrace { get; }
        [JsonIgnore]
        public List<ChecksumSite> ChecksumSites { get; }
    }
}
