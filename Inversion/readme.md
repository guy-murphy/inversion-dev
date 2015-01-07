`Inversion`, projects notes
## `T:Inversion.Collections.ConcurrentDataCollection{T1}`
An implementation of  [Inversion.Collections.IDataCollection`1](T-Inversion.Collections.IDataCollection`1)  that             is safe for concurrent use.

`T`: The type of the elements in the collection.

## `T:Inversion.Collections.IDataCollection{T1}`
Represents a collection of  [Inversion.IData](T-Inversion.IData)   objects,              that can be accessed by index.

`T`: The type of elements in the list.

## `T:Inversion.IData`
Base interface for data models in Inversion.

#### Remarks
While this represents a point of extensibility for data models in Inversion this is pretty much limited to simple serialisation. `IData` had more relevance in Acumen pre-extension methods, now it's primary untility is simply to flag what is data-model and to be able to contrain based upon that.

This approach to model serialisation is simple, but also fast, uncluttered, and explicit. The writers used obviously are  often used by a whole object graph being written out, and is part of a broader application concern. The writer should not be used in any way that yields side-effects beyond the serialisation at hand.  [Inversion.IData.ToXml-System.Xml.XmlWriter](M-Inversion.IData.ToXml-System.Xml.XmlWriter)  and  [Inversion.IData.ToJson-Newtonsoft.Json.JsonWriter](M-Inversion.IData.ToJson-Newtonsoft.Json.JsonWriter) need to be fast and reliable. 

Inversion favours an application where XML (or JSON for apps with data clients) is its primary external interface, which is transformed (normally with XSL) into a view stuitable for a particular class of user-agent. Other approaches may have a different regard for a data-model and `IData` serves as a point of extension in those cases perhaps.

Extension methods are provided in  [Inversion.DataEx](T-Inversion.DataEx)  to provide `IData.ToXml()` and `IData.ToJson()`.



### `.ToXml(System.Xml.XmlWriter)`
Produces an xml representation of the model.

* `writer`: The writer to used to write the xml to. 

### `.ToJson(Newtonsoft.Json.JsonWriter)`
Produces a json respresentation of the model.

* `writer`: The writer to use for producing json.
### `.Data`
Provides an abstract representation of the objects data expressed as a JSON object.


### `:Inversion.Collections.IDataCollection{T1}.Add({T0})`
Adds an item to the collection.

* `item`: The item to add to the collection.
## `P:Inversion.Collections.IDataCollection{T1}.Label`
The label that should be used for the collection in any notation presenting the collection. 


### `:Inversion.Collections.ConcurrentDataCollection{T1}.#ctor(System.String)`
Instantiates a new empty collection with the lable provided.

* `label`: The label of the collection.

### `:Inversion.Collections.ConcurrentDataCollection{T1}.#ctor`
Instantiates an empty collection.


### `:Inversion.Collections.ConcurrentDataCollection{T1}.#ctor(System.Collections.Generic.IEnumerable{{T0}})`
Instanciates a new data collection with elements copied from the provided collection.

* `collection`: The collection whose elements are copied into the new data collection.

### `:Inversion.Collections.ConcurrentDataCollection{T1}.#ctor(Inversion.Collections.IDataCollection{{T0}})`
Instantiates a collection populating it with the elements of the provided collection.

* `other`: The other collection to populate the new collection with.

### `:Inversion.Collections.ConcurrentDataCollection{T1}.Clone`
Creates a new instance of a data collection as a copy of this data collection.


**returns:** 



### `:Inversion.Collections.ConcurrentDataCollection{T1}.ContentToXml(System.Xml.XmlWriter)`
Produces an XML representation of the collections elements  to a provided writer.

* `writer`: The  [System.Xml.XmlWriter](T-System.Xml.XmlWriter)  the representation is written to.

### `:Inversion.Collections.ConcurrentDataCollection{T1}.ContextToJson(Newtonsoft.Json.JsonWriter)`
Produces an JSON representation of the collection to a provided writer.

* `writer`: The  [Newtonsoft.Json.JsonWriter](T-Newtonsoft.Json.JsonWriter)  the representation is written to.

### `:Inversion.Collections.ConcurrentDataCollection{T1}.ToXml(System.Xml.XmlWriter)`
Produces an XML representation of the collection  to a provided writer.

* `writer`: The  [System.Xml.XmlWriter](T-System.Xml.XmlWriter)  the representation is written to.

