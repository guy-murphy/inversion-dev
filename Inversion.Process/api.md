
## `T:Inversion.Process.IProcessBehaviour`
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

### `.Action(Inversion.Process.IEvent,Inversion.Process.ProcessContext)`
process and action for the provided  [Inversion.Process.IEvent](T-Inversion.Process.IEvent) with the  [Inversion.Process.ProcessContext](T-Inversion.Process.ProcessContext)  provided.

* `ev`: The event to process.
* `context`: The context to use.

### `.Condition(Inversion.Process.IEvent)`
The considtion that determines whether of not the behaviours action is valid to run.

* `ev`: The event to consider with the condition.

**returns:** 
`true` if the condition is met; otherwise,  returns  `false`.

### `.Message`
Gets the message that the behaviour will respond to.


**value:** A `string` value.

## `T:Inversion.Process.ProcessBehaviour`
A simple named behaviour with a default condition matching that name againts  [Inversion.Process.IEvent.Message](P-Inversion.Process.IEvent.Message) .


### `.#ctor(System.String)`
Creates a new instance of the behaviour.

* `message`: The name of the behaviour.

### `.Condition(Inversion.Process.IEvent)`
Determines if the event specifies the behaviour by name.

* `ev`: The event to consult.

**returns:** 
Returns true if the  [Inversion.Process.IEvent.Message](P-Inversion.Process.IEvent.Message) is the same as the  [Inversion.Process.ProcessBehaviour.Message](P-Inversion.Process.ProcessBehaviour.Message) 

#### Remarks
The intent is to override for bespoke conditions.

### `.Condition(Inversion.Process.IEvent,Inversion.Process.ProcessContext)`
Determines if the event specifies the behaviour by name.

* `ev`: The event to consult.
* `context`: The context to consult.

**returns:** 
Returns true if true if `ev.Message` is the same as `this.Message`

#### Remarks
The intent is to override for bespoke conditions.

### `.Action(Inversion.Process.IEvent)`
The action to perform when the `Condition(IEvent)` is met.

* `ev`: The event to consult.

### `.Action(Inversion.Process.IEvent,Inversion.Process.ProcessContext)`
The action to perform when the `Condition(IEvent)` is met.

* `ev`: The event to consult.
* `context`: The context upon which to perform any action.
## `P:Inversion.Process.ProcessContext.Services`
Exposes the processes service container.

## `P:Inversion.Process.ProcessContext.Bus`
The event bus of the process.

## `P:Inversion.Process.ProcessContext.Messages`
Messages intended for user feedback.

#### Remarks
This is a poor mechanism for localisation, and may need to be treated as tokens by the front end to localise.
## `P:Inversion.Process.ProcessContext.Errors`
Error messages intended for user feedback.

#### Remarks
This is a poor mechanism for localisation, and may need to be treated as tokens by the front end to localise.
## `P:Inversion.Process.ProcessContext.Timers`
A dictionary of named timers.

#### Remarks
`ProcessTimer` is only intended for informal timings, and it not intended for proper metrics.
## `P:Inversion.Process.ProcessContext.ViewSteps`



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
A collection of `ViewStep` objects representing the steps taken in a view generating pipeline.

#### Remarks
This is currently badly implemented as a concurrent stack, and needs to change. This needs to become a synchronised collection.
### `.HasSteps`
Determines whether or not there are any steps.

### `.Last`
The last step in the current pipeline.

