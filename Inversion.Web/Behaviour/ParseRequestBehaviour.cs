using System;

using Inversion.Process;

namespace Inversion.Web.Behaviour {

	public class ParseRequestBehaviour : WebBehaviour {

		// TODO: redo this implimentation as it's overly brittle
		// my thinking about both appDirectory and
		// the discovery heuristic is fuzzy

		// the app directory is that portion of the path that is not siginificant to the
		// request but instead represents the root directory of the application
		private readonly string _appDirectory;

		public ParseRequestBehaviour(string name) : this(name, null) { }

		public ParseRequestBehaviour(string name, string appDirectory)
			: base(name) {
			_appDirectory = appDirectory ?? String.Empty;
		}

		public override void Action(IEvent ev, WebContext context) {
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
			// note method
			context.Params["method"] = context.Request.Method;
		}
	}
}
