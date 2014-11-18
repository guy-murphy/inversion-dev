
## `T:Inversion.Collections.ConcurrentDataCollection`1`
An implementation of  [Inversion.Collections.IDataCollection`1](T-Inversion.Collections.IDataCollection`1)  that             is safe for concurrent use.

`T`: The type of the elements in the collection.

## `T:Inversion.Collections.IDataCollection`1`
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
Produces an Xml representation of the model.

* `writer`: The writer to used to write the Xml to. 

### `.ToJson(Newtonsoft.Json.JsonWriter)`
Produces a Json respresentation of the model.

* `writer`: The writer to use for producing JSON.
## `P:Inversion.Collections.IDataCollection`1.Label`
The label that should be used for the collection in any notation presenting the collection. 


### `:Inversion.Collections.ConcurrentDataCollection`1.#ctor(System.String)`
Instantiates a new empty collection with the lable provided.

* `label`: The label of the collection.

### `:Inversion.Collections.ConcurrentDataCollection`1.#ctor`
Instantiates an empty collection.


### `:Inversion.Collections.ConcurrentDataCollection`1.#ctor(System.Collections.Generic.IEnumerable{`0})`
Instanciates a new data collection with elements copied from the provided collection.

* `collection`: The collection whose elements are copied into the new data collection.

### `:Inversion.Collections.ConcurrentDataCollection`1.#ctor(Inversion.Collections.IDataCollection{`0})`
Instantiates a collection populating it with the elements of the provided collection.

* `other`: The other collection to populate the new collection with.

### `:Inversion.Collections.ConcurrentDataCollection`1.ContentToXml(System.Xml.XmlWriter)`
Produces an XML representation of the collections elements  to a provided writer.

* `writer`: The  [System.Xml.XmlWriter](T-System.Xml.XmlWriter)  the representation is written to.

### `:Inversion.Collections.ConcurrentDataCollection`1.ContextToJson(Newtonsoft.Json.JsonWriter)`
Produces an JSON representation of the collection to a provided writer.

* `writer`: The  [Newtonsoft.Json.JsonWriter](T-Newtonsoft.Json.JsonWriter)  the representation is written to.

### `:Inversion.Collections.ConcurrentDataCollection`1.ToXml(System.Xml.XmlWriter)`
Produces an XML representation of the collection  to a provided writer.

* `writer`: The  [System.Xml.XmlWriter](T-System.Xml.XmlWriter)  the representation is written to.

### `:Inversion.Collections.ConcurrentDataCollection`1.ToJson(Newtonsoft.Json.JsonWriter)`
Produces an JSON representation of the collection  to a provided writer.

* `writer`: The  [Newtonsoft.Json.JsonWriter](T-Newtonsoft.Json.JsonWriter)  the representation is written to.
## `P:Inversion.Collections.ConcurrentDataCollection`1.Label`
The label that should be used for the collection in any notation presenting the collection. 

#### Remarks
This will default to "list".

## `T:Inversion.Collections.ConcurrentDataDictionary`1`
A thread-safe dictionary of key-value pairs where the key is a string and the dictionary itself implements `IData`

`TValue`: The type of the element values.

## `T:Inversion.Collections.IDataDictionary`1`
Represents a generic dictionary that implements  [Inversion.IData](T-Inversion.IData) , where the keys are strings.

`T`: The type of the element values.

### `.Import(System.Collections.Generic.IEnumerable{System.Collections.Generic.KeyValuePair{System.String,`0}})`
Import the provided key/value pairs into the dictionary.

* `other`: The key/value pairs to copy.

**returns:** 
This dictionary.


### `:Inversion.Collections.ConcurrentDataDictionary`1.#ctor`
Instantiates a new empty dictionary.


### `:Inversion.Collections.ConcurrentDataDictionary`1.#ctor(System.Collections.Generic.IEnumerable{System.Collections.Generic.KeyValuePair{System.String,`0}})`
Instantiates a new dictionary populated with the enumerable of key-value pairs provided.

