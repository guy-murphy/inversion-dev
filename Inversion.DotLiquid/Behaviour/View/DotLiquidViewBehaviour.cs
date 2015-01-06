using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DotLiquid;
using Inversion.Process;
using Inversion.Web;
using Inversion.Web.Behaviour;
using Newtonsoft.Json.Linq;

namespace Inversion.DotLiquid.Behaviour.View {
	public class DotLiquidViewBehaviour : WebBehaviour {

		private readonly string _contentType;

		public DotLiquidViewBehaviour(string respondsTo) : this(respondsTo, "text/html") { }

		public DotLiquidViewBehaviour(string respondsTo, string contentType)
			: base(respondsTo) {
			_contentType = contentType;
		}

		private IEnumerable<string> _possibleTemplates(IWebContext context) {
			string area = context.Params["area"];
			string concern = context.Params["concern"];
			string action = String.Format("{0}.liquid", context.Params["action"]);

			// area/concern/action
			yield return Path.Combine(area, concern, action);
			// area/concern/default
			yield return Path.Combine(area, concern, "default.liquid");
			// area/action
			yield return Path.Combine(area, action);
			// area/default
			yield return Path.Combine(area, "default.liquid");
			// concern/action
			yield return Path.Combine(concern, action);
			// concern/default
			yield return Path.Combine(concern, "default.liquid");
			// action
			yield return action;
			// default
			yield return "default.liquid";
		}

		/// <summary>
		/// Implementors should impliment this behaviour with the desired action
		/// for their behaviour.
		/// </summary>
		/// <param name="ev">The event to consult.</param>
		/// <param name="context">The context upon which to perform any action.</param>
		public override void Action(IEvent ev, IWebContext context) {
			if (context.ViewSteps.HasSteps && context.ViewSteps.Last.HasContent || context.ViewSteps.Last.HasModel) {			
				foreach (string templateName in _possibleTemplates(context)) {
					string templatePath = Path.Combine(context.Application.BaseDirectory, "Resources", "Views", "Liquid", templateName);
					if (File.Exists(templatePath)) {
						string src = File.ReadAllText(templatePath);
						Template template = Template.Parse(src);
						JObject model;
						if (context.ViewSteps.Last.HasModel) {
							model = context.ViewSteps.Last.Model.Data;
						} else if (context.ViewSteps.Last.HasContent) {
							model = JObject.Parse(context.ViewSteps.Last.Content);
						} else {
							model = new JObject(new {errors = new []{"Unable to find any content or model to render."}});
						}
						Hash parms = new Hash {{"model", model}};
						string result = template.Render(parms);
						context.ViewSteps.CreateStep(templateName, _contentType, result);
					}
				}
			}
		}
	}
}
