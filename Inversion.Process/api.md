
## `T:Inversion.Process.Configuration`
Provides an immutable, ordered collection of configuration elements.


### `.#ctor`
Instantiates a new, empty configuration.


### `.#ctor(System.Collections.Generic.IEnumerable{Inversion.Process.Configuration.Element})`
Instantiates a new configuration from the elements provided.

* `elements`: The elements to populate the configuration with.

### `.GetElements(System.String)`
Gets the elements for a specified frame.

* `frame`: The frame to get the elements for.

**returns:** 
Returns an enumerable of the matching elements.


### `.GetElements(System.String,System.String)`
Gets the elements for the specified frame and slot.

* `frame`: The frame to get the elements for.
* `slot`: The slot within a frame to get the elements for.

**returns:** 
Returns an enumerable of the matching elements.


### `.GetElements(System.String,System.String,System.String)`
Gets the elements for the specified frame and slot.

* `frame`: The frame to get the elements for.
* `slot`: The slot within a frame to get the elements for.
* `name`: The name within the slot to get the elements for.

**returns:** 
Returns an enumerable of the matching elements.


### `.GetValue(System.String,System.String,System.String)`
Gets the specified value from the configuration.

* `frame`: The frame of the value.
* `slot`: The slot of the value.
* `name`: The name of the value.

**returns:** 
Returns the value macthing the frame, slot, and name specified.


### `.GetValues(System.String,System.String,System.String)`
Gets the specified values from the configuration.

* `frame`: The frame of the values.
* `slot`: The slot of the values.
* `name`: The name of the values.

**returns:** 
Returns the values matching the frame, slot, and name specified.


### `.GetMap(System.String,System.String)`
Get a map of name/value pairs from the configuration.

* `frame`: The frame of the map.
* `slot`: The slot of the map.

**returns:** 
Returns a map matching the frame and slot specified.


### `.GetNames(System.String,System.String)`
Gets names from the configuration.

* `frame`: The frame of the names.
* `slot`: The slot of the names.

**returns:** 
Returns an enumerable of the names matching the frame and slot specified.


### `.GetSlots(System.String)`
Gets slots from the configuration.

* `frame`: The frame of the slots.

**returns:** 
Returns an enumerable of the slots matching the frame specified.


### `.Has(System.String,System.String,System.String,System.String)`
Determines whether or not the configuration has an element with the frame, slot, name and value specified.

* `frame`: The frame of the element.
* `slot`: The slot of the element.
* `name`: The name of the element.
* `value`: The value of the element.

**returns:** 
Returns true if the configuration containes the specified element; otherwise, returns false.


### `.Has(System.String,System.String,System.String)`
Determines whether or not the configuration has any elements with the frame, slot, and name specified.

* `frame`: The frame of the element.
* `slot`: The slot of the element.
* `name`: The name of the element.

**returns:** 
Returns true if the configuration containes the specified element; otherwise, returns false.


### `.HasAll(System.String,System.String,System.String,System.String[])`
Determines whether or not the configuration has all elements with the frame, slot, name and values specified.

* `frame`: The frame of the element.
* `slot`: The slot of the element.
* `name`: The name of the element.
* `values`: The values of the elements.

**returns:** 
Returns true if the configuration containes all the specified elements; otherwise, returns false.


### `.HasAny(System.String,System.String,System.String,System.String[])`
Determines whether or not the configuration has any of then elements with the frame, slot, name and values specified.

* `frame`: The frame of the element.
* `slot`: The slot of the element.
* `name`: The name of the element.
* `values`: The values of the elements.

**returns:** 
Returns true if the configuration containes any of the specified elements; otherwise, returns false.

### `.Elements`
The elements comprising the configuration.


## `T:Inversion.Process.Configuration.Element`
Models an element of a behaviour configuration.


### `.#ctor(System.Int32,System.String,System.String,System.String,System.String)`
Initializes a new instance of the element class.

* `ordinal`: The order in which this element comes relative to its siblings.
* `frame`: The value of the tuple's first component.
* `slot`: The value of the tuple's second component.
* `name`: The value of the tuple's third component.
* `value`: The value of the tuple's third component.
### `.Ordinal`
The order or position which this element occupies relative to its siblings.

### `.Frame`
The frame of this element.

### `.Slot`
The slot of this element.

### `.Name`
The name of this element.

### `.Value`
The value of this element.


## `T:Inversion.Process.Behaviour.ConfiguredBehaviour`
A behaviour that can be configured.


## `T:Inversion.Process.Behaviour.ProcessBehaviour`
A simple named behaviour with a default condition matching that name againts  [Inversion.Process.IEvent.Message](P-Inversion.Process.IEvent.Message) .


## `T:Inversion.Process.Behaviour.IProcessBehaviour`
The base type for behaviours in Conclave. Behaviours are intended to be registered against a context such as  [Inversion.Process.ProcessContext](T-Inversion.Process.ProcessContext) using `ProcessContext.Register(behaviour)`.

#### Remarks
When events are fired against that context, each behaviour registered will apply it's condition to the  [Inversion.Process.IEvent](T-Inversion.Process.IEvent)  being fired. If this condition returns `true`,             then the context will apply the behaviours `Action` against             the event.

Care should be taken to ensure behaviours are well behaved. To this end the following contract is implied by use of `IProcessBehaviour`:-


##### Example


    context.Register(behaviours);
    context.Fire("set-up");
    context.Fire("process-request");	
    context.Fire("tear-down");
    context.Completed();
    context.Response.ContentType = "text/xml";
    context.Response.Write(context.ControlState.ToXml());


### `.Action(Inversion.Process.IEvent)`
Process an action for the provided  [Inversion.Process.IEvent](T-Inversion.Process.IEvent) .

