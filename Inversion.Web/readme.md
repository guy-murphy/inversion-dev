`Inversion.Web`, project notes
## `T:Inversion.Web.Behaviour.BehaviourConditionPredicates`
Extensions provided for ``WebBehaviour` providing basic checks performed in behaviour conditions.


### `.HasAnyUserRoles(Inversion.Web.Behaviour.WebBehaviour,Inversion.Web.WebContext)`
Determines whether or not the current context user is in any of the `required-user-roles` configured for this behaviour.

* `self`: The behaviour to act upon.
* `ctx`: The context to consult.

**returns:** 
Returns true if the current context user is in any of the  `required-user-roles` configured for this behaviour.


## `T:Inversion.Web.Behaviour.BootstrapBehaviour`
A behaviour responsible for boostrapping the request processing. Out of the box it simply imports the prameters configured for this behaviour into the contexts params, so can be seen as a way to configure a context with default prameters. It should be see as a point of extensibility for setting up the default state of a context prior to processing a request.


## `T:Inversion.Web.Behaviour.WebBehaviour`
An abstract provision of basic web-centric features for process behaviours being used in a web application.


## `T:Inversion.Web.Behaviour.IWebBehaviour`
An specification of basic web-centric features for process behaviours being used in a web application.


### `.Action(Inversion.Process.IEvent,Inversion.Web.WebContext)`
The action to perform if this behaviours condition is met.

* `ev`: The event to consult.
* `context`: The context upon which to perform any action.

### `:Inversion.Web.Behaviour.WebBehaviour.#ctor(System.String,System.Collections.Generic.IDictionary{System.String,System.Collections.Generic.IEnumerable{System.String}},System.Collections.Generic.IDictionary{System.String,System.Collections.Generic.IDictionary{System.String,System.String}},System.Collections.Generic.IDictionary{System.String,System.Collections.Generic.IDictionary{System.String,System.Collections.Generic.IEnumerable{System.String}}})`
Creates a new instance of the behaviour.

* `message`: The name of the behaviour.
* `namedLists`: Named lists used to configure this behaviour.
* `namedMaps`: Named maps used to configure this behaviour.
* `namedMappedLists`: Named maps of lists used to configure this behaviour.

### `:Inversion.Web.Behaviour.WebBehaviour.Condition(Inversion.Process.IEvent)`
Determines if this behaviours action should be executed in response to the provided event.

* `ev`: The event to consider.

**returns:** 
Returns true if this behaviours action to execute in response to this event; otherwise returns  false.


### `:Inversion.Web.Behaviour.WebBehaviour.Condition(Inversion.Process.IEvent,Inversion.Web.WebContext)`
Determines if this behaviours action should be executed in response to the provided event and context.

* `ev`: The event to consider.
* `context`: The context to consider.

**returns:** 



### `:Inversion.Web.Behaviour.WebBehaviour.Action(Inversion.Process.IEvent)`
The action to perform if this behaviours condition is met.

* `ev`: The event to consult.

### `:Inversion.Web.Behaviour.WebBehaviour.Action(Inversion.Process.IEvent,Inversion.Process.ProcessContext)`
The action to perform if this behaviours condition is met.

* `ev`: The event to consult.
* `context`: The context upon which to perform any action.

### `:Inversion.Web.Behaviour.WebBehaviour.Action(Inversion.Process.IEvent,Inversion.Web.WebContext)`
Implementors should impliment this behaviour with the desired action for their behaviour.

* `ev`: The event to consult.
* `context`: The context upon which to perform any action.

### `:Inversion.Web.Behaviour.BootstrapBehaviour.#ctor(System.String,System.Collections.Generic.IEnumerable{System.Collections.Generic.KeyValuePair{System.String,System.String}})`
Instantiates a new bootstrap behaviour configured with the key-value pairs provided as parameters.

* `message`: The message this behaviour should respond to.
* `parms`: The key value-pairs to configure as parameters for this behaviour.

### `:Inversion.Web.Behaviour.BootstrapBehaviour.Action(Inversion.Process.IEvent,Inversion.Web.WebContext)`
If the conditions of this behaviour are met, copies the parameters configured for this behaviour into the context parameters.

