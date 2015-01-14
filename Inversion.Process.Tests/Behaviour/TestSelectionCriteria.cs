using System.Collections.Generic;
using Inversion.Naiad;
using Inversion.Process.Behaviour;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Inversion.Process.Tests.Behaviour {
	[TestClass]
	public class TestSelectionCriteria {


		private IEnumerable<IProcessContext> _getContexts() {
			yield return new ProcessContext(ServiceContainer.Instance);
			yield return new SynchronizedProcessContext(ServiceContainer.Instance);
		}

		[TestMethod]
		public void RespondsTo() {
			foreach (IProcessContext context in _getContexts()) {

				context.Register(new TestBehaviour("*") {
					Perform = (ev, ctx) => ctx.Flags.Add("any-hit")
				});
				context.Register(new TestBehaviour("test-message") {
					Perform = (ev, ctx) => ctx.Flags.Add("test-hit")
				});
				context.Register(new TestBehaviour("never-message") {
					Perform = (ev, ctx) => ctx.Flags.Add("never-hit")
				});
				
				context.Fire("test-message");
				Assert.IsTrue(context.IsFlagged("any-hit"));
				Assert.IsTrue(context.IsFlagged("test-hit"));
				Assert.IsFalse(context.IsFlagged("never-hit"));

				context.Flags.Clear();

				context.Fire("different-message");
				Assert.IsTrue(context.IsFlagged("any-hit"));
				Assert.IsFalse(context.IsFlagged("test-hit"));
				Assert.IsFalse(context.IsFlagged("never-hit"));
			}
		}


		[TestMethod]
		public void EventHasAllParams() {
			foreach (IProcessContext ctx in _getContexts()) {
				IConfiguredBehaviour behaviour = new TestBehaviour("test",
					new Configuration.Builder {
						{"event", "has", "p1"},
						{"event", "has", "p2"},
						{"event", "has", "p3"}
					}
				);

				Event ev = new Event(ctx, "test") {
					{"p1", "v1"},
					{"p2", "v2"},
					{"p3", "v3"}
				};

				// positive
				Assert.IsTrue(behaviour.EventHasAllParams(ev));
				// negative
				ev.Params.Remove("p2");
				Assert.IsFalse(behaviour.EventHasAllParams(ev));
			}
		}

		[TestMethod]
		public void EventMatchesAllParamValues() {
			foreach (IProcessContext ctx in _getContexts()) {

				IConfiguredBehaviour behaviour = new TestBehaviour("test",
					new Configuration.Builder {
						{"event", "match", "p1", "v1"},
						{"event", "match", "p2", "v2"},
						{"event", "match", "p3", "v3"}
					}
				);

				Event ev = new Event(ctx, "test") {
					{"p1", "v1"},
					{"p2", "v2"},
					{"p3", "v3"}
				};

				// positive
				Assert.IsTrue(behaviour.EventMatchesAllParamValues(ev));
				// negative
				ev.Params.Remove("p2");
				Assert.IsFalse(behaviour.EventMatchesAllParamValues(ev));
				ev.Params.Add("p2", "v0");
				Assert.IsFalse(behaviour.EventMatchesAllParamValues(ev));
			}
		}

		[TestMethod]
		public void ContextHasAllParams() {
			foreach (IProcessContext ctx in _getContexts()) {
				ctx.Params.Add("p1", "v1");
				ctx.Params.Add("p2", "v2");
				ctx.Params.Add("p3", "v3");

				IConfiguredBehaviour behaviour = new TestBehaviour("test",
					new Configuration.Builder {
						{"context", "has", "p1"},
						{"context", "has", "p2"},
						{"context", "has", "p3"}
					}
				);

				// positive
				Assert.IsTrue(behaviour.ContextHasAllParams(ctx));
				// negative
				ctx.Params.Remove("p2");
				Assert.IsFalse(behaviour.ContextHasAllParams(ctx));
			}
		}

		[TestMethod]
		public void ContextMacthesAllParamValues() {
			foreach (IProcessContext ctx in _getContexts()) {
				ctx.Params.Add("p1", "v1");
				ctx.Params.Add("p2", "v2");
				ctx.Params.Add("p3", "v3");

				IConfiguredBehaviour behaviour = new TestBehaviour("test",
					new Configuration.Builder {
						{"context", "match", "p1", "v1"},
						{"context", "match", "p2", "v2"},
						{"context", "match", "p3", "v3"}
					}
				);

				// positive
				Assert.IsTrue(behaviour.ContextMacthesAllParamValues(ctx));
				// negative
				ctx.Params.Remove("p2");
				Assert.IsFalse(behaviour.ContextMacthesAllParamValues(ctx));
				ctx.Params.Add("p2", "v0");
				Assert.IsFalse(behaviour.ContextMacthesAllParamValues(ctx));
			}
		}

		[TestMethod]
		public void ContextExcludes() {
			foreach (IProcessContext ctx in _getContexts()) {

				IConfiguredBehaviour behaviour = new TestBehaviour("test",
					new Configuration.Builder {
						{"context", "excludes", "p1", "v1"},
						{"context", "excludes", "p2", "v2"},
						{"context", "excludes", "p3", "v3"},
					}
				);

				// positive
				Assert.IsTrue(behaviour.ContextExcludes(ctx));
				// negative
				ctx.Params.Add("p2", "v2");
				Assert.IsFalse(behaviour.ContextExcludes(ctx));
			}
		}

		[TestMethod]
		public void ContextHasAllControlStates() {
			foreach (IProcessContext ctx in _getContexts()) {
				ctx.ControlState["p1"] = "v1";
				ctx.ControlState["p2"] = "v2";

				IConfiguredBehaviour behaviour = new TestBehaviour("test",
					new Configuration.Builder {
						{"control-state", "has", "p1"},
						{"control-state", "has", "p2"}
					}
				);

				// positive 
				Assert.IsTrue(behaviour.ContextHasAllControlStates(ctx));
				// negative
				ctx.ControlState.Remove("p2");
				Assert.IsFalse(behaviour.ContextHasAllControlStates(ctx));
			}
		}

		[TestMethod]
		public void ContextExcludesControlState() {
			foreach (IProcessContext ctx in _getContexts()) {

				IConfiguredBehaviour behaviour = new TestBehaviour("test",
					new Configuration.Builder {
						{"control-state", "excludes", "p1"},
						{"control-state", "excludes", "p2"}
					}
				);

				// positive 
				Assert.IsTrue(behaviour.ContextExcludesControlState(ctx));
				// negative
				ctx.ControlState["p1"] = "v1";
				Assert.IsFalse(behaviour.ContextExcludesControlState(ctx));
				ctx.ControlState.Remove("p1");
				ctx.ControlState["p2"] = "v2";
				Assert.IsFalse(behaviour.ContextExcludesControlState(ctx));
			}
		}

		[TestMethod]
		public void ContextHasAllFlags() {
			foreach (IProcessContext ctx in _getContexts()) {
				ctx.Flags.Add("f1");
				ctx.Flags.Add("f2");

				IConfiguredBehaviour behaviour = new TestBehaviour("test",
					new Configuration.Builder {
						{"context", "flagged", "f1", "true"},
						{"context", "flagged", "f2", "true"},
						{"context", "flagged", "f3", "false"}
					}
				);

				// positive
				Assert.IsTrue(behaviour.ContextHasAllFlags(ctx));
				// negative
				ctx.Flags.Add("f3");
				Assert.IsFalse(behaviour.ContextHasAllFlags(ctx));
				ctx.Flags.Remove("f3");
				Assert.IsTrue(behaviour.ContextHasAllFlags(ctx));
				ctx.Flags.Remove("f1");
				Assert.IsFalse(behaviour.ContextHasAllFlags(ctx));
			}
		}


	}
}
