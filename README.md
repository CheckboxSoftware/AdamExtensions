# AdamExtensions
Project containing several extensions to the ADAM framework to make the life as ADAM partner a little easier. Beware that all sources are built using VS2015. So you should either use VisualStudio 2015 to build the sources or the [built releases](https://github.com/CheckboxSoftware/AdamExtensions/releases).

## How to deploy to Config studio (aka System)
* download sources and build them
* copy the built dll (CbxSw.AdamExtensions.ConfigStudio.dll) to the bin folder of Config studio
* open the studio's web.config and add the xml below in the /configuration/adam.web.studio section:
```xml
<studioExtensions>
  <providers>
	<add type="CbxSw.AdamExtensions.ConfigStudio.Startup, CbxSw.AdamExtensions.ConfigStudio" />
  </providers>
</studioExtensions>
```
From this point on the extensions are active and will be up and running.
Some translations might be missing or you might want to further configure the extensions so they are better visible.
For this you can find further steps and documentation on the [wiki](https://github.com/CheckboxSoftware/AdamExtensions/wiki).

# The extensions
Although there is one code base containing a number of different extensions, you can cherry pick your favourite(s) and use them in your project. Below you can find a list of the currently finished extensions:
* For ADAM core installation (ConfigStudio, AssetStudio and StudioSelector):
 * [Active users widget](https://github.com/CheckboxSoftware/AdamExtensions/wiki/View-Active-Users)
 * [Searching users by field](https://github.com/CheckboxSoftware/AdamExtensions/wiki/CustomUserListFieldControlBuilder)
 * [Assembly versions in AdamAdmin.axd](https://github.com/CheckboxSoftware/AdamExtensions/wiki/AdminHandlerAssemblyVersions)
* For Products:
 * [Product inheritance pages](https://github.com/CheckboxSoftware/AdamExtensions/wiki/Product-inheritance-pages)