* `ev`: The event that gave rise to this action.
* `context`: The context within which this action is being performed.
## `P:Inversion.Web.Behaviour.BootstrapBehaviour.Parameters`
Gives access to the prameters configured for the bootstrap behaviour that should be copied into the context params early in the request life-cycle.


## `T:Inversion.Web.Behaviour.ParseRequestBehaviour`
Behaviour responsible for deconstructing the request into a set of conext prameters.


### `.#ctor(System.String)`
Instantiates a behaviour that decontructs the request into context parameters.

* `message`: The message that the behaviour will respond to.

### `.#ctor(System.String,System.String)`
Instantiates a behaviour that decontructs the request into context parameters.

* `message`: The message that the behaviour will respond to.
* `appDirectory`: Configures an application directory to be regarded for the application. The application directory is that part of the request path that is not significant to the request but instead represents the root directory of the application.

### `.Action(Inversion.Process.IEvent,Inversion.Web.WebContext)`
Deconstructs the contexts request into a set of prameters for the context.

#### Remarks
The deafult implementation uses the convention of `/area/concern/action.aspc/tail?querystring`
* `ev`: The vent that was considered for this action.
* `context`: The context to act upon.

## `T:Inversion.Web.Behaviour.ProcessViewsBehaviour`
Behaviour responsible for driving the view pipeline expressed as view-steps.


### `.#ctor(System.String)`
Instantiates a new behaviour responsible for processes the inversion view pipeline.

* `message`: The message that the behaviour will respond to.

### `.Action(Inversion.Process.IEvent,Inversion.Web.WebContext)`
Iterates over each view-step object for the provided context and fires the event for that viiews processing. This is a driving behaviour.

* `ev`: The vent that was considered for this action.
* `context`: The context to act upon.

## `T:Inversion.Web.Behaviour.RenderBehaviour`
Controls the writting of results from the last view step in the view pipeline to the response stream.

#### Remarks
This behaviour is not responsible for producing or transforming views, merely the writting of results. Consult  [Inversion.Web.Behaviour.ProcessViewsBehaviour](T-Inversion.Web.Behaviour.ProcessViewsBehaviour) for the actually processing of views.



### `.#ctor(System.String)`
Creates a new instance of a render behaviour.

* `name`: The name of the render behaviour.
Throws an exception is there is no view step to actually render.
### `.Action(Inversion.Process.IEvent,Inversion.Web.WebContext)`
## `T:Inversion.Web.Behaviour.ViewStateBehaviour`
Constructs the initial view state of the reuqest as a  [Inversion.Process.ViewStep](T-Inversion.Process.ViewStep)  composed of the current  [Inversion.Process.ProcessContext.ControlState](P-Inversion.Process.ProcessContext.ControlState) .

#### Remarks
This is basically a filtering of the  [Inversion.Process.ProcessContext.ControlState](P-Inversion.Process.ProcessContext.ControlState)  into             the model called the view state, that is going to be rendered. Any item in the control state with a key             starting with the underscore character '_' is regarded as protected, and will             not be copied forward to the view state.

If one wished to present a model for render by different means, or wanted to change how the filtering was done, this is the behaviour you would swap out for an alternate implementation.



### `.#ctor(System.String)`
Instantiates a new view state behaviour configured with the message provided.

* `message`: The message the behaviour has set as responding to.

### `.Action(Inversion.Process.IEvent,Inversion.Web.WebContext)`
Takes the control state of the provided context and from it produces a view state model that is used as the basis of the view-step render pipeline.

#### Remarks
This is what you'd override if you wanted to govern your own model presented to your view layer.
* `ev`: The event that gave rise to this action.
* `context`: The context within which this action is being performed.

## `T:Inversion.Web.Behaviour.View.JsonViewBehaviour`
Serialise the model of the last view step to json.


### `.#ctor(System.String)`
Instantiates a new xml view behaviour to provide production of json views.

#### Remarks
Defaults the content-type to "application/json"
* `message`: The message the behaviour has set as responding to.

### `.#ctor(System.String,System.String)`
Instantiates a new xml view behaviour to provide production of json views.

* `message`: The message the behaviour has set as responding to.
* `contentType`: The content type of the view step produced from this behaviour.

### `.Action(Inversion.Process.IEvent,Inversion.Web.WebContext)`
Writes the model of the last view-step as json to the content of a new view-step.

* `ev`: The event that gave rise to this action.
* `context`: The context within which this action is being performed.

