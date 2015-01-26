using System.Collections.Generic;
using Inversion.Data;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using Inversion.Naiad;
using Inversion.Process.Behaviour;

namespace Inversion.Process.Tests.Behaviour {
	[TestClass]
	public class SelectionCriteriaTests {


		private IEnumerable<IProcessContext> _getContexts() {
			yield return new ProcessContext(ServiceContainer.Instance, FileSystemResourceAdapter.Instance);
			yield return new SynchronizedProcessContext(ServiceContainer.Instance, FileSystemResourceAdapter.Instance);
		}

		[TestMethod]
		public void RespondsTo() {
			foreach (IProcessContext context in _getContexts()) {

				context.Register(new TestBehaviour(
					respondsTo: "*", 
					action: (ev, ctx) => ctx.Flags.Add("any-hit"))
				);
				context.Register(new TestBehaviour(
					respondsTo: "test-message", 
					action: (ev, ctx) => ctx.Flags.Add("test-hit"))
				);
				context.Register(new TestBehaviour(
					respondsTo: "never-message", 
					action: (ev, ctx) => ctx.Flags.Add("never-hit"))
				);
				context.Register(
					condition: (ev) => ev.Message.Contains("left") && ev.Message.Contains("field"), 
					action: (ev, ctx) => ctx.Flags.Add("left-field")
				);
				
				context.Fire("test-message");
				Assert.IsTrue(context.IsFlagged("any-hit"));
				Assert.IsTrue(context.IsFlagged("test-hit"));
				Assert.IsFalse(context.IsFlagged("never-hit"));
				Assert.IsFalse(context.IsFlagged("left-field"));

				context.Flags.Clear();

				context.Fire("different-message");
				Assert.IsTrue(context.IsFlagged("any-hit"));
				Assert.IsFalse(context.IsFlagged("test-hit"));
				Assert.IsFalse(context.IsFlagged("never-hit"));
				Assert.IsFalse(context.IsFlagged("left-field"));

				context.Flags.Clear();

				context.Fire("left-field");
				Assert.IsTrue(context.IsFlagged("any-hit"));
				Assert.IsFalse(context.IsFlagged("test-hit"));
				Assert.IsFalse(context.IsFlagged("never-hit"));
				Assert.IsTrue(context.IsFlagged("left-field"));

				context.Flags.Clear();

				context.Fire("fielding-left");
				Assert.IsTrue(context.IsFlagged("any-hit"));
				Assert.IsFalse(context.IsFlagged("test-hit"));
				Assert.IsFalse(context.IsFlagged("never-hit"));
				Assert.IsTrue(context.IsFlagged("left-field"));
			}
		}


		[TestMethod]
		public void EventHasAllParams() {
			// event has
			foreach (IProcessContext context in _getContexts()) {
				IPrototypedBehaviour behaviour = new TestBehaviour("test",
					new Configuration.Builder {
						{"event", "has", "p1"},
						{"event", "has", "p2"},
						{"event", "has", "p3"}
					}
				);

				Event ev = new Event(context, "test") {
					{"p1", "v1"},
					{"p2", "v2"},
					{"p3", "v3"}
				};

				// positive
				Assert.IsTrue(behaviour.Condition(ev));
				// negative
				ev.Params.Remove("p2");
				Assert.IsFalse(behaviour.Condition(ev));
			}
		}

		[TestMethod]
		public void EventMatchesAllParamValues() {
			// event match
			foreach (IProcessContext context in _getContexts()) {
				IPrototypedBehaviour behaviour = new TestBehaviour("test",
					new Configuration.Builder {
						{"event", "match", "p1", "v1"},
						{"event", "match", "p2", "v2"},
						{"event", "match", "p3", "v3"}
					}
				);

				Event ev = new Event(context, "test") {
					{"p1", "v1"},
					{"p2", "v2"},
					{"p3", "v3"}
				};

				// positive
				Assert.IsTrue(behaviour.Condition(ev));
				// negative
				ev.Params.Remove("p2");
				Assert.IsFalse(behaviour.Condition(ev));
				ev.Params.Add("p2", "v0");
				Assert.IsFalse(behaviour.Condition(ev));
			}
		}

		[TestMethod]
		public void ContextHasAllParams() {
			// context has
			foreach (IProcessContext context in _getContexts()) {
				context.Params.Add("p1", "v1");
				context.Params.Add("p2", "v2");
				context.Params.Add("p3", "v3");

				IPrototypedBehaviour behaviour = new TestBehaviour("test",
					new Configuration.Builder {
						{"context", "has", "p1"},
						{"context", "has", "p2"},
						{"context", "has", "p3"}
					}
				);

				Event ev = new Event(context, "test");

				// positive
				Assert.IsTrue(behaviour.Condition(ev));
				// negative
				context.Params.Remove("p2");
				Assert.IsFalse(behaviour.Condition(ev));
			}
		}

