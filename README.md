# SwissKnife
*SwissKnife* is the Swiss Army knife for .NET developers. Keep reading to see what it means and what it contains.

## Inevitable Ritual
>The Art of Programming:

> &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Ctrl + C

> &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Ctrl + V

This genuine Art was practiced by me for years. Every project initiation ceremony contained the same inevitable ritual. Open any solution you did so far. Any. Find the project that all other projects in the solution depend on. You will easily recognize it by its generic and faceless name - *Core* or *Common* or perhaps *Helpers*. Recycle the classes out of it. Don't think too much which ones. You will most likely need all of them. If you find some time, wipe them of a bit. Call it refactoring if it will make you feel better. Be sure not to forget to rename the namespaces.

## Swiss Army Knife
All these general purpose classes were diligently copied from one .NET project to another with a good reason. When put together, they form developer's equivalent of the [Swiss Army knife](https://en.wikipedia.org/wiki/Swiss_Army_knife). They are our blades, corkscrews, can openers, screwdrivers, scissors and so on. We need them no matter what kind of a .NET project we work on.

*SwissKnife* is the Swiss Army knife for .NET developers. Symbolically, it represents my decision to abandon the Art. Practically, it replaces the ritual of copy-pasting with the ritual of referencing a lightweight, well documented and well tested class library.

## Tools
*SwissKnife* is currently equipped with the following tools:

* [Option idiom](Projects/SwissKnife/Idioms/Option.cs)
* [Extension methods for various .NET collections](Projects/SwissKnife/Collections/CollectionExtensions.cs)
* [Commonly used code contracts](Projects/SwissKnife/Diagnostics/Contracts)
* [Extension methods for System.Type](Source/SwissKnife/TypeExtensions.cs)

If you have a good candidate for an additional tool, let me know. I'll be glad to see it being a part of *SwissKnife*.
