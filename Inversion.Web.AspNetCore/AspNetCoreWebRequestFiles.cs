using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Http;

namespace Inversion.Web.AspNetCore
{
    public class AspNetCoreWebRequestFiles : IRequestFilesCollection
    {
        private readonly IFormFileCollection _files;
        private IDictionary<string, IRequestFile> _memo;

        public AspNetCoreWebRequestFiles(IFormFileCollection underlyingCollection)
        {
            _files = underlyingCollection;
        }

        public IEnumerator<KeyValuePair<string, IRequestFile>> GetEnumerator()
        {
            return Memo.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        protected IDictionary<string, IRequestFile> Memo
        {
            get
            {
                if (_memo == null)
                {
                    _memo = new Dictionary<string, IRequestFile>();
                    foreach (IFormFile file in _files)
                    {
                        _memo[file.FileName] = new AspNetCoreWebRequestFile(file);
                    }
                }
                return _memo;
            }
        }

        public IRequestFile this[string key] => this.Get(key);

        public IRequestFile Get(string key)
        {
            IRequestFile value;
            this.TryGetValue(key, out value);
            return value;
        }

        public bool TryGetValue(string key, out IRequestFile value)
        {
            return this.Memo.TryGetValue(key, out value);
        }
    }
}