## `T:Inversion.Web.Behaviour.View.RazorViewBehaviour`
A web behaviour that resolves razor templates to generate views.

#### Remarks
Razor isn't getting a lot of attention in Inversion initially, at some point I'll pay it some attention, but it's really not a priority as personally I'm not a big fan.

Razor is the fast food of templating. It's really tasting and super-saturated with utility, and it's bad for you. When rendering a view you really shouldn't be able to yield side-effects, and you shouldn't be able to consider anything other than the view you're rendering. In Razor you can do anything you want. And you will. Especially when people aren't looking.

Worse, you'll start architecting clever helpers, and mappings, and... you'll start refactoring, and all your templates will become enmeshed in one glorious front-end monolith.

Razor. Just say "no"... Okay, I'm over-egging it a bit.

Joking aside, I get why Razor is so popular. It's simple, bendy, easy for .NET devs to dive into, and you can brute force yourself out of any situation. It does however in my view encourage poor practice and blurs an important application layer so the middle and front of the application risk becoming quickly enmeshed.

Conclave favours XML/XSL, I understand why you might not, hence  [Inversion.Web.Behaviour.View.RazorViewBehaviour](T-Inversion.Web.Behaviour.View.RazorViewBehaviour) .



### `.#ctor(System.String)`


* `message`: 
#### Remarks
This constructor defaults the content type to `text/html`.

### `.#ctor(System.String,System.String)`


* `message`: 
* `contentType`: 

### `.Action(Inversion.Process.IEvent,Inversion.Web.WebContext)`
Tranforms the last view-step using a razor template.

* `ev`: The vent that was considered for this action.
* `context`: The context to act upon.

## `T:Inversion.Web.Behaviour.View.XmlViewBehaviour`
Serialise the model of the last view step to XML.


### `.#ctor(System.String)`
Instantiates a new xml view behaviour to provide production of xml views.

#### Remarks
Defaults the content-type to "text/xml"
* `message`: The message the behaviour has set as responding to.

### `.#ctor(System.String,System.String)`
Instantiates a new xml view behaviour to provide production of xml views.

* `message`: The message the behaviour has set as responding to.
* `contentType`: The content type of the view step produced from this behaviour.

### `.Action(Inversion.Process.IEvent,Inversion.Web.WebContext)`
Writes the model of the last view-step as xml to the content of a new view-step.

* `ev`: The event that gave rise to this action.
* `context`: The context within which this action is being performed.

## `T:Inversion.Web.Behaviour.View.XsltViewBehaviour`
A behaviour that will transform the last view step by attempting to find an appropriate XSL style sheet, based upon the context params of *area*, *concern*, and *action*. 

#### Remarks
This is intended for use in Web application, not as a general purpose XSL transform.

### `.#ctor(System.String)`
Instantiates a new xslt view behaviour used to provide xslt templating primarily for web applications.

* `message`: The message the behaviour has set as responding to.
#### Remarks
Defaults to caching compiled xslt, to a content type of "text/xml".

### `.#ctor(System.String,System.String)`
Instantiates a new xslt view behaviour used to provide xslt templating primarily for web applications.

* `message`: The message the behaviour has set as responding to.
* `contentType`: The content type of the view step produced from this behaviour.
#### Remarks
Defaults to caching compiled xslt.

### `.#ctor(System.String,System.String,System.Boolean)`
Instantiates a new xslt view behaviour used to provide xslt templating primarily for web applications.

* `message`: The message the behaviour has set as responding to.
* `contentType`: The content type of the view step produced from this behaviour.
* `enableCache`: Specifies whether or not the xslt compilation should be cached.

### `.#ctor(System.String,System.Boolean)`
Instantiates a new xslt view behaviour used to provide xslt templating primarily for web applications.

* `message`: The message the behaviour has set as responding to.
* `enableCache`: Specifies whether or not the xslt compilation should be cached.
#### Remarks
Defaults to a content type of "text/xml".

### `.Action(Inversion.Process.IEvent,Inversion.Web.WebContext)`
Takes the content of the last view-step and transforms it with the xslt with the location that best matches the path of the url. 