### `:Inversion.Collections.ConcurrentDataCollection{T1}.ToJson(Newtonsoft.Json.JsonWriter)`
Produces an JSON representation of the collection  to a provided writer.

* `writer`: The  [Newtonsoft.Json.JsonWriter](T-Newtonsoft.Json.JsonWriter)  the representation is written to.
## `P:Inversion.Collections.ConcurrentDataCollection{T1}.Label`
The label that should be used for the collection in any notation presenting the collection. 

#### Remarks
This will default to "list".
## `P:Inversion.Collections.ConcurrentDataCollection{T1}.Data`
Provides an abstract representation of the objects data expressed as a JSON object.

#### Remarks
For this type the json object is created each time it is accessed.

## `T:Inversion.Collections.ConcurrentDataDictionary{T1}`
A thread-safe dictionary of key-value pairs where the key is a string and the dictionary itself implements `IData`

`TValue`: The type of the element values.

## `T:Inversion.Collections.IDataDictionary{T1}`
Represents a generic dictionary that implements  [Inversion.IData](T-Inversion.IData) , where the keys are strings.

`T`: The type of the element values.

### `.Import(System.Collections.Generic.IEnumerable{System.Collections.Generic.KeyValuePair{System.String,{T0}}})`
Import the provided key/value pairs into the dictionary.

* `other`: The key/value pairs to copy.

**returns:** 
This dictionary.


### `:Inversion.Collections.ConcurrentDataDictionary{T1}.#ctor`
Instantiates a new empty dictionary.


### `:Inversion.Collections.ConcurrentDataDictionary{T1}.#ctor(System.Collections.Generic.IEnumerable{System.Collections.Generic.KeyValuePair{System.String,{T0}}})`
Instantiates a new dictionary populated with the enumerable of key-value pairs provided.

* `other`: The key-value pairs to populate the dictionary with.

### `:Inversion.Collections.ConcurrentDataDictionary{T1}.#ctor(Inversion.Collections.DataDictionary{{T0}})`
Instantiates a new dictionary populated from the dictionary provided.

* `other`: The other dictionary to populate this dictionary from.

### `:Inversion.Collections.ConcurrentDataDictionary{T1}.Clone`
Clones the dictionary.


**returns:** 
Returnes a new dictionary instance populated by this one.


### `:Inversion.Collections.ConcurrentDataDictionary{T1}.Import(System.Collections.Generic.IEnumerable{System.Collections.Generic.KeyValuePair{System.String,{T0}}})`
Imports the key-value pairs from a provided dictionary into this one.

* `other`: The other dictionary to import into this one.

**returns:** 
Returns the current instance of this dictionary.


### `:Inversion.Collections.ConcurrentDataDictionary{T1}.ContentToXml(System.Xml.XmlWriter)`
Produces an XML representation of the dictionary elements.

* `writer`: The xml writer the xml should be written to.

### `:Inversion.Collections.ConcurrentDataDictionary{T1}.ToXml(System.Xml.XmlWriter)`
Produces and xml representation of the dictionary.

* `writer`: The xml writer the xml should be written to.

### `:Inversion.Collections.ConcurrentDataDictionary{T1}.ContentToJson(Newtonsoft.Json.JsonWriter)`
Produces a json representation of the dictionary elements.

* `writer`: The json writer the json should be written to.

### `:Inversion.Collections.ConcurrentDataDictionary{T1}.ToJson(Newtonsoft.Json.JsonWriter)`
Produces a json representation of the dictionary.

* `writer`: The json writer the json should be written to.
## `P:Inversion.Collections.ConcurrentDataDictionary{T1}.Data`
Provides an abstract representation of the objects data expressed as a JSON object.

#### Remarks
For this type the json object is created each time it is accessed.
## `P:Inversion.Collections.ConcurrentDataDictionary{T1}.Item(System.String)`
Provides indexed acccess tot he dictionary with the key provided.

* `key`: The key used to index a key/value pair.

**returns:** 
Returns the value associated with the specified key.


## `T:Inversion.Collections.DataCollection{T1}`
An implementation of  [Inversion.Collections.IDataCollection`1](T-Inversion.Collections.IDataCollection`1)  as a simple  [System.Collections.Generic.List`1](T-System.Collections.Generic.List`1) . 

`T`: The type of elements in the list.

### `.#ctor(System.String)`
Instantiates a new empty collection with the lable provided.

