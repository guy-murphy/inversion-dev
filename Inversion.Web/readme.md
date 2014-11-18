`Inversion.Web`, project notes
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



## `T:Inversion.Web.Behaviour.View.JsonViewBehaviour`
Serialise the model of the last view step to XML.


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

## `T:Inversion.Web.Behaviour.View.XmlViewBehaviour`
Serialise the model of the last view step to XML.


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
## `P:Inversion.Web.UrlInfo.Regex`
The regular expression being used to break URLs down into their parts.

#### Remarks
If a regular expression isn't provided via the contructor then the default  [Inversion.Web.UrlInfo.DefaultRegex](F-Inversion.Web.UrlInfo.DefaultRegex)  is used.
## `P:Inversion.Web.UrlInfo.Match`
The matches produced by matching the [Inversion.Web.UrlInfo.Regex](P-Inversion.Web.UrlInfo.Regex)  against the  [Inversion.Web.UrlInfo.Url](P-Inversion.Web.UrlInfo.Url) .

## `P:Inversion.Web.UrlInfo.Url`
The url being processed.

## `P:Inversion.Web.UrlInfo.Protocol`
The protocol specified by the  [Inversion.Web.UrlInfo.Url](P-Inversion.Web.UrlInfo.Url) 

## `P:Inversion.Web.UrlInfo.Domain`
The domain specified by the  [Inversion.Web.UrlInfo.Url](P-Inversion.Web.UrlInfo.Url) 

## `P:Inversion.Web.UrlInfo.FullPath`
The full path specified by the  [Inversion.Web.UrlInfo.Url](P-Inversion.Web.UrlInfo.Url) 

## `P:Inversion.Web.UrlInfo.AppPath`
The application path specified by the  [Inversion.Web.UrlInfo.Url](P-Inversion.Web.UrlInfo.Url) 

## `P:Inversion.Web.UrlInfo.AppUrl`
The URL of the current application.

## `P:Inversion.Web.UrlInfo.File`
The file specified by the  [Inversion.Web.UrlInfo.Url](P-Inversion.Web.UrlInfo.Url) 

## `P:Inversion.Web.UrlInfo.Extension`
The extension specified by the  [Inversion.Web.UrlInfo.Url](P-Inversion.Web.UrlInfo.Url) 

## `P:Inversion.Web.UrlInfo.Tail`
The tail specified by the  [Inversion.Web.UrlInfo.Url](P-Inversion.Web.UrlInfo.Url) 

## `P:Inversion.Web.UrlInfo.QueryString`
The query string specified by the  [Inversion.Web.UrlInfo.Url](P-Inversion.Web.UrlInfo.Url) 

## `P:Inversion.Web.UrlInfo.Query`
A name / value dictionary as the propduct of parsing the  [Inversion.Web.UrlInfo.QueryString](P-Inversion.Web.UrlInfo.QueryString) 


## `T:Inversion.Web.WebContext`
Extends  [Inversion.Process.ProcessContext](T-Inversion.Process.ProcessContext)  with Web specific             information about an individual request being processed.

#### Remarks
The context object is threaded through the whole stack and provides a controled pattern and workflow of state, along with access to resources and services external to the application. Everything hangs off the context.

### `.#ctor(System.Web.HttpContext)`
Instantiates a new context object purposed for Web applications.

* `underlyingContext`: The underlying http context to wrap.