* `other`: The key-value pairs to populate the dictionary with.

### `:Inversion.Collections.ConcurrentDataDictionary`1.#ctor(Inversion.Collections.DataDictionary{`0})`
Instantiates a new dictionary populated from the dictionary provided.

* `other`: The other dictionary to populate this dictionary from.

### `:Inversion.Collections.ConcurrentDataDictionary`1.Clone`
Clones the dictionary.


**returns:** 
Returnes a new dictionary instance populated by this one.


### `:Inversion.Collections.ConcurrentDataDictionary`1.Import(System.Collections.Generic.IEnumerable{System.Collections.Generic.KeyValuePair{System.String,`0}})`
Imports the key-value pairs from a provided dictionary into this one.

* `other`: The other dictionary to import into this one.

**returns:** 
Returns the current instance of this dictionary.


### `:Inversion.Collections.ConcurrentDataDictionary`1.ContentToXml(System.Xml.XmlWriter)`
Produces an XML representation of the dictionary elements.

* `writer`: The xml writer the xml should be written to.

### `:Inversion.Collections.ConcurrentDataDictionary`1.ToXml(System.Xml.XmlWriter)`
Produces and xml representation of the dictionary.

* `writer`: The xml writer the xml should be written to.

### `:Inversion.Collections.ConcurrentDataDictionary`1.ContentToJson(Newtonsoft.Json.JsonWriter)`
Produces a json representation of the dictionary elements.

* `writer`: The json writer the json should be written to.

### `:Inversion.Collections.ConcurrentDataDictionary`1.ToJson(Newtonsoft.Json.JsonWriter)`
Produces a json representation of the dictionary.

* `writer`: The json writer the json should be written to.

## `T:Inversion.Collections.DataCollection`1`
An implementation of  [Inversion.Collections.IDataCollection`1](T-Inversion.Collections.IDataCollection`1)  as a simple  [System.Collections.Generic.List`1](T-System.Collections.Generic.List`1) . 

`T`: The type of elements in the list.

### `.#ctor(System.String)`
Instantiates a new empty collection with the lable provided.

* `label`: The label of the collection.

### `.#ctor`
Instantiates a new, empty data collection.


### `.#ctor(System.Collections.Generic.IEnumerable{`0})`
Instantiates a new data collection with elements copied from the provided collection.

* `collection`: The collection whose elements are copied into the new data collection.

### `.#ctor(Inversion.Collections.IDataCollection{`0})`
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

## `T:Inversion.Collections.DataDictionary`1`
A collection of key/value pairs, where the key is a string.

`TValue`: The type of the element values.

### `.#ctor`
Instantiates a new empty instance of the dictionary.


### `.#ctor(System.Collections.Generic.IDictionary{System.String,`0})`
instantiates a new dictionary with the elements copied over from the dictionary provided.

* `other`: The dictionary to copy elements from.

### `.#ctor(System.Collections.Generic.IEnumerable{System.Collections.Generic.KeyValuePair{System.String,`0}})`
instantiates a new dictionary with the elements copied from iterating over the key/value pairs provided.

* `other`: The key/value pairs to copy.

### `.Clone`
Clones the data dictionary by instantiating a new one populated by the elemens of this one.


**returns:** 



### `.Import(System.Collections.Generic.IEnumerable{System.Collections.Generic.KeyValuePair{System.String,`0}})`
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

### `.Keys`
A collection of all the keys contained in the model.

### `.Values`
A collection of all the values of the model.

### `.Item(System.String)`
Obtains the value of the key-value pair with the same key as the one provided.

* `key`: The key to lookup.

**returns:** 
Returns the value of the key-value pair found, if any.


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

## `T:Inversion.Extensions.ArrayEx`
An extension class providing extensions for arrays.