* `label`: The label of the collection.

### `.#ctor`
Instantiates a new, empty data collection.


### `.#ctor(System.Collections.Generic.IEnumerable{{T0}})`
Instantiates a new data collection with elements copied from the provided collection.

* `collection`: The collection whose elements are copied into the new data collection.

### `.#ctor(Inversion.Collections.IDataCollection{{T0}})`
Instantiates a collection populating it with the elements of the provided collection.

* `other`: The other collection to populate the new collection with.

### `.Clone`
Creates a clone of the collection by instantiating a new collection populated with the elements of this one.


**returns:** 
Returns a new collection populated by this one.


### `.ContentToXml(System.Xml.XmlWriter)`
Produces an XML representation of the dictionaries elements, to a provided writer.

* `writer`: The  [System.Xml.XmlWriter](T-System.Xml.XmlWriter)  the representation is written to.

### `.ContextToJson(Newtonsoft.Json.JsonWriter)`
Produces an JSON representation of the dictionaries elements, to a provided writer.

* `writer`: The  [Newtonsoft.Json.JsonWriter](T-Newtonsoft.Json.JsonWriter)  the representation is written to.

### `.ToXml(System.Xml.XmlWriter)`
Produces an XML representation of the dictionaries  to a provided writer.

* `writer`: The  [System.Xml.XmlWriter](T-System.Xml.XmlWriter)  the representation is written to.

### `.ToJson(Newtonsoft.Json.JsonWriter)`
Produces an JSON representation of the dictionaries  to a provided writer.

* `writer`: The  [Newtonsoft.Json.JsonWriter](T-Newtonsoft.Json.JsonWriter)  the representation is written to.
### `.Label`
The label that should be used for the collection in any notation presenting the collection. 

#### Remarks
This will default to "list".
### `.Data`
Provides an abstract representation of the objects data expressed as a JSON object.

#### Remarks
For this type the json object is created each time it is accessed.

## `T:Inversion.Collections.DataDictionary{T1}`
A collection of key/value pairs, where the key is a string.

`TValue`: The type of the element values.

### `.#ctor`
Instantiates a new empty instance of the dictionary.


### `.#ctor(System.Collections.Generic.IDictionary{System.String,{T0}})`
instantiates a new dictionary with the elements copied over from the dictionary provided.

* `other`: The dictionary to copy elements from.

### `.#ctor(System.Collections.Generic.IEnumerable{System.Collections.Generic.KeyValuePair{System.String,{T0}}})`
instantiates a new dictionary with the elements copied from iterating over the key/value pairs provided.

* `other`: The key/value pairs to copy.

### `.Clone`
Clones the data dictionary by instantiating a new one populated by the elemens of this one.


**returns:** 



### `.Import(System.Collections.Generic.IEnumerable{System.Collections.Generic.KeyValuePair{System.String,{T0}}})`
Instantiates a dictionary populating it with the elements of the provided dictionary.

* `other`: The other dictionary to populate the new collection with.

### `.ContentToXml(System.Xml.XmlWriter)`
Produces an xml representation of the elements of the dictionary.

* `writer`: The xml writer the representation should be written to.

### `.ToXml(System.Xml.XmlWriter)`
Produces and xml representation of the dictionary.

* `writer`: The xml writer the xml should be written to.

### `.ContentToJson(Newtonsoft.Json.JsonWriter)`
Produces a json representation of the dictionaries elements.

* `writer`: The json writer the representation should be written to.

### `.ToJson(Newtonsoft.Json.JsonWriter)`
Produces a json representation of the dictionary.

* `writer`: The json writer the representation should be written to.
### `.Data`
Provides an abstract representation of the objects data expressed as a JSON object.

#### Remarks
For this type the json object is created each time it is accessed.

## `T:Inversion.Collections.DataModel`
A  [System.Dynamic.DynamicObject](T-System.Dynamic.DynamicObject)  implementing             an  [Inversion.Collections.IDataDictionary`1](T-Inversion.Collections.IDataDictionary`1)  .

#### Remarks
This class is intended to help with exposing models to Razor templates, as it allows ad hoc properties to be used as dictionary keys, `model.UserDetails.Name` rather than `model["UserDetails"].Name`

The initial idea was for  the `ControlState` to be one of these. When I start playing about with Razor a bit more I'll test it to see if there's any consequences.



