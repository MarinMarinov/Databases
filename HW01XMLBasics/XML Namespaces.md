## Using Namespaces

Using a namespace is the process of using one or more elements or attributes from the given namespace in an XML document. This requires an understanding of the syntax outlined by the Namespaces in XML Recommendation for qualifying element and attribute names with namespace identifiers.
The names of both elements and attributes are really made up of two parts: a namespace name and a local name. Such a two-part name is known as a qualified name or QName.
In an XML document we use a namespace prefix to qualify the local names of both elements and attributes . A prefix is really just an abbreviation for the namespace identifier (URI), which is typically quite long. The prefix is first mapped to a namespace identifier through a namespace declaration. The syntax for a namespace declaration is:

	xmlns:<prefix>='<namespace identifier>'

A namespace declaration looks just like an attribute (on an element), but they're not officially considered attributes in terms of the logical document structure (that is, they won't appear in an element's attributes collection when using the DOM).
A namespace prefix is considered in-scope on the declaration element as well as on any of its descendant elements. Once declared, the prefix can be used in front of any element or attribute name separated by a colon (such as s:student). This complete name including the prefix is the lexical form of a qualified name (QName):

	QName = <prefix>:<local name>

The prefix associates the element or attribute with the namespace identifier mapped to the prefix currently in scope.
Let's suppose that a developer wants to use the XSLT 1.0 namespace. He would need to provide a namespace declaration that maps an arbitrary prefix to the official XSLT 1.0 namespace identifier (**http://www.w3.org/1999/XSL/Transform**). Then each element or attribute that the developer wants to use from the XSLT 1.0 namespace simply needs to be prefixed accordingly, as illustrated by the following example:

	<x:transform version='1.0'
	   xmlns:x='http://www.w3.org/1999/XSL/Transform'>
	   <x:template match='/'>
	      <hello_world/>
	   </x:template>
	</x:transform>

The previous example shows the syntax for referring to elements within a namespace. Every element prefixed by "x" is from the http://www.w3.org/1999/XSL/Transform namespace, while anything that does not have a prefix is from no namespace (for example, hello_world). Processors can now distinguish between XSLT 1.0 programming constructs and literal elements that are meant for output, like hello_world. If the XSLT 1.0 namespace were misspelled by one character, an XSLT 1.0 processor wouldn't be able to recognize the document as a vocabulary it understands.
In essence, each element now has a two-part name, a namespace identifier and a local name. The combination of these two names is often referred to as the namespace name (note: this is different from the QName, which is the combination of the prefix and the local name).
As another example, the following XML document shows how to use the elements from the XML Schema definition shown earlier in this column:

	<d:student xmlns:d='http://www.develop.com/student'>
	  <d:id>3235329</d:id>
	  <d:name>Jeff Smith</d:name>
	  <d:language>C#</d:language>
	  <d:rating>9.5</d:rating>
	</d:student>

Notice that regardless of how namespaces are defined, the syntax for referring to them is the same.
When documents use elements or attributes from more than one namespace, it's common to have multiple namespace declarations on a given element, as illustrated by the following example:

	<d:student xmlns:d='http://www.develop.com/student'
	  xmlns:i='urn:schemas-develop-com:identifiers'
	  xmlns:p='urn:schemas-develop-com:programming-languages'>
	  <i:id>3235329</i:id>
	  <name>Jeff Smith</name>
	  <p:language>C#</p:language>
	  <d:rating>9.5</d:rating>
	</d:student>

Here, student and rating are both from the same namespace, while id and language are each from a different namespace, but name doesn't belong to a namespace.
Namespace prefixes can also be overridden by redeclaring the prefix at a nested scope, as shown here:

	<d:student xmlns:d='http://www.develop.com/student'>
	  <d:id>3235329</d:id>  
	  <d:name xmlns:d='urn:names-r-us'>Jeff Smith</d:name>
	  <d:language>C#</d:language>
	  <d:rating>35</d:rating>
	</d:student>

In this example, everything is from the same namespace except for the name element, which is from the urn:names-r-us namespace. While it is possible to re-declare a namespace prefix, it's not possible to undeclare a namespace prefix. For example, the following is illegal:

	<d:student xmlns:d='http://www.develop.com/student'>
	  <d:id xmlns:d=''>3235329</d:id>  
	   ...
	</d:student>

The prefix-oriented syntax for referring to things in an XML namespaces is fairly intuitive for most software developers. Had the Namespaces in XML Recommendation stopped here, namespaces would have been much less confusing.