### `.DeepClone``1(``0[])`
A simple extension that constructs a new array from one provided by calling `.Clone()` on each element of the provided array.

`T`: The type of the array elements.
* `self`: The array to act upon.

**returns:** 
Provides a new array with elements cloned from the originating array.


## `T:Inversion.Extensions.DictionaryEx`
Utility extension methods provided for dictionaries.


### `.Import``2(System.Collections.Generic.IDictionary{``0,``1},System.Collections.Generic.IDictionary{``0,``1})`
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


### `.ToXml``1(System.Collections.Generic.IEnumerable{``0},System.String)`
Produces an XML representation of an enumerable by iterating over the elements of the enumerable and calling `.ToXml()` on them.

`T`: The type of elements in the enumerable.
* `self`: The enumerable to act upon.
* `label`: The label of the enclosing element.

**returns:** 
An XML representation of the provided enumerable.


### `.ToXml``1(System.Collections.Generic.IEnumerable{``0},System.IO.TextWriter,System.String)`
Produces an XML representation of an enumerable by iterating over the elements of the enumerable and calling `.ToXml()` on them.

`T`: The type of elements in the enumerable.
* `self`: The enumerable to act upon.
* `writer`: The text writer the XML should be written to.
* `label`: The label of the enclosing element.

### `.ToXml``1(System.Collections.Generic.IEnumerable{``0},System.Xml.XmlWriter,System.String)`
Produces an XML representation of an enumerable by iterating over the elements of the enumerable and calling `.ToXml()` on them.

`T`: The type of elements in the enumerable.
* `self`: The enumerable to act upon.
* `xml`: The xml writer the XML should be written to.
* `label`: The label of the enclosing element.

### `.ToJson``1(System.Collections.Generic.IEnumerable{``0})`
Produces a JSON representation of an enumerable by iterating over the elements of the enumerable and calling `.ToJson()` on them.

`T`: The type of elements in the enumerable.
* `self`: The enumerable to act upon.

**returns:** 
An JSON representation of the provided enumerable.


### `.ToJson``1(System.Collections.Generic.IEnumerable{``0},System.IO.TextWriter)`
Produces a JSON representation of an enumerable by iterating over the elements of the enumerable and calling `.ToJson()` on them.

`T`: The type of elements in the enumerable.
* `self`: The enumerable to act upon.
* `writer`: The text writer the JSON should be written to.

### `.ToJson``1(System.Collections.Generic.IEnumerable{``0},Newtonsoft.Json.JsonWriter)`
Produces a JSON representation of an enumerable by iterating over the elements of the enumerable and calling `.ToJson()` on them.

`T`: The type of elements in the enumerable.
* `self`: The enumerable to act upon.
* `json`: The json writer the JSON should be written to.

## `T:Inversion.Extensions.ListEx`
Utility extension methods provided for lists.

#### Remarks
Just some methods to allow a list to be treated as a stack. If a stack is being used as a context in tree processing, sometimes being able to peek at more than the last element, or also treat the stack like a list is useful.

### `.Push``1(System.Collections.Generic.List{``0},``0)`
Pushes an elelent onto the list as if it were a stack.

`T`: The type of the list elements.
* `self`: The list being acted on.
* `item`: The element being pushed onto the list.

**returns:** 
The list being acted on.


### `.Pop``1(System.Collections.Generic.List{``0})`
Pops an element from the list as if it were a stack.

`T`: The type of the list elements.
* `self`: The list being acted on.

**returns:** 
The element that was poped.


### `.Peek``1(System.Collections.Generic.List{``0},System.Int32)`
Provides an index of the list in reverse order, with `list.Peek(0)` considering the last element of the list, and `list.Peek(1)` being the penultimate element of the list. No bounds checking is provided.

`T`: The type of the list elements.
* `self`: The list being acted on.
* `i`: The index of the peek.

**returns:** 
The element found at the index.


### `.Peek``1(System.Collections.Generic.List{``0})`
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