		[TestMethod]
		public void ContextMacthesAllParamValues() {
			foreach (IProcessContext context in _getContexts()) {
				context.Params.Add("p1", "v1");
				context.Params.Add("p2", "v2");
				context.Params.Add("p3", "v3");

				IPrototypedBehaviour behaviour = new TestBehaviour("test",
					new Configuration.Builder {
						{"context", "match", "p1", "v1"},
						{"context", "match", "p2", "v2"},
						{"context", "match", "p3", "v3"}
					}
				);

				Event ev = new Event(context, "test");

				// positive
				Assert.IsTrue(behaviour.Condition(ev));
				// negative
				context.Params.Remove("p2");
				Assert.IsFalse(behaviour.Condition(ev));
				context.Params.Add("p2", "v0");
				Assert.IsFalse(behaviour.Condition(ev));
			}
		}

		[TestMethod]
		public void ContextMatchesAnyParamValues() {
			foreach (IProcessContext context in _getContexts()) {
				IPrototypedBehaviour behaviour = new TestBehaviour("test",
					new Configuration.Builder {
						{"context", "match-any", "p1", "v1"},
						{"context", "match-any", "p2", "v2"},
						{"context", "match-any", "p3", "v3"}
					}
				);

				Event ev = new Event(context, "test");

				// positive
				context.Params.Add("p1", "v1");
				Assert.IsTrue(behaviour.Condition(ev));
				context.Params.Remove("p1");

				context.Params.Add("p2", "v2");
				Assert.IsTrue(behaviour.Condition(ev));
				context.Params.Remove("p2");

				context.Params.Add("p3", "v3");
				Assert.IsTrue(behaviour.Condition(ev));
				context.Params.Remove("p3");

				// negative

				context.Params.Add("p1", "v2");
				Assert.IsFalse(behaviour.Condition(ev));
				context.Params.Remove("p1");

				context.Params.Add("p4", "v4");
				Assert.IsFalse(behaviour.Condition(ev));
				context.Params.Remove("p4");
			}
		}

		[TestMethod]
		public void ContextExcludes() {
			foreach (IProcessContext context in _getContexts()) {

				IPrototypedBehaviour behaviour = new TestBehaviour("test",
					new Configuration.Builder {
						{"context", "excludes", "p1", "v1"},
						{"context", "excludes", "p2", "v2"},
						{"context", "excludes", "p3", "v3"},
					}
				);

				Event ev = new Event(context, "test");

				// positive
				Assert.IsTrue(behaviour.Condition(ev));
				// negative
				context.Params.Add("p2", "v2");
				Assert.IsFalse(behaviour.Condition(ev));
			}
		}

		[TestMethod]
		public void ContextHasAllControlStates() {
			foreach (IProcessContext context in _getContexts()) {
				context.ControlState["p1"] = "v1";
				context.ControlState["p2"] = "v2";

				IPrototypedBehaviour behaviour = new TestBehaviour("test",
					new Configuration.Builder {
						{"control-state", "has", "p1"},
						{"control-state", "has", "p2"}
					}
				);

				Event ev = new Event(context, "test");

				// positive 
				Assert.IsTrue(behaviour.Condition(ev));
				// negative
				context.ControlState.Remove("p2");
				Assert.IsFalse(behaviour.Condition(ev));
			}
		}

		[TestMethod]
		public void ContextExcludesControlState() {
			foreach (IProcessContext context in _getContexts()) {

				IPrototypedBehaviour behaviour = new TestBehaviour("test",
					new Configuration.Builder {
						{"control-state", "excludes", "p1"},
						{"control-state", "excludes", "p2"}
					}
				);

				Event ev = new Event(context, "test");

				// positive 
				Assert.IsTrue(behaviour.Condition(ev));
				// negative
				context.ControlState["p1"] = "v1";
				Assert.IsFalse(behaviour.Condition(ev));
				context.ControlState.Remove("p1");
				context.ControlState["p2"] = "v2";
				Assert.IsFalse(behaviour.Condition(ev));
			}
		}

		[TestMethod]
		public void ContextHasAllFlags() {
			foreach (IProcessContext context in _getContexts()) {
				context.Flags.Add("f1");
				context.Flags.Add("f2");

				IPrototypedBehaviour behaviour = new TestBehaviour("test",
					new Configuration.Builder {
						{"context", "flagged", "f1", "true"},
						{"context", "flagged", "f2", "true"},
						{"context", "flagged", "f3", "false"}
					}
				);

				Event ev = new Event(context, "test");

				// positive
				Assert.IsTrue(behaviour.Condition(ev));
				// negative
				context.Flags.Add("f3");
				Assert.IsFalse(behaviour.Condition(ev));
				context.Flags.Remove("f3");
				Assert.IsTrue(behaviour.Condition(ev));
				context.Flags.Remove("f1");
				Assert.IsFalse(behaviour.Condition(ev));
			}
		}


	}
}
