using Inversion.Process;
using Inversion.Process.Behaviour;

namespace Inversion.Web.Behaviour.View {
	/// <summary>
	/// Serialise the model of the last view step to json.
	/// </summary>
	public class TextViewBehaviour : ProcessBehaviour {

		private readonly string _contentType;

        /// <summary>
        /// Instantiates a new view behaviour to provide production of text views.
        /// </summary>
        /// <remarks>Defaults the content-type to "text/plain"</remarks>
        /// <param name="respondsTo">The message the behaviour has set as responding to.</param>
        public TextViewBehaviour(string respondsTo) : this(respondsTo, "text/plain") { }

		/// <summary>
		/// Instantiates a new view behaviour to provide production of text views.
		/// </summary>
		/// <param name="respondsTo">The message the behaviour has set as responding to.</param>
		/// <param name="contentType">The content type of the view step produced from this behaviour.</param>
		public TextViewBehaviour(string respondsTo, string contentType)
			: base(respondsTo) {
			_contentType = contentType;
		}

		/// <summary>
		/// Writes the model of the last view-step as json to the content of a new view-step.
		/// </summary>
		/// <param name="ev">The event that gave rise to this action.</param>
		/// <param name="context">The context within which this action is being performed.</param>
		public override void Action(IEvent ev, IProcessContext context) {
			if (context.ViewSteps.HasSteps) {
				if (context.ViewSteps.Last.HasModel) {
					context.ViewSteps.CreateStep("text", _contentType, ((TextData) context.ViewSteps.Last.Model).Value);
				} else {
					throw new WebException("Was expecting a model to serialise but found content.");
				}
			} else {
				throw new WebException("There is no initial view state to serialise into XML.");
			}
		}

	}
}
