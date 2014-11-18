using System;
using System.Collections.Generic;
using System.IO;

using RazorEngine;
using RazorEngine.Templating;

using Inversion.Process;
using Inversion.Collections;

namespace Inversion.Web.Behaviour.View {

	/// <summary>
	/// A web behaviour that resolves razor templates to generate views.
	/// </summary>
	/// <remarks>
	/// <para>
	/// Razor isn't getting a lot of attention in Inversion initially, at
	/// some point I'll pay it some attention, but it's really not a priority
	/// as personally I'm not a big fan.
	/// </para>
	/// <para>
	/// Razor is the fast food of templating. It's really tasting
	/// and super-saturated with utility, and it's bad for you.
	/// When rendering a view you really shouldn't be able to
	/// yield side-effects, and you shouldn't be able to consider
	/// anything other than the view you're rendering. In Razor
	/// you can do anything you want. And you will. Especially
	/// when people aren't looking.
	/// </para>
	/// <para>
	/// Worse, you'll start architecting clever helpers, and mappings,
	/// and... you'll start refactoring, and all your templates will
	/// become enmeshed in one glorious front-end monolith.
	/// </para>
	/// <para>
	/// Razor. Just say "no"... Okay, I'm over-egging it a bit.
	/// </para>
	/// <para>
	/// Joking aside, I get why Razor is so popular. It's simple, bendy,
	/// easy for .NET devs to dive into, and you can brute force yourself
	/// out of any situation. It does however in my view encourage
	/// poor practice and blurs an important application layer
	/// so the middle and front of the application risk becoming
	/// quickly enmeshed.
	/// </para>
	/// <para>
	/// Conclave favours XML/XSL, I understand why you
	/// might not, hence <see cref="RazorViewBehaviour"/>.
	/// </para>
	/// </remarks>
	public class RazorViewBehaviour : WebBehaviour {

		private readonly string _contentType;

		/// <summary>
		/// 
		/// </summary>
		/// <param name="message"></param>
		/// <remarks>
		/// This constructor defaults the content type to `text/html`.
		/// </remarks>
		public RazorViewBehaviour(string message) : this(message, "text/html") { }

		/// <summary>
		/// 
		/// </summary>
		/// <param name="message"></param>
		/// <param name="contentType"></param>
		public RazorViewBehaviour(string message, string contentType)
			: base(message) {
			_contentType = contentType;
		}

		// TODO: confirm thread safe
		// The iterator generated for this should be
		//		ThreadLocal and therefore safe to use
		//		in this manner on a singleton, would be
		//		nice to fonfirm this.
		private IEnumerable<string> _possibleTemplates(WebContext context) {
			string area = context.Params["area"];
			string concern = context.Params["concern"];
			string action = String.Format("{0}.cshtml", context.Params["action"]);

			// area/concern/action
			yield return Path.Combine(area, concern, action);
			yield return Path.Combine(area, concern, "default.cshtml");
			// area/action
			yield return Path.Combine(area, action);
			yield return Path.Combine(area, "default.cshtml");
			// action
			yield return action;
			yield return "default.cshtml";

		}

		/// <summary>
		/// Tranforms the last view-step using a razor template.
		/// </summary>
		/// <param name="ev">The vent that was considered for this action.</param>
		/// <param name="context">The context to act upon.</param>
		public override void Action(IEvent ev, WebContext context) {

			if (context.ViewSteps.HasSteps && context.ViewSteps.Last.HasModel) { // we should have a model that we're going to render

				DataDictionary<IData> viewState = context.ViewSteps.Last.Model as DataDictionary<IData>;

				string content = String.Empty; // default output if we can't process a template

				foreach (string templateName in _possibleTemplates(context)) { // check each possible template in turn
					// This is the most immediate way I found to check if a template has been compiled
					//		but we don't actually use the returned template as I can't find how to get a razor context
					//		to run against, when I do this code will probably chage with a possible early bail here.
					ITemplate compiledTemplate = Razor.Resolve(templateName);

					bool compiled = false;
					if (compiledTemplate == null) { // we'll need to look for the template
						string templatePath = Path.Combine(context.Application.BaseDirectory, "Resources", "Views", "Razor", templateName);
						if (File.Exists(templatePath)) {
							string template = File.ReadAllText(templatePath);
							Razor.Compile(template, viewState.GetType(), templateName);
							compiled = true;
						} else {
							continue;
						}
					}
					if (compiledTemplate != null || compiled) {
						content = Razor.Run(templateName, viewState);
					}

					if (!String.IsNullOrEmpty(content)) { // we have content so create the view step and bail
						context.ViewSteps.CreateStep(templateName, _contentType, content);
						break;
					}
				}
			}


		}
	}
}