* `ev`: The event to be processed. 

### `.Preprocess(Inversion.Process.IEvent)`
Perform any processing necessary before the action for this behaviour is processed.

* `ev`: The event that any preprocessing is responding to.

### `.Postprocess(Inversion.Process.IEvent)`
Perform any processing necessary after the action for this behaviour is processed.

* `ev`: The event that any postprocessing is responding to.

### `.Action(Inversion.Process.IEvent,Inversion.Process.IProcessContext)`
Process the action in response to the provided  [Inversion.Process.IEvent](T-Inversion.Process.IEvent) with the  [Inversion.Process.ProcessContext](T-Inversion.Process.ProcessContext)  provided.

* `ev`: The event to process.
* `context`: The context to use.

### `.Condition(Inversion.Process.IEvent)`
The considtion that determines whether of not the behaviours action is valid to run.

* `ev`: The event to consider with the condition.

**returns:** 
`true` if the condition is met; otherwise,  returns  `false`.


### `.Condition(Inversion.Process.IEvent,Inversion.Process.IProcessContext)`
The considtion that determines whether of not the behaviours action is valid to run.

* `ev`: The event to consider with the condition.
* `context`: The context to use.

**returns:** 
`true` if the condition is met; otherwise,  returns  `false`.

### `.RespondsTo`
Gets the message that the behaviour will respond to.


**value:** A `string` value.

### `:Inversion.Process.Behaviour.ProcessBehaviour.#ctor(System.String)`
Creates a new instance of the behaviour.

* `respondsTo`: The name of the behaviour.

### `:Inversion.Process.Behaviour.ProcessBehaviour.Condition(Inversion.Process.IEvent)`
Determines if the event specifies the behaviour by name.

* `ev`: The event to consult.

**returns:** 
Returns true if the  [Inversion.Process.IEvent.Message](P-Inversion.Process.IEvent.Message) is the same as the  [Inversion.Process.Behaviour.ProcessBehaviour.RespondsTo](P-Inversion.Process.Behaviour.ProcessBehaviour.RespondsTo) 

#### Remarks
The intent is to override for bespoke conditions.

### `:Inversion.Process.Behaviour.ProcessBehaviour.Condition(Inversion.Process.IEvent,Inversion.Process.IProcessContext)`
Determines if the event specifies the behaviour by name.

* `ev`: The event to consult.
* `context`: The context to consult.

**returns:** 
Returns true if true if `ev.Message` is the same as `this.Message`

#### Remarks
The intent is to override for bespoke conditions.

### `:Inversion.Process.Behaviour.ProcessBehaviour.Preprocess(Inversion.Process.IEvent)`
Fires a message on the context of the event announcing preprocessing for the event.

#### Remarks
The form the message will take is `"preprocessing::" + ev.Message`, and with parameters from the original message copied over.
* `ev`: The event for which preprocessing should take place.

### `:Inversion.Process.Behaviour.ProcessBehaviour.Postprocess(Inversion.Process.IEvent)`
Fires a message on the context of the event announcing postprocessing for the event.

#### Remarks
The form the message will take is `"postprocessing::" + ev.Message`, and with parameters from the original message copied over.
* `ev`: The event for which postprocessing should take place.

### `:Inversion.Process.Behaviour.ProcessBehaviour.Action(Inversion.Process.IEvent)`
The action to perform when the `Condition(IEvent)` is met.

* `ev`: The event to consult.

### `:Inversion.Process.Behaviour.ProcessBehaviour.Action(Inversion.Process.IEvent,Inversion.Process.IProcessContext)`
The action to perform when the `Condition(IEvent)` is met.

* `ev`: The event to consult.
* `context`: The context upon which to perform any action.
## `P:Inversion.Process.Behaviour.ProcessBehaviour.RespondsTo`
The name the behaviour is known by to the system.


## `T:Inversion.Process.Behaviour.IConfiguredBehaviour`
Described a behaviour that has a configuration.


## `T:Inversion.Process.IConfigured`
Expresses access to structured configuration for a component suitable for querying.

### `.Configuration`
Provices access to component configuration stuiable for querying.


### `:Inversion.Process.Behaviour.ConfiguredBehaviour.#ctor(System.String)`
Creates a new instance of the behaviour with no configuration.

* `respondsTo`: The message the behaviour will respond to.

### `:Inversion.Process.Behaviour.ConfiguredBehaviour.#ctor(System.String,Inversion.Process.Configuration)`
Creates a new instance of the behaviour.

* `respondsTo`: The message the behaviour will respond to.
* `config`: Configuration for the behaviour.

### `:Inversion.Process.Behaviour.ConfiguredBehaviour.#ctor(System.String,System.Collections.Generic.IEnumerable{Inversion.Process.Configuration.Element})`
Creates a new instance of the behaviour.

* `respondsTo`: The message the behaviour will respond to.
* `config`: Configuration for the behaviour.
## `P:Inversion.Process.Behaviour.ConfiguredBehaviour.Configuration`
Exposes the configuration of the behaviour for querying.


## `T:Inversion.Process.Behaviour.MatchingBehaviour`
Extends `ConfiguredBehaviour` with an implemented `Condition` intended to act upont he behaviours configuration to match with state expressed on the process context.


### `.#ctor(System.String)`
Creates a new instance of the behaviour with no configuration.

* `respondsTo`: The message the behaviour will respond to.

### `.#ctor(System.String,Inversion.Process.Configuration)`
Creates a new instance of the behaviour.

* `respondsTo`: The message the behaviour will respond to.
* `config`: Configuration for the behaviour.

### `.#ctor(System.String,System.Collections.Generic.IEnumerable{Inversion.Process.Configuration.Element})`
Creates a new instance of the behaviour.

