using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Inversion.Process;
using Inversion.Process.Behaviour;

namespace Inversion.Web.Behaviour.View {
	/// <summary>
	/// A behaviour that will transform the last view step by attempting
	/// to locate a template based on the context parameters of
	/// *area*, *concern* and *action*.
	/// </summary>
	public abstract class ViewBehaviour: ProcessBehaviour {

		private readonly string _contentType;

		/// <summary>
		/// The content type of this behaviours transformation.
		/// </summary>
		protected string ContentType {
			get { return _contentType; }
		}

		/// <summary>
		/// Locates possible templates of the specified extension.
		/// </summary>
		/// <param name="ctx">The context to consult.</param>
		/// <param name="extension">The file extension of templates to look for.</param>
		/// <returns>Returns an enumerable of template names.</returns>
		/// <remarks>
		/// The locations checked are produced by the following series of yields:-
		/// <code>
		///	//area/concern/action
		///	yield return Path.Combine(area, concern, action);
		///	yield return Path.Combine(area, concern, "default.ext");
		///	// area/action
		///	yield return Path.Combine(area, action);
		///	yield return Path.Combine(area, "default.ext");
		///	// concern/action
		///	yield return Path.Combine(concern, action);
		///	yield return Path.Combine(concern, "default.ext");
		///	// action
		///	yield return action;
		///	yield return "default.xslt"; 
		/// </code>
		/// Where ".ext" referes to the extension specified.
		/// </remarks>
		protected virtual IEnumerable<string> GetPossibleTemplates(IProcessContext ctx, string extension) {
			// The iterator generated for this should be
			//		ThreadLocal and therefore safe to use
			//		in this manner on a singleton, would be
			//		nice to fonfirm this.
			// At some point this will need to move to being
			//		and injected strategy, perhaps some ordered expression
			//		of patterns that can be specified as config for the behaviour.
			string area = ctx.Params["area"];
			string concern = ctx.Params["concern"];
			string action = String.Format("{0}.{1}", ctx.Params["action"], extension);
			string defaultName = String.Format("default.{0}", extension);

			// area/concern/action
			yield return Path.Combine(area, concern, action);
			// area/concern/default
			yield return Path.Combine(area, concern, defaultName);
			// area/action
			yield return Path.Combine(area, action);
			// area/default
			yield return Path.Combine(area, defaultName);
			// concern/action
			yield return Path.Combine(concern, action);
			// concern/default
			yield return Path.Combine(concern, defaultName);
			// action
			yield return action;
			// default
			yield return defaultName;
		}

		/// <summary>
		/// Creates a new instance of the behaviour.
		/// </summary>
		/// <param name="respondsTo">The name of the behaviour.</param>
		/// <param name="contentType">The content type of the view step produced from this behaviour.</param>
		protected ViewBehaviour(string respondsTo, string contentType) : base(respondsTo) {
			_contentType = contentType;
		}

	}
}
