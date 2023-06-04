# **GildedRose - C# Core - Verify**
**This is an example of an application refactoring.**<br><br>

**Summary:**<br>
All tests run and more have been added to the ones already present, the logic has been updated according to the [requirements](https://github.com/emilybache/GildedRose-Refactoring-Kata/blob/main/GildedRoseRequirements.txt), and the application overall have been refactored to be easier to scale and mantain going forward.<br><br>

**Refactoring for scalability and mantainability**:<br>
- ***Data layer***: a mock database class is created extending an interface, this allows for IoC making it easier to integrate different database sources into the application in the future.
- ***Caching layer***: a caching class allows to store the frequently used elements at runtime sourced from a database, this both speeds up runtime since the application does not need to constantly retrieve elements (a cache expiration is set to ensure eventual consistency with the underlying data), and eases the database of the constant querying for identical elements.
- ***logic clean up***: the main class "GildedRose" where most of the logic resides has been split up into two partials, keeping the depreciation login in its own file in a more orderly approach. Within its partial, the depreciation logic previously consisting of several nested (and hard to mantain and expand upon) "if" conditions has been rationalized into clear methods with speaking names, comments have been added where an operation's purpose might not make immediate sense while rewieving the code, explaining its rationale.
- ***learning from other languages***: C# is a great language to write performing and scalable applications but a good dev's toolkit should expand into several domains so to serve the perfect tool for each task. One thing that here is borrowed from the Rust community is the use of Result types to ensure the code robustness, and while C# does not offer a direct equivalent out of the box the Result type behaviour can be emulated to achieve the same result. Here, the Item type is returned from a method and matched in a switch against a enum which contains all the allowed Item categories, so to cover every case at all types (avoiding that every dev working on the project makes their own version of this logic, inevitably opening it to bugs and logic breaks as the product expands).
<br><br>

**Expanding the tests**:<br><br>
Beyond the base tests that shipped with the projects, more have been added and used to ensure that the application logic would keep its intended behaviour during development. The added tests include verification for the depreciation behaviour for both the already present common and legendary items, as well as the newly added conjured items, and appreciation for items that gain in value as time passes.

<br><br>
---------------------------------------------------------------------------------

The project has been edited on a Linux machine running VS Code, using GIT version control in sync with GitHub:<br><br>
<img src="https://github.com/TrishkaV/csharpcore-Verify.xunit/assets/96583994/398f5ae6-b6d2-476b-9bb2-86cdc6d082c8"><br><br>

<br><br>
Thank you for checking out this project.
