using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading;
using System.Xml;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Inversion.Collections {

	/// <summary>
	/// An implementation of <see cref="IDataCollection{T}"/> that
	/// is safe for concurrent use.
	/// </summary>
	/// <typeparam name="T">The type of the elements in the collection.</typeparam>

	public class ConcurrentDataCollection<T> : IDataCollection<T>, IDisposable {

		private bool _isDisposed;

		private readonly ReaderWriterLockSlim _lock = new ReaderWriterLockSlim(LockRecursionPolicy.SupportsRecursion);
		private readonly string _label;
		private readonly Collection<T> _inner;

		/// <summary>
		/// The label that should be used for the collection in
		/// any notation presenting the collection. 
		/// </summary>
		/// <remarks>This will default to "list".</remarks>
		public string Label { get { return _label ?? "list"; } }

		/// <summary>
		/// Provides an abstract representation
		/// of the objects data expressed as a JSON object.
		/// </summary>
		/// <remarks>
		/// For this type the json object is created each time
		/// it is accessed.
		/// </remarks>
		public JObject Data {
			get {
				_lock.EnterReadLock();
				try {
					return this.ToJsonObject();
				} finally {
					_lock.ExitReadLock();
				}
			}
		}

		/// <summary>
		/// Instantiates an empty collection.
		/// </summary>
		public ConcurrentDataCollection() : this("list") { }

		/// <summary>
		/// Instantiates an empty collection.
		/// </summary>
		/// <param name="label">The label to use for this collection.</param>
		public ConcurrentDataCollection(string label) : this(label, null) { }

		/// <summary>
		/// Instantiates a new data collection with elements
		/// copied from the provided collection.
		/// </summary>
		/// <param name="collection">The collection to copy elements from.</param>
		public ConcurrentDataCollection(IEnumerable<T> collection) : this("list", collection) { }

		/// <summary>
		/// Instantiates a new data collection with elements
		/// copied from the provided collection.
		/// </summary>
		/// <param name="label">The label to use for this collection.</param>
		/// <param name="collection">The collection to copy elements from.</param>
		public ConcurrentDataCollection(string label, IEnumerable<T> collection) {
			_label = label;
			_inner = (collection == null) ? new Collection<T>() : new Collection<T>(collection.ToList()); // maybe just assign the list?
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

		object ICloneable.Clone() {
			_lock.EnterReadLock();
			try {
				return new ConcurrentDataCollection<T>(this);
			} finally {
				_lock.ExitReadLock();
			}
		}

		/// <summary>
		/// Creates a new instance of a data collection as a copy
		/// of this data collection.
		/// </summary>
		/// <returns></returns>
		public ConcurrentDataCollection<T> Clone() {
			_lock.EnterReadLock();
			try {
				return new ConcurrentDataCollection<T>(this);
			} finally {
				_lock.ExitReadLock();
			}
		}

		/// <summary>
		/// Produces an XML representation of the collections elements  to a provided writer.
		/// </summary>
		/// <param name="writer">
		/// The <see cref="XmlWriter"/> the representation is written to.
		/// </param>

		protected virtual void ContentToXml(XmlWriter writer) {
			foreach (T item in this) {
				if (item is ValueType) {
					writer.WriteElementString("item", item.ToString());
				} else if (item is IData) {
					((IData)item).ToXml(writer);
				} else {
					writer.WriteElementString("item", item.ToString());
				}
			}
		}

		/// <summary>
		/// Produces an JSON representation of the collection to a provided writer.
		/// </summary>
		/// <param name="writer">
		/// The <see cref="JsonWriter"/> the representation is written to.
		/// </param>

		protected virtual void ContentToJson(JsonWriter writer) {
			foreach (T item in this) {
				if (item is ValueType) {
					writer.WriteValue(item.ToString());
				} else if (item != null) {
					if (item is IData) {
						((IData)item).ToJson(writer);
					} else {
						writer.WriteValue(item.ToString());
					}
				}
			}
		}

		/// <summary>
		/// Produces an XML representation of the collection  to a provided writer.
		/// </summary>
		/// <param name="writer">
		/// The <see cref="XmlWriter"/> the representation is written to.
		/// </param>

		public void ToXml(XmlWriter writer) {
			_lock.EnterReadLock();
			try {
				writer.WriteStartElement("list");
				this.ContentToXml(writer);
				writer.WriteEndElement();
			} finally {
				_lock.ExitReadLock();
			}
		}

		/// <summary>
		/// Produces an JSON representation of the collection  to a provided writer.
		/// </summary>
		/// <param name="writer">
		/// The <see cref="JsonWriter"/> the representation is written to.
		/// </param>

		public void ToJson(JsonWriter writer) {
			_lock.EnterReadLock();
			try {
				writer.WriteStartArray();
				this.ContentToJson(writer);
				writer.WriteEndArray();
			} finally {
				_lock.ExitReadLock();
			}
		}

		/// <summary>
		/// Returns an enumerator that iterates through the collection.
		/// </summary>
		/// <returns>
		/// A <see cref="T:System.Collections.Generic.IEnumerator`1"/> that can be used to iterate through the collection.
		/// </returns>
		public IEnumerator<T> GetEnumerator() {
			return new DataCollection<T>(this).GetEnumerator();
		}

		/// <summary>
		/// Returns an enumerator that iterates through a collection.
		/// </summary>
		/// <returns>
		/// An <see cref="T:System.Collections.IEnumerator"/> object that can be used to iterate through the collection.
		/// </returns>
		IEnumerator IEnumerable.GetEnumerator() {
			return new DataCollection<T>(this).GetEnumerator();
		}

		/// <summary>
		/// Adds an item to the <see cref="T:System.Collections.Generic.ICollection`1"/>.
		/// </summary>
		/// <param name="item">The object to add to the <see cref="T:System.Collections.Generic.ICollection`1"/>.</param><exception cref="T:System.NotSupportedException">The <see cref="T:System.Collections.Generic.ICollection`1"/> is read-only.</exception>
		public void Add(T item) {
			_lock.EnterWriteLock();
			try {
				_inner.Add(item);	
			} finally {
				_lock.ExitWriteLock();
			}
		}

		/// <summary>
		/// Removes all items from the <see cref="T:System.Collections.Generic.ICollection`1"/>.
		/// </summary>
		/// <exception cref="T:System.NotSupportedException">The <see cref="T:System.Collections.Generic.ICollection`1"/> is read-only. </exception>
		public void Clear() {
			_lock.EnterWriteLock();
			try {
				_inner.Clear();
			} finally {
				_lock.ExitWriteLock();
			}
		}

		/// <summary>
		/// Determines whether the <see cref="T:System.Collections.Generic.ICollection`1"/> contains a specific value.
		/// </summary>
		/// <returns>
		/// true if <paramref name="item"/> is found in the <see cref="T:System.Collections.Generic.ICollection`1"/>; otherwise, false.
		/// </returns>
		/// <param name="item">The object to locate in the <see cref="T:System.Collections.Generic.ICollection`1"/>.</param>
		public bool Contains(T item) {
			_lock.EnterReadLock();
			try {
				return _inner.Contains(item);
			} finally {
				_lock.ExitReadLock();
			}
		}

		/// <summary>
		/// Copies the elements of the <see cref="T:System.Collections.Generic.ICollection`1"/> to an <see cref="T:System.Array"/>, starting at a particular <see cref="T:System.Array"/> index.
		/// </summary>
		/// <param name="array">The one-dimensional <see cref="T:System.Array"/> that is the destination of the elements copied from <see cref="T:System.Collections.Generic.ICollection`1"/>. The <see cref="T:System.Array"/> must have zero-based indexing.</param><param name="arrayIndex">The zero-based index in <paramref name="array"/> at which copying begins.</param><exception cref="T:System.ArgumentNullException"><paramref name="array"/> is null.</exception><exception cref="T:System.ArgumentOutOfRangeException"><paramref name="arrayIndex"/> is less than 0.</exception><exception cref="T:System.ArgumentException">The number of elements in the source <see cref="T:System.Collections.Generic.ICollection`1"/> is greater than the available space from <paramref name="arrayIndex"/> to the end of the destination <paramref name="array"/>.</exception>
		public void CopyTo(T[] array, int arrayIndex) {
			_lock.EnterReadLock();
			try {
				_inner.CopyTo(array, arrayIndex);
			} finally {
				_lock.ExitReadLock();
			}
		}

		/// <summary>
		/// Removes the first occurrence of a specific object from the <see cref="T:System.Collections.Generic.ICollection`1"/>.
		/// </summary>
		/// <returns>
		/// true if <paramref name="item"/> was successfully removed from the <see cref="T:System.Collections.Generic.ICollection`1"/>; otherwise, false. This method also returns false if <paramref name="item"/> is not found in the original <see cref="T:System.Collections.Generic.ICollection`1"/>.
		/// </returns>
		/// <param name="item">The object to remove from the <see cref="T:System.Collections.Generic.ICollection`1"/>.</param><exception cref="T:System.NotSupportedException">The <see cref="T:System.Collections.Generic.ICollection`1"/> is read-only.</exception>
		public bool Remove(T item) {
			_lock.EnterWriteLock();
			try {
				return _inner.Remove(item);
			} finally {
				_lock.ExitWriteLock();
			}
		}

		/// <summary>
		/// Gets the number of elements contained in the <see cref="T:System.Collections.Generic.ICollection`1"/>.
		/// </summary>
		/// <returns>
		/// The number of elements contained in the <see cref="T:System.Collections.Generic.ICollection`1"/>.
		/// </returns>
		public int Count {
			get {
				_lock.EnterReadLock();
				try {
					return _inner.Count;
				} finally {
					_lock.ExitReadLock();
				}
			}
		}

		/// <summary>
		/// Gets a value indicating whether the <see cref="T:System.Collections.Generic.ICollection`1"/> is read-only.
		/// </summary>
		/// <returns>
		/// true if the <see cref="T:System.Collections.Generic.ICollection`1"/> is read-only; otherwise, false.
		/// </returns>
		public bool IsReadOnly { get { return false; } }

	}
}