#### Remarks
The locations checked are produced by the following series of yields:-

    //area/concern/action
    yield return Path.Combine(area, concern, action);
    yield return Path.Combine(area, concern, "default.xslt");
    // area/action
    yield return Path.Combine(area, action);
    yield return Path.Combine(area, "default.xslt");
    // concern/action
    yield return Path.Combine(concern, action);
    yield return Path.Combine(concern, "default.xslt");
    // action
    yield return action;
    yield return "default.xslt"; 

* `ev`: The event that gave rise to this action.
* `context`: The context within which this action is being performed.

## `T:Inversion.Web.IInversionHandler`
A base handler for Conclave.


### `.ProcessRequest(Inversion.Web.WebContext)`
Process the current request with the provided `WebContext`.

* `context`: The `WebContext` being used for the current request.

## `T:Inversion.Web.UrlInfo`
Represent the structure of a url.


### `F:Inversion.Web.UrlInfo.DefaultRegex`
The default regex that is used to deconstruct urls.


### `.#ctor(System.Uri)`
Instantiates a new url-info object from the uri object provided.

* `uri`: The uri object to contrsut the url-info from.

### `.#ctor(System.String)`
Instantiates a new url-info object from the url string representation provided.

* `url`: The url to construct the url-info from.

### `.#ctor(System.Uri,System.Text.RegularExpressions.Regex)`
Instantiates a new url-info object from the uri provided using the regex provided to deconstruct it.

* `uri`: The uri object to contrsut the url-info from.
* `regex`: The regex to use in deconstructing the uri.

### `.#ctor(System.String,System.Text.RegularExpressions.Regex)`
Instantiates a new url-info object from the url string representation provided, using the regex provided to deconstruct it.

* `url`: The url to construct the url-info from.
* `regex`: The regex to use in deconstructing the uri.

### `.#ctor(Inversion.Web.UrlInfo)`
Instantiates a url-info object as a copy of the url-info object provided.

* `info`: The url-info obect to create a copy from.

### `.ProcessUrl`
Processes the url with a deconstruction regex.


### `.Clone`
Instantiates a new url-info object that is a copy of the current instance.


**returns:** 


### `.Regex`
The regular expression being used to break URLs down into their parts.

#### Remarks
If a regular expression isn't provided via the contructor then the default  [Inversion.Web.UrlInfo.DefaultRegex](F-Inversion.Web.UrlInfo.DefaultRegex)  is used.
### `.Match`
The matches produced by matching the [Inversion.Web.UrlInfo.Regex](P-Inversion.Web.UrlInfo.Regex)  against the  [Inversion.Web.UrlInfo.Url](P-Inversion.Web.UrlInfo.Url) .

### `.Url`
The url being processed.

### `.Protocol`
The protocol specified by the  [Inversion.Web.UrlInfo.Url](P-Inversion.Web.UrlInfo.Url) 

### `.Domain`
The domain specified by the  [Inversion.Web.UrlInfo.Url](P-Inversion.Web.UrlInfo.Url) 

### `.FullPath`
The full path specified by the  [Inversion.Web.UrlInfo.Url](P-Inversion.Web.UrlInfo.Url) 

### `.AppPath`
The application path specified by the  [Inversion.Web.UrlInfo.Url](P-Inversion.Web.UrlInfo.Url) 

### `.AppUrl`
The URL of the current application.

### `.File`
The file specified by the  [Inversion.Web.UrlInfo.Url](P-Inversion.Web.UrlInfo.Url) 

### `.Extension`
The extension specified by the  [Inversion.Web.UrlInfo.Url](P-Inversion.Web.UrlInfo.Url) 

### `.Tail`
The tail specified by the  [Inversion.Web.UrlInfo.Url](P-Inversion.Web.UrlInfo.Url) 

### `.QueryString`
The query string specified by the  [Inversion.Web.UrlInfo.Url](P-Inversion.Web.UrlInfo.Url) 

### `.Query`
A name / value dictionary as the propduct of parsing the  [Inversion.Web.UrlInfo.QueryString](P-Inversion.Web.UrlInfo.QueryString) 


## `T:Inversion.Web.WebApplication`
Represents a running web application application.


### `.#ctor`
Instantiates a new web application, defaulting to the base directory of the current app domain.

### `.BaseDirectory`
The base directory from which the application is running.


## `T:Inversion.Web.WebContext`
Extends the process context with web specific information about an individual request being processed.

