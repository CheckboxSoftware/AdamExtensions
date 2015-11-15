# AdamExtensions
Project containing several extensions to the ADAM framework to make the life as ADAM partner a little easier.

## How to deploy to Config studio (aka System)
* download sources and build them
* copy the built dll (CbxSw.AdamExtensions.ConfigStudio.dll) to the bin folder of Config studio
* open the studio's web.config and add the xml below in the /configuration/adam.web.studio section:
```xml
<studioExtensions>
  <providers>
	<add type="CbxSw.AdamExtensions.ConfigStudio.StudioStartup, CbxSw.AdamExtensions.ConfigStudio" />
  </providers>
</studioExtensions>
```
From this point on the extensions are active and will be up and running.
Some translations might be missing or you might want to further configure the extensions so they are better visible.
For this you can find further steps and documentation on the [wiki](wiki).
