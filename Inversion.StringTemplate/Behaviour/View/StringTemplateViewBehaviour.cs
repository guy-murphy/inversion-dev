using System;
using System.Collections.Generic;
using System.IO;

using Antlr4.StringTemplate;

using Inversion.Process;
using Inversion.Process.Behaviour;

namespace Inversion.StringTemplate.Behaviour.View {
	public class StringTemplateViewBehaviour : ProcessBehaviour {
		private readonly string _contentType;

		public StringTemplateViewBehaviour(string respondsTo) : this(respondsTo, "text/html") { }

		public StringTemplateViewBehaviour(string respondsTo, string contentType)
			: base(respondsTo) {
			_contentType = contentType;
		}

		private IEnumerable<string> _possibleTemplates(IProcessContext context) {
			string area = context.Params["area"];
			string concern = context.Params["concern"];
			string action = String.Format("{0}.st", context.Params["action"]);

			// area/concern/action
			yield return Path.Combine(area, concern, action);
			// area/concern/default
			yield return Path.Combine(area, concern, "default.st");
			// area/action
			yield return Path.Combine(area, action);
			// area/default
			yield return Path.Combine(area, "default.st");
			// concern/action
			yield return Path.Combine(concern, action);
			// concern/default
			yield return Path.Combine(concern, "default.st");
			// action
			yield return action;
			// default
			yield return "default.st";
		}

		/// <summary>
		/// Implementors should impliment this behaviour with the desired action
		/// for their behaviour.
		/// </summary>
		/// <param name="ev">The event to consult.</param>
		/// <param name="context">The context upon which to perform any action.</param>
		public override void Action(IEvent ev, IProcessContext context) {
			if (context.ViewSteps.HasSteps && context.ViewSteps.Last.HasModel) {
				foreach (string templateName in _possibleTemplates(context)) {
					string templatePath = Path.Combine(context.ParamOrDefault("baseDirectory", ""), "Resources", "Views", "ST", templateName);
					if (File.Exists(templatePath)) {
						string src = File.ReadAllText(templatePath);
						Template template = new Template(src, '`', '`');
						template.Add("ctx", context);
						template.Add("model", context.ViewSteps.Last.Model);
						string result = template.Render();
						context.ViewSteps.CreateStep(templateName, _contentType, result);
					}
				}
			}
		}
	}
}
