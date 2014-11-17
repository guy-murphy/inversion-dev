using System.Collections;
using System.Collections.Generic;
using System.Dynamic;
using System.Xml;

using Newtonsoft.Json;

namespace Inversion.Collections {

	/// <summary>
	/// A <see cref="DynamicObject"/> implementing
	/// an <see cref="IDataDictionary{Data}"/> .
	/// </summary>
	/// <remarks>
	/// <para>
	/// This class is intended to help with exposing models to
	/// Razor templates, as it allows ad hoc properties
	/// to be used as dictionary keys, `model.UserDetails.Name` rather
	/// than `model["UserDetails"].Name`
	/// </para>
	/// <para>
	/// The initial idea was for  the `ControlState` to be
	/// one of these. When I start playing about with Razor
	/// a bit more I'll test it to see if there's any consequences.
	/// </para>
	/// </remarks>

	public class DataModel : DynamicObject, IDataDictionary<IData> {

		private readonly DataDictionary<IData> _backing = new DataDictionary<IData>();

		public override bool TrySetMember(SetMemberBinder binder, object value) {
			IData data = value as IData;
			if (data != null) {
				_backing[binder.Name] = data;
				return true;
			} else {
				return false;
			}
		}

		public override bool TryGetMember(GetMemberBinder binder, out object item) {
			if (_backing.ContainsKey(binder.Name)) {
				item = _backing[binder.Name];
				return true;
			} else {
				item = null;
				return false;
			}
		}

		public IDataDictionary<IData> Import(IEnumerable<KeyValuePair<string, IData>> other) {
			foreach (KeyValuePair<string, IData> entry in other) {
				this.Add(entry);
			}
			return this;
		}


		#region IDictionary<string,IData> Members

		public void Add(string key, IData value) {
			_backing.Add(key, value);
		}

		public bool ContainsKey(string key) {
			return _backing.ContainsKey(key);
		}

		public ICollection<string> Keys {
			get { return _backing.Keys; }
		}

		public bool Remove(string key) {
			return _backing.Remove(key);
		}

		public bool TryGetValue(string key, out IData value) {
			return _backing.TryGetValue(key, out value);
		}

		public ICollection<IData> Values {
			get { return _backing.Values; }
		}

		public IData this[string key] {
			get { return _backing[key]; }
			set { _backing[key] = value; }
		}

		#endregion

		#region ICollection<KeyValuePair<string,IData>> Members

		public void Add(KeyValuePair<string, IData> item) {
			((ICollection<KeyValuePair<string, IData>>)_backing).Add(item);
		}

		public void Clear() {
			_backing.Clear();
		}

		public bool Contains(KeyValuePair<string, IData> item) {
			return ((ICollection<KeyValuePair<string, IData>>)_backing).Contains(item);
		}

		public void CopyTo(KeyValuePair<string, IData>[] array, int arrayIndex) {
			((ICollection<KeyValuePair<string, IData>>)_backing).CopyTo(array, arrayIndex);
		}

		public int Count {
			get { return _backing.Count; }
		}

		public bool IsReadOnly {
			get { return ((ICollection<KeyValuePair<string, IData>>)_backing).IsReadOnly; }
		}

		public bool Remove(KeyValuePair<string, IData> item) {
			return ((ICollection<KeyValuePair<string, IData>>)_backing).Remove(item);
		}

		#endregion

		#region IEnumerable<KeyValuePair<string,IData>> Members

		public IEnumerator<KeyValuePair<string, IData>> GetEnumerator() {
			return ((IEnumerable<KeyValuePair<string, IData>>)_backing).GetEnumerator();
		}

		#endregion

		#region IEnumerable Members

		IEnumerator IEnumerable.GetEnumerator() {
			return ((IEnumerable)_backing).GetEnumerator();
		}

		#endregion

		#region IData Members

		public void ToXml(XmlWriter writer) {
			_backing.ToXml(writer);
		}

		public void ToJson(JsonWriter writer) {
			_backing.ToJson(writer);
		}

		#endregion

		#region ICloneable Members

		public object Clone() {
			return _backing.Clone();
		}

		#endregion
	}
}