### `.TrySetMember(System.Dynamic.SetMemberBinder,System.Object)`
Implements trying to set a member of the data model ensuring that the value being assigned is both of type `IData` and not null.

* `binder`: The binder provided by the call site.
* `value`: The value to set.

**returns:** 
true if the operation is complete, false if the call site should determine behavior.


### `.TryGetMember(System.Dynamic.GetMemberBinder,System.Object@)`
Implements trying to get a member.

* `binder`: The binder provided by the call site.
* `result`: The result of the get operation.

**returns:** 
true if the operation is complete, false if the call site should determine behavior.


### `.Import(System.Collections.Generic.IEnumerable{System.Collections.Generic.KeyValuePair{System.String,Inversion.IData}})`
Imports the key-value pairs from a provided dictionary into this one.

* `other`: The other dictionary to import into this one.

**returns:** 
Returns the current instance of this dictionary.


### `.Add(System.String,Inversion.IData)`
Adds the provided value to the model against the specified key.

* `key`: The key for the new element.
* `value`: The value to be added/

### `.ContainsKey(System.String)`
Determines whether or not the model contains anything stored against the provided key.

* `key`: The key to check.

**returns:** 
Returns true if the model contains a key-value pair with a key corresponding to the specified key; otherwise returns false.


### `.Remove(System.String)`
Removes the key-value pair of the specified key.

* `key`: The key to remove from the model.

**returns:** 
Rreturns true if the key was found and removed; otherwise returns false.


### `.TryGetValue(System.String,Inversion.IData@)`
Tries to get the value of the key-value pair with the same key as the one provided.

* `key`: The key to lookup.
* `value`: The value of the found key-value pair.

**returns:** 
Returns true if the operation was successful; otherwise returns false.


### `.Add(System.Collections.Generic.KeyValuePair{System.String,Inversion.IData})`
Adds an element to the collection.

* `item`: The item to add to the collection.

### `.Clear`
Removes all elements from the collection.


### `.Contains(System.Collections.Generic.KeyValuePair{System.String,Inversion.IData})`
Checks to see if the collection contains a particular element.

* `item`: The item to check for in the collection.

**returns:** 
Returns true if the item is contained in the collection; otherwise returns false.


### `.CopyTo(System.Collections.Generic.KeyValuePair{System.String,Inversion.IData}[],System.Int32)`
Copies elements from the collection to and array, starting at a specified index in the array.

* `array`: The array to copy elements to.
* `arrayIndex`: The index in the array to start copying to.

### `.Remove(System.Collections.Generic.KeyValuePair{System.String,Inversion.IData})`
Removes a specific item from the collection if it is present.

* `item`: The item to remove from the collection.

**returns:** 
Returns true if the item was removed; otherwise returns false.


### `.GetEnumerator`
Gets an enumerator that can be used to iterate over the collection.


**returns:** 
The enumerator for the collection.


### `.System#Collections#IEnumerable#GetEnumerator`
Gets an enumerator that can be used to iterate over the collection.


**returns:** 
The enumerator for the collection.


### `.ToXml(System.Xml.XmlWriter)`
Produces an xml representation of the collection.

* `writer`: The xml writer the xml should be written to.

### `.ToJson(Newtonsoft.Json.JsonWriter)`
Produces a json representation of the collection.

* `writer`: The writer the json should be written to.

### `.Clone`
Clones this collection by creating a new one populated by elements from this on.


**returns:** 
Returns the new collection as a copy of this one.

### `.Data`
Provides an abstract representation of the objects data expressed as a JSON object.

#### Remarks
For this type the json object is created each time it is accessed.
### `.Keys`
A collection of all the keys contained in the model.

### `.Values`
A collection of all the values of the model.

### `.Item(System.String)`
Obtains the value of the key-value pair with the same key as the one provided.

* `key`: The key to lookup.

**returns:** 
Returns the value of the key-value pair found, if any.

### `.Count`
Obtains the number of elements in the collection.

### `.IsReadOnly`
Determines if the collection is read-only or not.


## `T:Inversion.DataEx`
Extension methods for  [Inversion.IData](T-Inversion.IData) largely concerned with supporting both `.ToXml(...)` and `.ToJson(...)`


### `.ToXml(Inversion.IData)`
Generates an XML representation of the specified  [Inversion.IData](T-Inversion.IData)  object.

* `self`: The data model to produce XML for.

**returns:** 
Returns the XML representation as a `string`.

#### Remarks
This is implemented by creating a `StringWriter` and calling `.ToXml(IData, StringWriter)`