* `respondsTo`: The message the behaviour will respond to.
* `config`: Configuration for the behaviour.

### `.Condition(Inversion.Process.IEvent,Inversion.Process.IProcessContext)`
Determines if the event specifies the behaviour by name.

* `ev`: The event to consult.
* `context`: The context to consult.

**returns:** 
Returns true if true if the configured parameters for the behaviour match the current context.

#### Remarks
The intent is to override for bespoke conditions.

## `T:Inversion.Process.Behaviour.BehaviourConditionPredicates`
Extensions provided for ``IApplicationBehaioour` providing basic checks performed in behaviour conditions.


### `.EventHasAllParams(Inversion.Process.Behaviour.IConfiguredBehaviour,Inversion.Process.IEvent)`
Determines whether or not the parameters  specified exist in the current event.

* `self`: The behaviour to act upon.
* `ev`: The event to consult.

**returns:** 
Returns true if all the parameters exist; otherwise return false.


### `.EventMatchesAllParamValues(Inversion.Process.Behaviour.IConfiguredBehaviour,Inversion.Process.IEvent)`
Determines whether or not all the key-value pairs provided exist in the events parameters.

* `self`: The behaviour to act upon.
* `ev`: The event to consult.

**returns:** 
Returns true if all the key-value pairs specified exists in the events parameters; otherwise returns false.


### `.ContextHasAllParams(Inversion.Process.Behaviour.IConfiguredBehaviour,Inversion.Process.IProcessContext)`
Determines whether or not the parameters  specified exist in the current context.

* `self`: The behaviour to act upon.
* `ctx`: The context to consult.

**returns:** 
Returns true if all the parameters exist; otherwise return false.


### `.ContextMacthesAllParamValues(Inversion.Process.Behaviour.IConfiguredBehaviour,Inversion.Process.IProcessContext)`
Determines whether or not all the key-value pairs provided exist in the contexts parameters.

* `self`: The behaviour to act upon.
* `ctx`: The context to consult.

**returns:** 
Returns true if all the key-value pairs specified exists in the contexts parameters; otherwise returns false.


### `.ContextExcludes(Inversion.Process.Behaviour.IConfiguredBehaviour,Inversion.Process.IProcessContext)`
Determines whether or not all the key-value pairs provided are NOT in the contexts parameters.

* `self`: The behaviour to act upon.
* `ctx`: The context to consult.

**returns:** 
Returns true if all the key-value pairs specified are absent in the contexts parameters; otherwise returns false.


### `.ContextHasAllControlStates(Inversion.Process.Behaviour.IConfiguredBehaviour,Inversion.Process.IProcessContext)`
Dtermines whether or not the control state has entries indexed under the keys provided.

* `self`: The behaviour to act upon.
* `ctx`: The context to consult.

**returns:** 
Returns true if all the specified keys exist in the control state; otherwise returns false.


### `.ContextExcludesControlState(Inversion.Process.Behaviour.IConfiguredBehaviour,Inversion.Process.IProcessContext)`
Determines whether or not all the key-value pairs provided are NOT in the contexts control state.

* `self`: The behaviour to act upon.
* `ctx`: The context to consult.

**returns:** 
Returns true if all the key-value pairs specified are absent in the contexts parameters; otherwise returns false.


### `.ContextHasAllFlags(Inversion.Process.Behaviour.IConfiguredBehaviour,Inversion.Process.IProcessContext)`
Determines whether or not each of the specified is set on the context.

* `self`: The behaviour to act upon.
* `ctx`: The context to consult.

**returns:** 
Returns true is all flags are set on the context; otherwise, returns false.


## `T:Inversion.Process.Behaviour.MessageTraceBehaviour`
A simple behaviour to wire up to test the simplest possible output.


### `.#ctor(System.String)`
Creates a new instance of the behaviour with no configuration.

* `respondsTo`: The message the behaviour will respond to.

### `.#ctor(System.String,Inversion.Process.Configuration)`
Creates a new instance of the behaviour.

* `respondsTo`: The message the behaviour will respond to.
* `config`: Configuration for the behaviour.

### `.#ctor(System.String,System.Collections.Generic.IEnumerable{Inversion.Process.Configuration.Element})`
Creates a new instance of the behaviour.

* `respondsTo`: The message the behaviour will respond to.
* `config`: Configuration for the behaviour.

### `.Action(Inversion.Process.IEvent,Inversion.Process.IProcessContext)`
The action to perform when the `Condition(IEvent)` is met.

* `ev`: The event to consult.
* `context`: The context upon which to perform any action.

## `T:Inversion.Process.Behaviour.ParameterisedSequenceBehaviour`
A behaviour concerned with driving the processing of a sequence of messages.


### `.#ctor(System.String,Inversion.Process.Configuration)`
Creates a new instance of the behaviour.

* `respondsTo`: The message the behaviour will respond to.
* `config`: Configuration for the behaviour.

### `.#ctor(System.String,System.Collections.Generic.IEnumerable{Inversion.Process.Configuration.Element})`
Creates a new instance of the behaviour.

* `respondsTo`: The message the behaviour will respond to.
* `config`: Configuration for the behaviour.

### `.Action(Inversion.Process.IEvent,Inversion.Process.IProcessContext)`
The action to perform when the `Condition(IEvent)` is met.

* `ev`: The event to consult.
* `context`: The context upon which to perform any action.

## `T:Inversion.Process.DataCollectionEx`
Extension methods acting upon `IDataCollection{ErrorMessage}` objects.


### `.CreateMessage(Inversion.Collections.IDataCollection{Inversion.Process.ErrorMessage},System.String)`
Creates a new error message and adds it to the collection.

* `self`: The collection to add the message to.
* `message`: The human readable error message.

**returns:** 
Returns the error message object that was created.


