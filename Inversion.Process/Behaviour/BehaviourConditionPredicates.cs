namespace Inversion.Process.Behaviour {
	public static class BehaviourConditionPredicates {

		public static bool HasAllParameters(this ApplicationBehaviour self, ProcessContext ctx) {
			return !self.NamedLists.ContainsKey("has-all-params") || ctx.HasParams(self.NamedLists["has-all-params"]);
		}

		public static bool MacthesAllParamValues(this ApplicationBehaviour self, ProcessContext ctx) {
			return !self.NamedMaps.ContainsKey("matching-all-param-values") || ctx.HasParamValues(self.NamedMaps["matching-all-param-values"]);
		}

		public static bool HasAllControlStates(this ApplicationBehaviour self, ProcessContext ctx) {
			return !self.NamedLists.ContainsKey("has-all-control-states") || ctx.HasControlState(self.NamedLists["has-all-control-states"]);
		}
	}
}
