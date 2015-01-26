using System;
using System.Collections.Concurrent;
using System.Threading;

namespace Inversion.Process {

	/// <summary>
	/// A collection of view step objects representing
	/// the steps taken in a view generating pipeline.
	/// </summary>
	/// <remarks>
	/// This is currently badly implemented as a concurrent stack,
	/// and needs to change. This needs to become a synchronised
	/// collection.
	/// </remarks>
	public class ViewSteps : ConcurrentStack<ViewStep>, IDisposable {

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

		/// <summary>
		/// Releases all resources maintained by the current context instance.
		/// </summary>
		public void Dispose() {
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		/// <summary>
		/// Disposal that allows for partitioning of 
		/// clean-up of managed and unmanaged resources.
		/// </summary>
		/// <param name="disposing"></param>
		/// <remarks>
		/// This is looking conceited and should probably be removed.
		/// I'm not even sure I can explain a use case for it in terms
		/// of an Inversion context.
		/// </remarks>
		protected virtual void Dispose(bool disposing) {
			if (!_isDisposed) {
				if (disposing) {
					// managed resource clean-up
					_lock.Dispose();
				}
				// unmanaged resource clean-up
				// ... nothing to do
				// call dispose on base class, and clear data
				// base.Dispose(disposing);
				// mark disposing as done
				_isDisposed = true;
			}
		}

		/// <summary>
		/// Creates a new view step and pushes it onto the
		/// stack of view steps to be processed.
		/// </summary>
		/// <param name="name">The name of the view step to be created.</param>
		/// <param name="contentType">The content type that the new view step represents.</param>
		/// <param name="content">The actual content of the new view step.</param>
		public void CreateStep(string name, string contentType, string content) {
			this.Push(new ViewStep(name, contentType, content));
		}

		/// <summary>
		/// Creates a new view step and pushes it onto the
		/// stack of view steps to be processed.
		/// </summary>
		/// <param name="name">The name of the view step to be created.</param>
		/// <param name="model">The actual model of the new view step.</param>
		public void CreateStep(string name, IData model) {
			this.Push(new ViewStep(name, model));
		}

	}
}