### `.CreateMessage(Inversion.Collections.IDataCollection{Inversion.Process.ErrorMessage},System.String,System.Object[])`
Creates a new error message and adds it to the collection.

* `self`: The collection to add the message to.
* `message`: The human readable error message as text for string formatting.
* `parms`: Paramters for formatting the message text.

**returns:** 
Returns the error message object that was created.


## `T:Inversion.Process.ErrorMessage`
Represents an error message that occurred during application processing that may be suitable for presenting in any user agent.


### `.System#ICloneable#Clone`
Clones a new error message as a copy of this one.


**returns:** 
The newly cloned error message.


### `.Clone`
Clones a new error message as a copy of this one.


**returns:** 
The newly cloned error message.


### `.#ctor(System.String)`
Instantiates a new error message.

* `message`: The human readable message.

### `.#ctor(System.String,System.Exception)`
Instantiates a new error message.

* `message`: The human readable message.
* `err`: The exception that gave rise to this error.

### `.ToXml(System.Xml.XmlWriter)`
Produces an xml representation of the model.

* `writer`: The writer to used to write the xml to. 

### `.ToJson(Newtonsoft.Json.JsonWriter)`
Produces a json respresentation of the model.

* `writer`: The writer to use for producing json.

### `.ToString`
Provides a string representation of this error message.


**returns:** 
Returns a new string representing this error message.

### `.Data`
Provides an abstract representation of the objects data expressed as a JSON object.

#### Remarks
For this type the json object is only created the once.
### `.Message`
A human readable message summarising the error.

### `.Exception`
The exception if any that gave rise to this error.


## `T:Inversion.Process.Event`
Represents an event occuring in the system.

#### Remarks
Exactly what "event" means is application specific and can range from imperative to reactive.

## `T:Inversion.Process.IEvent`
Represents an event occuring in the system.

#### Remarks
Exactly what "event" means is application specific and can range from imperative to reactive.

### `.Add(System.String,System.String)`
Adds a key-value pair as a parameter to the event.

* `key`: The key of the parameter.
* `value`: The value of the parameter.

### `.HasParams(System.String[])`
Determines whether or not the parameters  specified exist in the event.

* `parms`: The parameters to check for.

**returns:** 
Returns true if all the parameters exist; otherwise return false.


### `.HasParams(System.Collections.Generic.IEnumerable{System.String})`
Determines whether or not the parameters  specified exist in the event.

* `parms`: The parameters to check for.

**returns:** 
Returns true if all the parameters exist; otherwise return false.


### `.HasParamValues(System.Collections.Generic.IEnumerable{System.Collections.Generic.KeyValuePair{System.String,System.String}})`
Determines whether or not all the key-value pairs provided exist in the events parameters.

* `match`: The key-value pairs to check for.

**returns:** 
Returns true if all the key-value pairs specified exists in the events parameters; otherwise returns false.


### `.HasRequiredParams(System.String[])`
Determines whether or not each of the prameters specified exist on the event, and creates an error for each one that does not.

* `parms`: The paramter names to check for.

**returns:** 
Returns true if each of the parameters exist on the event; otherwise returns false.


### `.Fire`
Fires the event on the context to which it is bound.


**returns:** 
Returns the event that has just been fired.

### `.Item(System.String)`
Provides indexed access to the events parameters.

* `key`: The key of the parameter to look up.

**returns:** 
Returns the parameter indexed by the key.

### `.Message`
The simple message the event represents.

#### Remarks
Again, exactly what this means is application specific.
### `.Params`
The parameters for this event represented as key-value pairs.

### `.Context`
The context upon which this event is being fired.

#### Remarks
And event always belongs to a context.
### `.Object`
Any object that the event may be carrying.

#### Remarks
This is a dirty escape hatch, and can even be used as a "return" value for the event.

### `:Inversion.Process.Event.#ctor(Inversion.Process.ProcessContext,System.String,System.Collections.Generic.IDictionary{System.String,System.String})`
Instantiates a new event bound  to a context.

* `context`: The context to which the event is bound.
* `message`: The simple message the event represents.
* `parameters`: The parameters of the event.

### `:Inversion.Process.Event.#ctor(Inversion.Process.ProcessContext,System.String,Inversion.IData,System.Collections.Generic.IDictionary{System.String,System.String})`
Instantiates a new event bound  to a context.

* `context`: The context to which the event is bound.
* `message`: The simple message the event represents.
* `obj`: An object being carried by the event.
* `parameters`: The parameters of the event.

### `:Inversion.Process.Event.#ctor(Inversion.Process.ProcessContext,System.String,System.String[])`
Instantiates a new event bound  to a context.

* `context`: The context to which the event is bound.
* `message`: The simple message the event represents.
* `parms`: A sequnce of context parameter names that should be copied from the context to this event.

### `:Inversion.Process.Event.#ctor(Inversion.Process.ProcessContext,System.String,Inversion.IData,System.String[])`
Instantiates a new event bound  to a context.

* `context`: The context to which the event is bound.
* `message`: The simple message the event represents.
* `obj`: An object being carried by the event.
* `parms`: A sequnce of context parameter names that should be copied from the context to this event.

### `:Inversion.Process.Event.#ctor(Inversion.Process.IEvent)`
Instantiates a new event as a copy of the event provided.

* `ev`: The event to copy for this new instance.

### `:Inversion.Process.Event.System#ICloneable#Clone`
Creates a clone of this event by copying it into a new instance.


**returns:** 
The newly cloned event.


### `:Inversion.Process.Event.Clone`
Creates a clone of this event by copying it into a new instance.


**returns:** 
The newly cloned event.


### `:Inversion.Process.Event.Add(System.String,System.String)`
Adds a key-value pair as a parameter to the event.

* `key`: The key of the parameter.
* `value`: The value of the parameter.

