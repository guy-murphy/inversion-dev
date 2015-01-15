using System.IO;

using Antlr4.StringTemplate;

using Inversion.Process;
using Inversion.Web.Behaviour.View;

namespace Inversion.StringTemplate.Behaviour.View {
	/// <summary>
	/// A behaviour that will transform the last view step by attempting to find
	/// an appropriate ST4 template, based upon the context params
	/// of *area*, *concern*, and *action*. 
	/// </summary>
	public class StringTemplateViewBehaviour : ViewBehaviour {

		/// <summary>
		/// Creates a new instance of the behaviour, with the default
		/// content type of "text/html".
		/// </summary>
		/// <param name="respondsTo">The name of the behaviour.</param>
		public StringTemplateViewBehaviour(string respondsTo) : this(respondsTo, "text/html") { }
		/// <summary>
		/// Creates a new instance of the behaviour.
		/// </summary>
		/// <param name="respondsTo">The name of the behaviour.</param>
		/// <param name="contentType">The content type of the view step produced from this behaviour.</param>
		public StringTemplateViewBehaviour(string respondsTo, string contentType)
			: base(respondsTo, contentType) {}
		
		/// <summary>
		/// Implementors should impliment this behaviour with the desired action
		/// for their behaviour.
		/// </summary>
		/// <param name="ev">The event to consult.</param>
		/// <param name="context">The context upon which to perform any action.</param>
		public override void Action(IEvent ev, IProcessContext context) {
			if (context.ViewSteps.HasSteps && context.ViewSteps.Last.HasModel) {
				foreach (string templateName in this.GetPossibleTemplates(context, "st")) {
					string templatePath = Path.Combine("Resources", "Views", "ST", templateName);
					if (context.Resources.Exists(templatePath)) {
						string src = context.Resources.ReadAllText(templatePath);
						Template template = new Template(src, '`', '`');
						template.Add("ctx", context);
						template.Add("model", context.ViewSteps.Last.Model);
						string result = template.Render();
						context.ViewSteps.CreateStep(templateName, this.ContentType, result);
					}
				}
			}
		}
	}
}