### `.ToXml(Inversion.IData,System.IO.TextWriter)`
Produces an xml representation of the subject `IData` object.

* `self`: The `IData` object to act upon.
* `writer`: The xml writer to write the representation to.

### `.ToJson(Inversion.IData)`
Produces a json representation of the subject `IData` object.

* `self`: The `IData` object to act upon.

**returns:** 
Return the json representation of the `IData` object as a string.


### `.ToJson(Inversion.IData,System.IO.TextWriter)`
Produces a json representation of the subject `IData` object.

* `self`: The `IData` object to act upon.
* `writer`: The text writer the representation should be writtern to.

### `.ToJsonObject(Inversion.IData)`
Provides a JSON Object view of the objects data.

* `self`: The `IData` object to act upon.

**returns:** 
Returns a `JObject` representation of this objects data.


## `T:Inversion.DataView`
Represents a frozen view of an `IData` object.

#### Remarks
The idea here is that if you need to be using the JSON representation of a mutable entity, it's going to be expensive to generate that JSON representation each time it is accessed. This applies to XML also, but the case is felt to be more likely with the JSON rep. So the purpose of the data-view is to take a snap-shot of the entity, with the JSON being generated only the once. Unfortunately `JObject` is muttable making it unfit for what is supposed to be an immutable view. A guard has been put in to throw an exception on property change for the JObject, but this is felt to be only just adequate long-term. I'm going to see how this plays out in actual usage before deciding if it's appropriate. See `JDataObject` for an alternative but similar approach.

### `.#ctor(Inversion.IData)`
Instantiates a new data view object.

* `other`: The `IData` the data view should be created from.

### `.System#ICloneable#Clone`
Creates a new object that is a copy of the current instance.


**returns:** 
A new object that is a copy of this instance.


### `.Clone`
Creates a new object that is a copy of the current instance.


**returns:** 
A new object that is a copy of this instance.


### `.ToXml(System.Xml.XmlWriter)`
Produces an xml representation of the model.

* `writer`: The writer to used to write the xml to. 

### `.ToJson(Newtonsoft.Json.JsonWriter)`
Produces a json respresentation of the model.

* `writer`: The writer to use for producing json.
### `.Data`
Provides an abstract representation of the objects data expressed as a JSON object.


## `T:Inversion.Extensions.ArrayEx`
An extension class providing extensions for arrays.


### `.DeepClone`{T1}(`{T0}[])`
A simple extension that constructs a new array from one provided by calling `.Clone()` on each element of the provided array.

`T`: The type of the array elements.
* `self`: The array to act upon.

**returns:** 
Provides a new array with elements cloned from the originating array.


## `T:Inversion.Extensions.DictionaryEx`
Utility extension methods provided for dictionaries.


### `.Import`{T2}(System.Collections.Generic.IDictionary{`{T0},`{T1}},System.Collections.Generic.IDictionary{`{T0},`{T1}})`
Copies the elements from one dictionary to another.

`TKey`: The type of the dictionary keys.
`TValue`: The type of the dictionary values.
* `self`: The dictionary being acted on.
* `other`: The dictionary being copied from.

### `.ContentToXml(System.Collections.Generic.IDictionary{System.String,Inversion.IData},System.Xml.XmlWriter)`
Produces an XML representation of the elements of a dictionary.

* `self`: The dictionary being acted upon.
* `writer`: The  [System.Xml.XmlWriter](T-System.Xml.XmlWriter)  the representation             is written to.

## `T:Inversion.Extensions.EnumerableEx`
An extension class providing extensions for `IEnumerable{T}` objects.


### `.ToXml`{T1}(System.Collections.Generic.IEnumerable{`{T0}},System.String)`
Produces an XML representation of an enumerable by iterating over the elements of the enumerable and calling `.ToXml()` on them.

`T`: The type of elements in the enumerable.
* `self`: The enumerable to act upon.
* `label`: The label of the enclosing element.

**returns:** 
An XML representation of the provided enumerable.


### `.ToXml`{T1}(System.Collections.Generic.IEnumerable{`{T0}},System.IO.TextWriter,System.String)`
Produces an XML representation of an enumerable by iterating over the elements of the enumerable and calling `.ToXml()` on them.

`T`: The type of elements in the enumerable.
* `self`: The enumerable to act upon.
* `writer`: The text writer the XML should be written to.
* `label`: The label of the enclosing element.