### `:Inversion.Process.Event.Fire`
Fires the event on the context to which it is bound.


**returns:** 
Returns the event that has just been fired.


### `:Inversion.Process.Event.HasParams(System.String[])`
Determines whether or not the parameters  specified exist in the event.

* `parms`: The parameters to check for.

**returns:** 
Returns true if all the parameters exist; otherwise return false.


### `:Inversion.Process.Event.HasParams(System.Collections.Generic.IEnumerable{System.String})`
Determines whether or not the parameters  specified exist in the event.

* `parms`: The parameters to check for.

**returns:** 
Returns true if all the parameters exist; otherwise return false.


### `:Inversion.Process.Event.HasParamValues(System.Collections.Generic.IEnumerable{System.Collections.Generic.KeyValuePair{System.String,System.String}})`
Determines whether or not all the key-value pairs provided exist in the events parameters.

* `match`: The key-value pairs to check for.

**returns:** 
Returns true if all the key-value pairs specified exists in the events parameters; otherwise returns false.


### `:Inversion.Process.Event.HasRequiredParams(System.String[])`
Determines whether or not each of the prameters specified exist on the event, and creates an error for each one that does not.

* `parms`: The paramter names to check for.

**returns:** 
Returns true if each of the parameters exist on the event; otherwise returns false.


### `:Inversion.Process.Event.ToString`
Creates a string representation of the event.


**returns:** 
Returns a new string representing the event.


### `:Inversion.Process.Event.GetEnumerator`
Obtains an enumerator for the events parameters.


**returns:** 
Returns an enumerator suitable for iterating through the events parameters.


### `:Inversion.Process.Event.System#Collections#IEnumerable#GetEnumerator`
Obtains an enumerator for the events parameters.


**returns:** 
Returns an enumerator suitable for iterating through the events parameters.


### `:Inversion.Process.Event.ToXml(System.Xml.XmlWriter)`
Produces an xml representation of the model.

* `xml`: The writer to used to write the xml to. 

### `:Inversion.Process.Event.ToJson(Newtonsoft.Json.JsonWriter)`
Produces a json respresentation of the model.

* `json`: The writer to use for producing json.

### `:Inversion.Process.Event.FromXml(Inversion.Process.ProcessContext,System.String)`
Creates a new event from an xml representation.

* `context`: The context to which the new event will be bound.
* `xml`: The xml representation of an event.

**returns:** 
Returns a new event.


### `:Inversion.Process.Event.FromJson(Inversion.Process.ProcessContext,System.String)`
Creates a new event from an json representation.

* `context`: The context to which the new event will be bound.
* `json`: The json representation of an event.

**returns:** 
Returns a new event.

## `P:Inversion.Process.Event.Item(System.String)`
Provides indexed access to the events parameters.

* `key`: The key of the parameter to look up.

**returns:** 
Returns the parameter indexed by the key.

## `P:Inversion.Process.Event.Message`
The simple message the event represents.

#### Remarks
Again, exactly what this means is application specific.
## `P:Inversion.Process.Event.Params`
The parameters for this event represented as key-value pairs.

## `P:Inversion.Process.Event.Context`
The context upon which this event is being fired.

#### Remarks
And event always belongs to a context.
## `P:Inversion.Process.Event.Object`
Any object that the event may be carrying.

#### Remarks
This is a dirty escape hatch, and can even be used as a "return" value for the event.
## `P:Inversion.Process.Event.Data`
Provides an abstract representation of the objects data expressed as a JSON object.

#### Remarks
For this type the json object is created each time it is accessed.

## `T:Inversion.Process.Event.ParseException`
An exception thrown when unable to parse the xml or json representations for creating a new event.


### `.#ctor(System.String)`
Instantiates a new parse exception with a human readable message.

* `message`: The human readable message for the exception.

### `.#ctor(System.String,System.Exception)`
instantiates a new exception wrapping a provided inner exception, with a human readable message.

* `message`: The human readable message for the exception.
* `err`: The inner exception to wrap.

## `T:Inversion.Process.IProcessContext`
Provides a processing context as a self-contained and sufficient channel of application execution. The context manages a set of behaviours and mediates between them and the outside world.

#### Remarks
The process context along with the `IBehaviour` objects registered on its bus *are* Inversion. Everything else is chosen convention about how those behaviours interact with each other via the context.

### `.Register(Inversion.Process.Behaviour.IProcessBehaviour)`
Registers a behaviour with the context ensuring it is consulted for each event fired on this context.

* `behaviour`: The behaviour to register with this context.

### `.Register(System.Collections.Generic.IEnumerable{Inversion.Process.Behaviour.IProcessBehaviour})`
Registers a whole bunch of behaviours with this context ensuring each one is consulted when an event is fired on this context.

* `behaviours`: The behaviours to register with this context.

### `.Register(System.Predicate{Inversion.Process.IEvent},System.Action{Inversion.Process.IEvent,Inversion.Process.IProcessContext})`
Creates and registers a runtime behaviour with this context constructed  from a predicate representing the behaviours condition, and an action representing the behaviours action. This behaviour will be consulted for any event fired on this context.

* `condition`: The predicate to use as the behaviours condition.
* `action`: The action to use as the behaviours action.

### `.Fire(Inversion.Process.IEvent)`
Fires an event on the context. Each behaviour registered with context is consulted in no particular order, and for each behaviour that has a condition that returns true when applied to the event, that behaviours action is executed.

* `ev`: The event to fire on this context.

**returns:** 



### `.Fire(System.String)`
Constructs a simple event, with a simple string message and fires it on this context.

* `message`: The message to assign to the event.

**returns:** 
Returns the event that was constructed and fired on this context.


