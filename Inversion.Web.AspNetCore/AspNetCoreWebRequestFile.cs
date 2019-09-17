using System.IO;
using Microsoft.AspNetCore.Http;

namespace Inversion.Web.AspNetCore
{
    public class AspNetCoreWebRequestFile : IRequestFile
    {
        private readonly IFormFile _underlyingFile;

        public AspNetCoreWebRequestFile(IFormFile underlyingFile)
        {
            _underlyingFile = underlyingFile;
        }

        public string FileName => _underlyingFile.FileName;
        public string ContentType => _underlyingFile.ContentType;

        public Stream Content => _underlyingFile.OpenReadStream();
    }
}