### `.ToXml`{T1}(System.Collections.Generic.IEnumerable{`{T0}},System.Xml.XmlWriter,System.String)`
Produces an XML representation of an enumerable by iterating over the elements of the enumerable and calling `.ToXml()` on them.

`T`: The type of elements in the enumerable.
* `self`: The enumerable to act upon.
* `xml`: The xml writer the XML should be written to.
* `label`: The label of the enclosing element.

### `.ToJson`{T1}(System.Collections.Generic.IEnumerable{`{T0}})`
Produces a JSON representation of an enumerable by iterating over the elements of the enumerable and calling `.ToJson()` on them.

`T`: The type of elements in the enumerable.
* `self`: The enumerable to act upon.

**returns:** 
An JSON representation of the provided enumerable.


### `.ToJson`{T1}(System.Collections.Generic.IEnumerable{`{T0}},System.IO.TextWriter)`
Produces a JSON representation of an enumerable by iterating over the elements of the enumerable and calling `.ToJson()` on them.

`T`: The type of elements in the enumerable.
* `self`: The enumerable to act upon.
* `writer`: The text writer the JSON should be written to.

### `.ToJson`{T1}(System.Collections.Generic.IEnumerable{`{T0}},Newtonsoft.Json.JsonWriter)`
Produces a JSON representation of an enumerable by iterating over the elements of the enumerable and calling `.ToJson()` on them.

`T`: The type of elements in the enumerable.
* `self`: The enumerable to act upon.
* `json`: The json writer the JSON should be written to.

### `.CalculateHash(System.Collections.Generic.IEnumerable{System.String})`
Produces a hash from all the string elements of an enumerable.

* `self`: The enumerable of strings to act upon.

**returns:** 
Returns a has of all the elements.


## `T:Inversion.Extensions.JsonWriterEx`
Conventient extension methods for the json writer.


### `.WriteProperty(Newtonsoft.Json.JsonWriter,System.String,System.String)`
Writes both a json property name, and a value at the same time.

* `self`: The json writer to act upon.
* `name`: The name of the property.
* `value`: The value of the property.

## `T:Inversion.Extensions.ListEx`
Utility extension methods provided for lists.

#### Remarks
Just some methods to allow a list to be treated as a stack. If a stack is being used as a context in tree processing, sometimes being able to peek at more than the last element, or also treat the stack like a list is useful.

### `.Push`{T1}(System.Collections.Generic.List{`{T0}},`{T0})`
Pushes an elelent onto the list as if it were a stack.

`T`: The type of the list elements.
* `self`: The list being acted on.
* `item`: The element being pushed onto the list.

**returns:** 
The list being acted on.


### `.Pop`{T1}(System.Collections.Generic.List{`{T0}})`
Pops an element from the list as if it were a stack.

`T`: The type of the list elements.
* `self`: The list being acted on.

**returns:** 
The element that was poped.


### `.Peek`{T1}(System.Collections.Generic.List{`{T0}},System.Int32)`
Provides an index of the list in reverse order, with `list.Peek(0)` considering the last element of the list, and `list.Peek(1)` being the penultimate element of the list. No bounds checking is provided.

`T`: The type of the list elements.
* `self`: The list being acted on.
* `i`: The index of the peek.

**returns:** 
The element found at the index.


### `.Peek`{T1}(System.Collections.Generic.List{`{T0}})`
Takes a look at the last element of a list without removing it, as if it were a stack.

`T`: The type of the list elements.
* `self`: The list being acted on.

**returns:** 
The last element of the list.


## `T:Inversion.Extensions.StringBuilderEx`
Some utility extension methods provided for string builders.


### `.Filter(System.Text.StringBuilder,System.Predicate{System.Char})`
Filters a `StringBuilder`, removing any elements that the provided predicate returns true for.

* `self`: The string builder being acted upon.
* `test`: The predicate to test each element for removal.

**returns:** 
The string builder being acted upon.


### `.RemoveNonNumeric(System.Text.StringBuilder)`
Removes all non-numeric characters from the string builder.

* `self`: The string builder being acted upon.

**returns:** 
The string builder being acted upon.


### `.RemoveNonAlpha(System.Text.StringBuilder)`
Removes all the non-alphabetic characters from the string builder.

* `self`: The string builder being acted upon.

**returns:** 
The string builder being acted upon.