### `.Fire(System.String,System.Collections.Generic.IDictionary{System.String,System.String})`
Constructs an event using the message specified, and using the dictionary provided to populate the parameters of the event. This event is then fired on this context.

* `message`: The message to assign to the event.
* `parms`: The parameters to populate the event with.

**returns:** 
Returns the event that was constructed and fired on this context.


### `.FireWith(System.String,System.String[])`
Contructs an event with the message specified, using the supplied parameter keys to copy parameters from the context to the constructed event. This event is then fired on this context.

* `message`: The message to assign to the event.
* `parms`: The parameters to copy from the context.

**returns:** 
Returns the event that was constructed and fired on this context.

### `.Services`
Exposes the processes service container.

### `.ObjectCache`
Provsion of a simple object cache for the context.

#### Remarks
This really needs replaced with our own interface that we control. This isn't portable.
### `.Messages`
Messages intended for user feedback.

#### Remarks
This is a poor mechanism for localisation, and may need to be treated as tokens by the front end to localise.
### `.Errors`
Error messages intended for user feedback.

#### Remarks
This is a poor mechanism for localisation, and may need to be treated as tokens by the front end to localise.
### `.Timers`
A dictionary of named timers.

#### Remarks
`ProcessTimer` is only intended for informal timings, and it not intended for proper metrics.
### `.ViewSteps`
Gives access to a collection of view steps that will be used to control the render pipeline for this context.

### `.ControlState`
Gives access to the current control state of the context. This is the common state that behaviours share and that provides the end state or result of a contexts running process.

### `.Flags`
Flags for the context available to behaviours as shared state.

### `.Params`
The parameters of the contexts execution available to behaviours as shared state.


## `T:Inversion.Process.IServiceContainer`
Represent the contract of a simple service container from which services may be ontained by name. This interface focused on consuming services from a container and does not speak to the configuration or management of a container.


### `.GetService`{T1}(System.String)`
Gets the service if any of the provided name and type.

`T`: The type of the service being obtained.
* `name`: The name of the service to obtain.

**returns:** 
Returns the service of the specified name.


### `.ContainsService(System.String)`
Determines if the container has a service of a specified name.

* `name`: The name of the service to check for.

**returns:** 
Returns true if the service exists; otherwise returns false.


## `T:Inversion.Process.ProcessContext`
Provides a processing context as a self-contained and sufficient channel of application execution. The context manages a set of behaviours and mediates between them and the outside world.

#### Remarks
The process context along with the `IBehaviour` objects registered on its bus *are* Inversion. Everything else is chosen convention about how those behaviours interact with each other via the context.

### `.#ctor(Inversion.Process.IServiceContainer)`
Instantiates a new process contrext for inversion.

#### Remarks
You can think of this type here as "being Inversion". This is the thing.
* `services`: The service container the context will use.

### `.Finalize`
Desrtructor for the type.


### `.Dispose`
Releases all resources maintained by the current context instance.


### `.Dispose(System.Boolean)`
Disposal that allows for partitioning of  clean-up of managed and unmanaged resources.

* `disposing`: 
#### Remarks
This is looking conceited and should probably be removed. I'm not even sure I can explain a use case for it in terms of an Inversion context.

### `.Register(Inversion.Process.Behaviour.IProcessBehaviour)`
Registers a behaviour with the context ensuring it is consulted for each event fired on this context.

* `behaviour`: The behaviour to register with this context.

### `.Register(System.Collections.Generic.IEnumerable{Inversion.Process.Behaviour.IProcessBehaviour})`
Registers a whole bunch of behaviours with this context ensuring each one is consulted when an event is fired on this context.

* `behaviours`: The behaviours to register with this context.

### `.Register(System.Predicate{Inversion.Process.IEvent},System.Action{Inversion.Process.IEvent,Inversion.Process.IProcessContext})`
Creates and registers a runtime behaviour with this context constructed  from a predicate representing the behaviours condition, and an action representing the behaviours action. This behaviour will be consulted for any event fired on this context.

* `condition`: The predicate to use as the behaviours condition.
* `action`: The action to use as the behaviours action.

### `.Fire(Inversion.Process.IEvent)`
Fires an event on the context. Each behaviour registered with context is consulted in no particular order, and for each behaviour that has a condition that returns true when applied to the event, that behaviours action is executed.

* `ev`: The event to fire on this context.

**returns:** 



### `.Fire(System.String)`
Constructs a simple event, with a simple string message and fires it on this context.

* `message`: The message to assign to the event.

**returns:** 
Returns the event that was constructed and fired on this context.


### `.Fire(System.String,System.Collections.Generic.IDictionary{System.String,System.String})`
Constructs an event using the message specified, and using the dictionary provided to populate the parameters of the event. This event is then fired on this context.

* `message`: The message to assign to the event.
* `parms`: The parameters to populate the event with.

**returns:** 
Returns the event that was constructed and fired on this context.


### `.FireWith(System.String,System.String[])`
Contructs an event with the message specified, using the supplied parameter keys to copy parameters from the context to the constructed event. This event is then fired on this context.

* `message`: The message to assign to the event.
* `parms`: The parameters to copy from the context.

**returns:** 
Returns the event that was constructed and fired on this context.


### `.Completed`
Instructs the context that operations have finished, and that while it may still be consulted no further events will be fired.


### `.ToString`
Provides a string representation of the context and its current state.


**returns:** 
Returns a string representation of the context.

### `.Services`
Exposes the processes service container.

### `.Bus`
The event bus of the process.

### `.ObjectCache`
Provsion of a simple object cache for the context.

#### Remarks
This really needs replaced with our own interface that we control. This isn't portable.
### `.Messages`
Messages intended for user feedback.

#### Remarks
This is a poor mechanism for localisation, and may need to be treated as tokens by the front end to localise.
### `.Errors`
Error messages intended for user feedback.

