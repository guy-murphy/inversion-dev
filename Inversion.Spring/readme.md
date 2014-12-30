`Inversion.Spring`, project notes
## `T:Inversion.Spring.ServiceContainer`
A service container backed by Sprint.NET


### `.#ctor`
Instantiates a new service container, and configures it from the Spring config.

#### Remarks
In most cases you'll probably just want to use `ServiceContainer.Instance`

### `.#ctor(Spring.Context.IApplicationContext)`
Instantiates a new service container using the provided application context.

* `container`: You can think of this `container` as the underlying Spring backing. This is "the thing".

### `.Dispose`
Releases all reasources currently being used by this container.


### `.GetService(System.String)`
Gets the service if any of the provided name.

* `name`: The name of the service to obtain.

**returns:** 
Returns the service of the specified name.


### `.GetService(System.String,System.Type)`
Gets the service if any of the provided name. Further asserts that the service is on an expected type.

* `name`: The name of the service to obtain.
* `type`: The type the service is expected to be.

**returns:** 
Returns the service of the specified name.


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

### `.Instance`
A singleton instance of the service container.

