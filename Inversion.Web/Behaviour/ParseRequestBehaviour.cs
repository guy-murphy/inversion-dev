using System;

using Inversion.Process;

namespace Inversion.Web.Behaviour {

	/// <summary>
	/// Behaviour responsible for deconstructing the request
	/// into a set of conext prameters.
	/// </summary>
	public class ParseRequestBehaviour : WebBehaviour {

		// TODO: redo this implimentation as it's overly brittle
		// my thinking about both appDirectory and
		// the discovery heuristic is fuzzy

		// the app directory is that portion of the path that is not siginificant to the
		// request but instead represents the root directory of the application
		private readonly string _appDirectory;

		/// <summary>
		/// Instantiates a behaviour that decontructs the request into context parameters.
		/// </summary>
		/// <param name="respondsTo">The message that the behaviour will respond to.</param>
		public ParseRequestBehaviour(string respondsTo) : this(respondsTo, null) { }

		/// <summary>
		/// Instantiates a behaviour that decontructs the request into context parameters.
		/// </summary>
		/// <param name="respondsTo">The message that the behaviour will respond to.</param>
		/// <param name="appDirectory">
		/// Configures an application directory to be regarded for the application.
		/// The application directory is that part of the request path that is not
		/// significant to the request but instead represents the root directory of the application.
		/// </param>
		public ParseRequestBehaviour(string respondsTo, string appDirectory)
			: base(respondsTo) {
			_appDirectory = appDirectory ?? String.Empty;
		}

		/// <summary>
		/// Deconstructs the contexts request into a set of prameters for the context.
		/// </summary>
		/// <remarks>
		/// The deafult implementation uses the convention of `/area/concern/action.aspc/tail?querystring`
		/// </remarks>
		/// <param name="ev">The vent that was considered for this action.</param>
		/// <param name="context">The context to act upon.</param>
		public override void Action(IEvent ev, IWebContext context) {
			// eliminate the app directory from the path
			string path = _appDirectory.Length > 0 ? context.Request.UrlInfo.AppPath.Trim('/').Replace(_appDirectory, "") : context.Request.UrlInfo.AppPath;
			path = path.Trim('/');

			if (!String.IsNullOrEmpty(context.Request.UrlInfo.File)) {
				context.Params["action"] = context.Request.UrlInfo.File.Split('.')[0].ToLower();

				string[] parts = path.Split('/');
				if (parts.Length >= 2) {
					context.Params["area"] = parts[parts.Length - 2].ToLower();
					context.Params["concern"] = parts[parts.Length - 1].ToLower();
				} else if (parts.Length == 1) {
					context.Params["area"] = parts[0];
				}
			}

			// import query string and form values
			context.Params.Import(context.Request.Params);
			// establish flags
			foreach (string flag in context.Request.Flags) {
				context.Flags.Add(flag);
			}
			context.Params.Import(context.Request.Headers);
			// note method and tail
			context.Params["method"] = context.Request.Method;
			context.Params["tail"] = context.Request.UrlInfo.Tail;
		}
	}
}