#### Remarks
This is a poor mechanism for localisation, and may need to be treated as tokens by the front end to localise.
### `.Timers`
A dictionary of named timers.

#### Remarks
`ProcessTimer` is only intended for informal timings, and it not intended for proper metrics.
### `.ViewSteps`
Gives access to a collection of view steps that will be used to control the render pipeline for this context.

### `.ControlState`
Gives access to the current control state of the context. This is the common state that behaviours share and that provides the end state or result of a contexts running process.

### `.Flags`
Flags for the context available to behaviours as shared state.

### `.Params`
The parameters of the contexts execution available to behaviours as shared state.


## `T:Inversion.Process.ProcessContextEx`
Extension methods for `IProcessContext` concerned with performing checks upon that context.


### `.IsFlagged(Inversion.Process.IProcessContext,System.String)`
Determines whether or not the flag of the specified key exists.

* `flag`: The key of the flag to check for.
* `self`: The context being acted upon.

**returns:** 
Returns true if the flag exists; otherwise returns false.


### `.HasParams(Inversion.Process.IProcessContext,System.String[])`
Determines whether or not the parameters  specified exist in the current context.

* `parms`: The parameters to check for.
* `self`: The context being acted upon.

**returns:** 
Returns true if all the parameters exist; otherwise return false.


### `.HasParams(Inversion.Process.IProcessContext,System.Collections.Generic.IEnumerable{System.String})`
Determines whether or not the parameters  specified exist in the current context.

* `parms`: The parameters to check for.
* `self`: The context being acted upon.

**returns:** 
Returns true if all the parameters exist; otherwise return false.


### `.HasParamValue(Inversion.Process.IProcessContext,System.String,System.String)`
Determines whether or not the parameter name and value specified exists in the current context.

* `name`: The name of the parameter to check for.
* `value`: The value of the parameter to check for.
* `self`: The context being acted upon.

**returns:** 
Returns true if a parameter with the name and value specified exists in this conext; otherwise returns false.


### `.HasParamValues(Inversion.Process.IProcessContext,System.Collections.Generic.IEnumerable{System.Collections.Generic.KeyValuePair{System.String,System.String}})`
Determines whether or not all the key-value pairs provided exist in the contexts parameters.

* `match`: The key-value pairs to check for.
* `self`: The context being acted upon.

**returns:** 
Returns true if all the key-value pairs specified exists in the contexts parameters; otherwise returns false.


### `.HasParamValues(Inversion.Process.IProcessContext,System.Collections.Generic.IEnumerable{System.Collections.Generic.KeyValuePair{System.String,System.Collections.Generic.IEnumerable{System.String}}})`
Determines whether or not all any of the values for their associated parameter name exist in the contexts parameters.

* `match`: The possible mapped values to match against.
* `self`: The context being acted upon.

**returns:** 
Returns if each of the keys has at least one value that exists for the conext; otherwise, returns false.


### `.HasRequiredParams(Inversion.Process.IProcessContext,System.String[])`
Determines whether or not the specified paramters exist in this context, and produces and error for each one that does not exist.

* `parms`: The parameter keys to check for.
* `self`: The context being acted upon.

**returns:** 
Returns true if all the paramter keys are present; otherwise returns false.


### `.HasControlState(Inversion.Process.IProcessContext,System.String[])`
Dtermines whether or not the control state has entries indexed under the keys provided.

* `parms`: The keys to check for in the control state.
* `self`: The context being acted upon.

**returns:** 
Returns true if all the specified keys exist in the control state; otherwise returns false.


### `.HasControlState(Inversion.Process.IProcessContext,System.Collections.Generic.IEnumerable{System.String})`
Dtermines whether or not the control state has entries indexed under the keys provided.

* `parms`: The keys to check for in the control state.
* `self`: The context being acted upon.

**returns:** 
Returns true if all the specified keys exist in the control state; otherwise returns false.


### `.HasRequiredControlState(Inversion.Process.IProcessContext,System.String[])`
Dtermines whether or not the control state has entries indexed under the keys provided, and creates an error for each one that doesn't.

* `parms`: The keys to check for in the control state.
* `self`: The context being acted upon.

**returns:** 
Returns true if all the specified keys exist in the control state; otherwise returns false.


### `.ParamOrDefault(Inversion.Process.IProcessContext,System.String,System.String)`
Obtains the context prarameter for the specified key, or if it doesn't exist uses the default value specified.

* `key`: The key of the context parameter to use.
* `defaultValue`: The value to use if the parameter doesn't exist.
* `self`: The context being acted upon.

**returns:** 
Returns the specified parameter if it exists; otherwise returns false.


## `T:Inversion.Process.ProcessException`
An exception that is thrown when a problem is encountered in the Inversion processing model.


### `.#ctor(System.String)`
Instantiates a new process exception with the message provided.

* `message`: A simple human readable message that summarises this exceptions cause.

### `.#ctor(System.Runtime.Serialization.SerializationInfo,System.Runtime.Serialization.StreamingContext)`
instantiates a new process exception with the details needed to handle serialisation and deserialisation.

* `info`: The info needed to handle the serialisation of this exception.
* `context`: The context used to manage the serialisation stream.

## `T:Inversion.Process.ProcessTimer`
Represents a simplistic timer as a start and stop time as a pair of date time objects.

#### Remarks
This is **NOT** suitable for adult timings, but introduces no overhead and is suitable for applications developers to be able to monitor basic timings of features to know if features are costing more time that expected or are going into distress.

### `.#ctor`
Instances a new process timer, defaulting its start time as now.


### `.#ctor(System.DateTime)`
Instantiates a new process timer with the start time specified.

* `start`: The start time of the new timer.

