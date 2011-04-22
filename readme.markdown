
###Update: 22/04/2011


 - Support for Coffeescript Compiler 1.0.1 via [my fork](https://github.com/cynosura/CoffeeScript-Compiler-for-Windows) of the coffeeScript compiler for windows as a [git submodule](http://chrisjean.com/2009/04/20/git-submodules-adding-using-removing-and-updating/).
 
    - To use .coffee files with the less library, modify your asp.net config thus:
     <pre>
     &lt;configSections&gt;
...
          &lt;section 
              name="coffee" 
              type="dotless.Core.configuration.CoffeeConfigurationSectionHandler,dotless.Core" /&gt;
...
     &lt;/configSections&gt;
...
     &lt;coffee
           cache="true" 
           compilerPath="../../lib/coffeescript/coffee/jshost.exe" 
           compilerPattern="-u coffee-stdinout.js" /&gt;

     </pre>

     The sample project shows this in action.  Tested on VS2010

    - Tip: to load the coffeescript submodule, issue the following commands:
	
         `git submodule init`
         `git submodule update`
	 
Just Want a .dll?
=================

If you don't care about the source and just want a .dll you can visit our build server and download the latest release:
[DotLess Build Server](http://www.dotlesscss.com:8081/guestLogin.html?guest=1). 

Simply select for the latest successful build and click on the "Artifacts" section, here you'll find the latest compiler exe and any dll's required.


Whats this all about?
---------------------

This is a project to port the hugely useful Less libary to the .NET world. 
It give variables, nested rules and operators to CSS. 

For more information about the original Less project see [http://lesscss.org/](http://lesscss.org/).
For more information about how to get started with the .NET version see  [http://www.dotlesscss.org/](http://www.dotlesscss.org).