### `.RemoveNonAlphaNumeric(System.Text.StringBuilder)`
Removes all non-alphanumeric characters from the string builder.

* `self`: The string builder being acted upon.

**returns:** 
The string builder being acted upon.


### `.RemoveWhitespace(System.Text.StringBuilder)`
Removes all whitespace from the string builder.

* `self`: The string builder being acted upon.

**returns:** 
The string builder being acted upon.


### `.TrimLeftBy(System.Text.StringBuilder,System.Int32)`
Removes a specified number of characters from the left-side of the string builder.

* `self`: The string builder being acted upon.
* `amount`: 

**returns:** 
The string builder being acted upon.


### `.TrimRightBy(System.Text.StringBuilder,System.Int32)`
Removes a specified number of characters from the right-side of the string builder.

* `self`: The string builder being acted upon.
* `amount`: 

**returns:** 
The string builder being acted upon.


### `.TrimEndsBy(System.Text.StringBuilder,System.Int32)`
Removes a specified number of characters from each end of the string builder.

* `self`: The string builder being acted upon.
* `amount`: 

**returns:** 
The string builder being acted upon.


## `T:Inversion.Extensions.StringEx`
An extension class providing extensions for string.


### `.HasValue(System.String)`
Determines if the string is not null and has a length greater than zero.

* `self`: The subject of extension.

**returns:** 
Returns true if the string has a values;             otherwise returns false.


### `.AssertHasValue(System.String,System.String)`
Checks if a string has a value and if not throws an  [System.ArgumentNullException](T-System.ArgumentNullException) .

* `self`: The subject of extension.
* `message`: The message to use as part of the exception.
[Inversion.Extensions.StringEx.HasValue-System.String](M-Inversion.Extensions.StringEx.HasValue-System.String)
### `.Prepend(System.String,System.Int32,System.Char)`
Places the 

* `self`: 
* `number`: 
* `character`: 

**returns:** 



### `.IsXmlName(System.String)`
Determines if a string is a valid XML tag name.

* `self`: The subject of extension.

**returns:** 
Returns true if the string is a valid XML name;             otherwise, returns false.


### `.Filter(System.String,System.Predicate{System.Char})`
Filters out characters from string by testing them with a predicate.

* `self`: The string to act upon.
* `test`: The predicate to test each character with.

**returns:** 
Returns a new string containing only those charcters for which the test returned true.


### `.RemoveNonNumeric(System.String)`
Produces a new string by removing all non-numeric characters from the sting provided.

* `self`: The string to act upon.

**returns:** 
The new filtered string.


### `.RemoveNonAlpha(System.String)`
Produces a new string by removing all alphabetic characters from the sting provided.

* `self`: The string to act upon.

**returns:** 
The new filtered string.


### `.RemoveNonAlphaNumeric(System.String)`
Produces a new string by removing all non-alpha-numeric characters from the sting provided.

* `self`: The string to act upon.

**returns:** 
The new filtered string.


### `.RemoveWhitespace(System.String)`
Produces a new string by removing all whitespace characters from the sting provided.

* `self`: The string to act upon.

**returns:** 
The new filtered string.


### `.RemoveInvalidXmlCharacters(System.String)`
This method ensures that the returned string has only valid XML unicode charcters as specified in the XML 1.0 standard. For reference please see http://www.w3.org/TR/2000/REC-xml-20001006#NT-Char for the standard reference.

* `self`: The string being acted upon.

**returns:** 
A copy of the input string with non-valid charcters removed.


### `.TrimLeftBy(System.String,System.Int32)`
Removes characters from the left side of a string.

* `self`: The string to be acted upon.
* `amount`: The number of charcters to remove.

**returns:** 
Returns a new string with the characters removed.


### `.TrimRightBy(System.String,System.Int32)`
Removes characters from the right side of a string.

* `self`: The string to be acted upon.
* `amount`: The number of charcters to remove.

**returns:** 
Returns a new string with the characters removed.


### `.TrimEndsBy(System.String,System.Int32)`
Removes characters from the left and right sides of a string.

* `self`: The string to be acted upon.
* `amount`: The number of charcters to remove.

**returns:** 
Returns a new string with the characters removed.


### `.Hash(System.String)`
Generates a simple hash for a string. 

#### Remarks
This hash is not asserted to be fit for any particular purpose other than simple features where you just need a hash of a string.
* `self`: The string to be acted upon.

**returns:** 
Returns a simple hash of a string.


