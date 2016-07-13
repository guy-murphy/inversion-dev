using System.Collections.Generic;
using System.Linq;

using Inversion.Collections;
using Inversion.Process;
using Inversion.Process.Behaviour;

namespace Inversion.Web.Behaviour {

	/// <summary>
	/// Constructs the initial view state of the reuqest
	/// as a <see cref="ViewStep"/> composed of the current <see cref="ProcessContext.ControlState"/>.
	/// </summary>
	/// <remarks>
	/// <para>
	/// This is basically a filtering of the <see cref="ProcessContext.ControlState"/> into
	/// the model called the view state, that is going to be rendered. Any item in the control state with a key
	/// starting with the underscore character '_' is regarded as protected, and will
	/// not be copied forward to the view state.
	/// </para>
	/// <para>
	/// If one wished to present a model for render by different means,
	/// or wanted to change how the filtering was done, this is the behaviour
	/// you would swap out for an alternate implementation.
	/// </para>
	/// </remarks>
	public class ViewStateBehaviour : ProcessBehaviour {

		/// <summary>
		/// Instantiates a new view state behaviour configured with the message provided.
		/// </summary>
		/// <param name="respondsTo">The message the behaviour has set as responding to.</param>
		public ViewStateBehaviour(string respondsTo) : base(respondsTo) { }

		/// <summary>
		/// Takes the control state of the provided context and from it produces
		/// a view state model that is used as the basis of the view-step render
		/// pipeline.
		/// </summary>
		/// <remarks>
		/// This is what you'd override if you wanted to govern your own model presented
		/// to your view layer.
		/// </remarks>
		/// <param name="ev">The event that gave rise to this action.</param>
		/// <param name="context">The context within which this action is being performed.</param>
		public override void Action(IEvent ev, IProcessContext context) {
			DataDictionary<IData> model = new DataDictionary<IData>();

			// copy from the context
			model["messages"] = context.Messages;
			model["errors"] = context.Errors;
			model["flags"] = context.Flags;
			model["timers"] = context.Timers;
			model["params"] = new DataDictionary<string>(context.Params.Where(param => !param.Key.StartsWith("_")));

			// copy from the control state
			foreach (KeyValuePair<string, object> entry in context.ControlState) {
				if (!entry.Key.StartsWith("_")) { // exclude "private" items
					if (entry.Value is IData) {
						model[entry.Key] = entry.Value as IData;
					} else {
					    if (entry.Value != null)
					    {
					        model[entry.Key] = new TextData(entry.Value.ToString());
					    }
					}
				}
			}

			
			if (context.HasParams("model-item") && model.ContainsKey(context.Params["model-item"])) {
				context.ViewSteps.CreateStep("view-state", model[context.Params["model-item"]]);
			} else {
				context.ViewSteps.CreateStep("view-state", model);
			}
		}

	}
}
