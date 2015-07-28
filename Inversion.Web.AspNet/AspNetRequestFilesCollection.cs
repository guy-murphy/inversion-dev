using System.Collections;
using System.Collections.Generic;
using System.Web;

namespace Inversion.Web.AspNet {
	public class AspNetRequestFilesCollection : IRequestFilesCollection {

		private readonly HttpFileCollection _files;
		private IDictionary<string, IRequestFile> _memo;

		public IRequestFile this[string key] {
			get { return this.Get(key); }
		}

		protected IDictionary<string, IRequestFile> Memo {
			get {
				if (_memo == null) {
					_memo = new Dictionary<string, IRequestFile>();
					foreach (string key in _files.AllKeys)
					{
					    HttpPostedFile file = _files[key];
					    if (file != null)
					    {
					        _memo[file.FileName] = new AspNetPostedFile(file);
					    }
					}
				}
				return _memo;
			}
		}

		public AspNetRequestFilesCollection(HttpFileCollection files) {
			_files = files;
		}

		public IRequestFile Get(string key) {
			IRequestFile value;
			this.TryGetValue(key, out value);
			return value;
		}

		public bool TryGetValue(string key, out IRequestFile value) {
			return this.Memo.TryGetValue(key, out value);
		}

		/// <summary>
		/// Returns an enumerator that iterates through the collection.
		/// </summary>
		/// <returns>
		/// A <see cref="T:System.Collections.Generic.IEnumerator`1"/> that can be used to iterate through the collection.
		/// </returns>
		public IEnumerator<KeyValuePair<string, IRequestFile>> GetEnumerator() {
			return this.Memo.GetEnumerator();
		}

		/// <summary>
		/// Returns an enumerator that iterates through a collection.
		/// </summary>
		/// <returns>
		/// An <see cref="T:System.Collections.IEnumerator"/> object that can be used to iterate through the collection.
		/// </returns>
		IEnumerator IEnumerable.GetEnumerator() {
			return this.GetEnumerator();
		}

	}
}
