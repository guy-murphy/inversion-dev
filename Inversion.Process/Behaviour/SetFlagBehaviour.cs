using System.Collections.Generic;

namespace Inversion.Process.Behaviour
{
    public class SetFlagBehaviour : PrototypedBehaviour
    {
        public SetFlagBehaviour(string respondsTo) : base(respondsTo) {}
        public SetFlagBehaviour(string respondsTo, IPrototype prototype) : base(respondsTo, prototype) {}
        public SetFlagBehaviour(string respondsTo, IEnumerable<IConfigurationElement> config) : base(respondsTo, config) {}

        public override void Action(IEvent ev, IProcessContext context)
        {
            foreach (string name in this.Configuration.GetNames("config", "set"))
            {
                context.Flags.Add(name);
            }
        }
    }
}