### `.ReplaceKeys(System.String,System.Collections.Generic.IDictionary{System.String,System.String})`
Regards all occurrences of substrings starting and finishing with `|` pipe charcters as potential keys, and if those keys occur within the provided dictionary, replaces those keys in the provided text with the corresposponding value in the dictionary.

#### Remarks
This is performed as a single scan of characters and should be used in preference in those situations where you find yourself doing multiple replacements on a large string, as this will do them in one go.
* `text`: The text to act upon.
* `kv`: The dictionary of key-value pairs for substitution.

**returns:** 
Returns a new string with any matching keys replaced.


## `T:Inversion.IConsumeData{T2}`
Expresses a type that is able to consume both json and xml representations of itself.

`TBuilder`: The type of the builder being used for this type.
`TConcrete`: The type of the concrete product of consuming data.

### `.FromConcrete({T1})`
Assigns values to this object based on those of the other object provided.

* `other`: The other object from which to take values.

**returns:** 
Returns a builder populated from the provided concrete object.


### `.FromJson(Newtonsoft.Json.Linq.JObject)`
Assigns values to this object based on those in the json provided.

* `json`: The property set from which to take values.

**returns:** 
Returns a builded populated from the json provided.


### `.ToConcrete`
Produced a concrete object from this one.


**returns:** 
Returns a concrete object from this one.


## `T:Inversion.IMutate{T2}`
Describes a type that manages mutation via a `Mutate` function using a builder object as an intermediary on which to exercise mutation.

`TBuilder`: The type of the builder that will be used for mutation.
`TConcrete`: The type of the concrete object to be created.

### `.Mutate(System.Func{{T0},{T1}})`
Mutates the current object by transforming it to a builder, applying a mutation function to the builder, and then transforming the builder back to a specified concrete type.

* `mutator`: The mutation function to apply to the builder.

**returns:** 
Returns a concrete object which is the product of a builder that has had the mutation function applied to it.


## `T:Inversion.JDataObject`
Implements a `JObject` as an `IData` type.

#### Remarks
This is addressing a concern not disimilar to that being addressed by `DataView` which is the presentation of data in abstract terms especially for views or ad-hoc data.

### `.#ctor(Newtonsoft.Json.Linq.JObject)`
Instantiates a new `JDataObject` from an other `JObject`.

* `other`: The `JObject` to copy data from.

### `.#ctor(Inversion.IData)`
Instantiates a new `JDataObject` from another `IDataObject`.

* `other`: The `IData` object to copy data from.

### `.ToXml(System.Xml.XmlWriter)`
Produces an xml representation of the model.

* `writer`: The writer to used to write the xml to. 

### `.ToJson(Newtonsoft.Json.JsonWriter)`
Produces a json respresentation of the model.

* `writer`: The writer to use for producing json.
### `.Data`
Provides an abstract representation of the objects data expressed as a JSON object.


## `T:Inversion.TextData`
An implementation of  [Inversion.IData](T-Inversion.IData)  that             represents a simple text node within a model.


### `.op_Implicit(System.String)~Inversion.TextData`
Implicitly casts a string of text into a `TextData` object, by instantiating a `TextData` object from the string.

* `text`: The string of text to be cast.

**returns:** 
Returns the `TextData` object created.


### `.op_Implicit(Inversion.TextData)~System.String`
Implicitly casts a `TextData` object into a string.

* `text`: The `TextData` object to cast.

**returns:** 
Returns the string value of the `TextData` object.


### `.#ctor(System.String)`
Instantiates a new `TextData` object with the value of the text provided.

* `text`: The text to initialise from.

### `.#ctor(Inversion.TextData)`
Instantiates a new `TextData` object as a copy of the one provided.

* `text`: The `TextData` to copy.

### `.Clone`
Creates a new instance as a copy of the original.


**returns:** 
A copy as a `TextData` object.


### `.ToXml(System.Xml.XmlWriter)`
Produces an xml representation of the text data.

* `writer`: The xml writer the representation should be written to.

### `.ToJson(Newtonsoft.Json.JsonWriter)`
Produces a json representation of the text data.

* `writer`: The json writer the representation should be written to.

### `.ToString`
Returns a string that represents the current object.


**returns:** 
A string that represents the current object.

### `.Value`
The string value of the text data.

### `.Data`
Provides an abstract representation of the objects data expressed as a JSON object.

#### Remarks
For this type the json object is only created the once.
