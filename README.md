# SomeNone
An very minimalistic Option type for C# like the Haskell Maybe. 

## Why would you need this
You would want to use this to avoid all types of NullReferenceExceptions. Using Option types lets you return optionals instead of a certain value or null. You won't have to unpack the optional at every stage because of the many extension methods this package features. Unpacking is only required at the stage where you actually care about the contents.

Properly using an Optional will not only make the code shorter and more readable, it will also reduce the number of exceptions that can be thrown resulting in reduced debug workloads.


## Examples

```csharp
Some<int> someInt = 39;
Option<int> optionInt = 40;
None noneInt = None.Value;

int some = someInt;
if (optionInt is Some<int> newSomeInt)
{
    int option = newSomeInt;
}
```


Checking permissions based on a token that may or may not be available. In this example, any request can check permissions without having to worry about the existance of the token. If it's not present the method will be called with None and the permission check will return false.

```csharp
public bool HasPermission(Option<string> token)
{
    if (!(token is Some<string> someToken))
    {
        return false;
    }
    
    // check if token 'someToken' has access
}
```

Please note that someToken is of type Some<string> instead of Option<string>. This means that we can treat it like a normal string because of the implicit operator:

```csharp
public static implicit operator T(Some<T> some) => some.Content;
```

### Databases
Many C# systems that use some kind of data structure will heavily use FirstOrDefault to find values. We offer FirstOrNone that works exactly like FirstOrDefault but instead of returning null when no value is found it will produce a None.
```csharp
Option<Admin> admin = this.dbContext.Table.FirstOrNone(admin => admin.Id == adminId);
```
We now have the option to unpack it or leave it in the Option. In the case of a FirstOrDefault call we would have the same options, we could check for null or hand it over to the next method.

One advantage that we have over passing null is that we have Map. Map takes a Func<T, TResult> or Action<T> and only runs the given method on a Some<T> value. When Map is called on a None it will simply return None in the case of a Func<T, TResult> or void for the Action<T>.