#### Remarks
The context object is threaded through the whole stack and provides a controled pattern and workflow of state, along with access to resources and services external to the application. Everything hangs off the context.

### `.#ctor(System.Web.HttpContext,Inversion.Process.IServiceContainer)`
Instantiates a new context object purposed for Web applications.

* `underlyingContext`: The underlying http context to wrap.
* `services`: The service container the context will use.
### `.UnderlyingContext`
The underlying http context that is being wrapped by this web context.

### `.Application`
Provides access to the running web application to which this context belongs.

### `.Response`
Gives access to the web response of this context.

### `.Request`
Gives access to the web request for this context.

### `.Cache`
Gives access to the cache being used for this context.

### `.User`
Gives access to the `IPrinciple` user object that represents the current user for this context.


## `T:Inversion.Web.WebException`
An exception that is thrown when an general error occurs within a web application that would correspond to a http status code.


### `.#ctor(System.String)`
Instantiates a new web exception with the message provided.

* `message`: The message to be output if the exception is unhandled.
#### Remarks
Defaults the status code to 500.

### `.#ctor(System.Net.HttpStatusCode,System.String)`
Instantiates a new web exception with the status code and message provided.

* `status`: The status code that should be produced for this error if it is unhandled.
* `message`: The message that should be produced for this error if it is unhandled.
### `.Status`
The http status code that should be produced for this error if it is unhandled and recovered from.


## `T:Inversion.Web.WebRequest`
Provides a wrapper for the underlying web request for application developers to use.

#### Remarks
This wrapping is mindful of providing a common interface that can port to other platforms. Along with providing a point of extensibility and control.

### `.#ctor(System.Web.HttpContext)`
Instantiates a new web request by wrapping the http request of the http context provided.

* `context`: The http context from which to obtain the http request to wrap.

### `.#ctor(System.Web.HttpRequest)`
Instantiates a new web request wrapping the http request provided.

* `request`: The underlying http request to wrap.
### `.UnderlyingRequest`
The underlying http request being wrapped.

### `.Files`
Gives access to any files uploaded by the user agent as part of this request.

### `.UrlInfo`
Gives access to a url-info object that provides info about the structure of the url of the request.

### `.Method`
The http method of the request.

### `.IsGet`
Returns true if the http method of this request is GET; otherwise returns false.

### `.IsPost`
Returns true if the http method of this request is POST; otherwise returns false.

### `.Params`
Provides access to the request parameters from both the querystring and those that are posted.

#### Remarks
First params are read from the querystring and then those posted which will override any from the querystring.
### `.Payload`
Gives access to the payload if any of the request.

### `.Flags`
Gives access to any flags present in the querystring.

#### Remarks
Any querystring parameter that is a single value rather that a key-value pair is regarded as a flag.
### `.Headers`
Gives access to the headers of the reuqest.

### `.Cookies`
Gives access to the request cookies.


## `T:Inversion.Web.WebResponse`
Provides a wrapper of the underlying http response for application developers to use.

#### Remarks
This wrapping is mindful of providing a common interface that can port to other platforms. Along with providing a point of extensibility and control.

### `.#ctor(System.Web.HttpContext)`
Instantiates a new web response by wrapping the http response of the http context provided.

* `context`: The http context from which to obtain the http response to wrap.

### `.#ctor(System.Web.HttpResponse)`
Instantiates a new web response wrapping the http response provided.

* `underlyingResponse`: The underlying http response to wrap.

### `.End`
Flushes the response steam and ends the response.


### `.Write(System.String)`
Writes the provided text to the response stream.

* `text`: The text to write to the response stream.

### `.WriteFormat(System.String,System.Object[])`
Writes the provided formatted text to the response stream.

* `text`: The text to write to the response stream.
* `args`: The arguments to interpolate into the text.

### `.Redirect(System.String)`
Redirects the request to the provided url.

* `url`: The url to redirect to.

### `.PermanentRedirect(System.String)`
Redirects the request permanently to the provided url issuing a `301` in the response.

* `url`: 
### `.UnderlyingResponse`
The underlying http response being wrapped.

### `.Output`
The text writer used for writing to the response stream.

### `.OutputStream`
The response stream.

### `.StatusCode`
The status code of the response.

### `.StatusDescription`
The status description of the response stream.

### `.ContentType`
The content type of the response stream.

### `.Cookies`
Access to the response cookies.