### `.#ctor(Inversion.Process.ProcessTimer)`
Instantiates a new process timer as a copy of a provied timer.

* `timer`: 

### `.System#ICloneable#Clone`
Clones this timer as a copy.


**returns:** 
Returns a new process timer.


### `.Clone`
Clones this timer as a copy.


**returns:** 
Returns a new process timer.


### `.Begin`
Sets the start time of this timer to now.


**returns:** 
Returns this timer.


### `.End`
Sets the stop time of this timer to now.


**returns:** 
Returns this process timer.


### `.ToXml(System.Xml.XmlWriter)`
Produces an xml representation of the model.

* `writer`: The writer to used to write the xml to. 

### `.ToJson(Newtonsoft.Json.JsonWriter)`
Produces a json respresentation of the model.

* `writer`: The writer to use for producing json.
### `.HasStopped`
Determines if this timer has been stopped or not.

### `.Start`
The start time of the timer.

### `.Stop`
The stop time of the timer.

### `.Duration`
The time that has elapsed between the start and the stop times.

### `.Data`
Provides an abstract representation of the objects data expressed as a JSON object.

#### Remarks
For this type the json object is created each time it is accessed.

## `T:Inversion.Process.ProcessTimerDictionary`
A simple dictionary that contains and helps control process timer instances.


### `.Begin(System.String)`
Create and start a new timer of the specified name.

* `name`: The name of the new timer.

**returns:** 
Returns the timer that has just been started.


### `.End(System.String)`
Ends the process timer of the corresponding name.

* `name`: The name of the timer to end.

**returns:** 
Returns the process timer that was ended.


### `.TimeAction(System.String,System.Action)`
Creates and starts a new timer of a specified name, starts it, performs the provided action, and then stops the timer.

* `name`: The name of the process timer.
* `action`: The action to perform.

**returns:** 
Returns the process timer that was run.


## `T:Inversion.Process.Behaviour.RuntimeBehaviour`
A behaviour that facilitates creating behaviours whose conditions and actions are assigned at runtime not compile time.


### `.#ctor(System.String)`
Instantiates a new runtime behaviour.

* `respondsTo`: The name by which the behaviour is known to the system.

### `.#ctor(System.String,System.Predicate{Inversion.Process.IEvent},System.Action{Inversion.Process.IEvent,Inversion.Process.IProcessContext})`
Instantiates a new runtime behaviour.

* `respondsTo`: The name by which the behaviour is known to the system.
* `condition`: The predicate that will determine if this behaviours action should be executed.
* `action`: The action that should be performed if this behaviours conditions are met.

### `.Condition(Inversion.Process.IEvent)`
Determines if this behaviours action should be executed in response to the provided event.

* `ev`: The event to consider.

**returns:** 
Returns true if this behaviours action to execute in response to this event; otherwise returns  false.


### `.Action(Inversion.Process.IEvent,Inversion.Process.IProcessContext)`
The action to perform if this behaviours condition is met.

* `ev`: The event to consult.
* `context`: The context upon which to perform any action.

## `T:Inversion.Process.Behaviour.SimpleSequenceBehaviour`
A behaviour concerned with driving the processing of a sequence of messages.

#### Remarks
You can think of behaviour as taking one incoming message and turning it into a sequence of messages.

### `.#ctor(System.String,System.Collections.Generic.IEnumerable{System.String})`
Instantiates a new simple sequence behaviour, configuring it with a sequence as an enumerable.

* `respondsTo`: The message this behaviour should respond to.
* `sequence`: The sequence of simple messages this behaviour should generate.

### `.Action(Inversion.Process.IEvent,Inversion.Process.IProcessContext)`
if the conditions of this behaviour are met it will generate a sequence of configured messages.

* `ev`: The event that gave rise to this action.
* `context`: The context that should be acted apon.

## `T:Inversion.Process.ViewStep`
Represents a step in a rendering view pipeline.

#### Remarks
A step can either have  [Inversion.Process.ViewStep.Content](P-Inversion.Process.ViewStep.Content)  or             a  [Inversion.Process.ViewStep.Model](P-Inversion.Process.ViewStep.Model) , but not both.

### `.#ctor(System.String,System.String,System.String)`
Creates a new instance of a step with the parameters provided.

* `name`: Human readable name of the step.
* `contentType`: The type of the steps content.
* `content`: The actual content of the step.

### `.#ctor(System.String,Inversion.IData)`
Creates a new instance of a step with the parameters provided.

* `name`: The human readable name of the step.
* `model`: The actual model of the step.
### `.Name`
The human readable name of the step.

### `.ContentType`
The content type of the  [Inversion.Process.ViewStep.Content](P-Inversion.Process.ViewStep.Content) if there is any.

### `.Content`
The content if any of the step.

### `.Model`
The model if any of the step.

### `.HasContent`
Determines whether or not the step has any content.

### `.HasModel`
Determines whether or not the step has a model.


## `T:Inversion.Process.ViewSteps`
A collection of view step objects representing the steps taken in a view generating pipeline.

#### Remarks
This is currently badly implemented as a concurrent stack, and needs to change. This needs to become a synchronised collection.

### `.Dispose`
Releases all reasources currently being used by this instance of view steps.


### `.CreateStep(System.String,System.String,System.String)`
Creates a new view step and pushes it onto the stack of view steps to be processed.

* `name`: The name of the view step to be created.
* `contentType`: The content type that the new view step represents.
* `content`: The actual content of the new view step.

### `.CreateStep(System.String,Inversion.IData)`
Creates a new view step and pushes it onto the stack of view steps to be processed.

* `name`: The name of the view step to be created.
* `model`: The actual model of the new view step.
### `.HasSteps`
Determines whether or not there are any steps.

### `.Last`
The last step in the current pipeline.

