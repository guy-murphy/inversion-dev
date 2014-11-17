using System.Collections.Concurrent;
using System.Threading;

namespace Inversion.Process {

	/// <summary>
	/// A collection of `ViewStep` objects representing
	/// the steps taken in a view generating pipeline.
	/// </summary>
	/// <remarks>
	/// This is currently badly implemented as a concurrent stack,
	/// and needs to change. This needs to become a synchronised
	/// collection.
	/// </remarks>
	public class ViewSteps : ConcurrentStack<ViewStep> {

		private readonly ReaderWriterLockSlim _lock = new ReaderWriterLockSlim();
		private bool _isDisposed;

		/// <summary>
		/// Determines whether or not there are any
		/// steps.
		/// </summary>
		public bool HasSteps {
			get {
				try {
					_lock.EnterReadLock();
					return base.Count > 0;
				} finally {
					_lock.ExitReadLock();
				}
			}
		}

		/// <summary>
		/// The last step in the current pipeline.
		/// </summary>
		public ViewStep Last {
			get {
				try {
					_lock.EnterReadLock();
					ViewStep last;
					base.TryPeek(out last);
					return last;
				} finally {
					_lock.ExitReadLock();
				}
			}
		}

		public void Dispose() {
			this.Dispose(true);
		}

		public void Dispose(bool disposing) {
			if (!_isDisposed) {
				if (disposing) {
					_lock.Dispose();
				}
			}
			_isDisposed = true;
		}

		public void CreateStep(string name, string contentType, string content) {
			this.Push(new ViewStep(name, contentType, content));
		}

		public void CreateStep(string name, IData model) {
			this.Push(new ViewStep(name, model));
		}

	}
}
